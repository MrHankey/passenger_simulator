using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {
	SearchDoor,
	SearchSeat,
	Done,
    Push,
}

public class passenger : MonoBehaviour {

    public float maxVelocity = 1.4f;
    public float maxAccel = 5;
    public float maxForce = 5;

	public float dst_speed;
	public float esc_speed;
	public float esc_eps = 1;
	public float ctr_speed;
	public float ctr_eps = 1;
	private Rigidbody rb;
	private State state = State.Done;

    public float aggro;  // in [0,1)
    public float aggro_vs_other;  // in [0,1)
    public float aggro_strength = 10;
    public float aggro_cooldown = 2; // seconds
    private float aggro_next = 0;
    private GameObject push;

    private void AddTargetVelocityImpulse(Vector3 targetVelocity)
    {
        targetVelocity = Vector3.ClampMagnitude(targetVelocity, maxVelocity);
        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        // Maybe re-enable
        //velocityChange.x = Mathf.Clamp(velocityChange.x, -maxAccel, maxAccel);
        //velocityChange.z = Mathf.Clamp(velocityChange.z, -maxAccel, maxAccel);
        velocityChange.y = 0;

        //clamp max force
        velocityChange = Vector3.ClampMagnitude(velocityChange, maxForce);
        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        //Debug.DrawRay(transform.position, velocityChange, Color.red, 0.01f, false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal, Color.red, 0.01f, false);
        //}
        if (collision.gameObject.CompareTag("door"))
        {
            state = State.SearchSeat;
        }
        //else if (collision.gameObject.CompareTag("seat"))
        //{
        //    state = State.Done;
        //}
    }

    public void SearchDoor()
    {
        state = State.SearchDoor;
    }

    void OnCollisionStay(Collision collision) {
        if (state == State.Push) return;
        if (aggro != 0 && (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("enemy"))) {
            if (Time.time > aggro_next) {
                aggro_next = Time.time + aggro_cooldown;
                float random = Random.value;
                float a = aggro;
                // it is less likely that enemies push each other
                if (collision.gameObject.CompareTag("enemy")) a = 0*aggro_vs_other;
                if (random <= a) {
                    //maybe use this too? or stun player
                    //Vector3 dir = collision.impulse.normalized;
                    //collision.gameObject.GetComponent<Rigidbody>().AddForce(-dir * aggro_strength * rb.mass, ForceMode.Impulse);

                    push = collision.gameObject;
                    state = State.Push;
                    dst_speed *= 2;
                    gameObject.GetComponent<PassengerAudio>().CheckAggro();
                }
            }

            gameObject.GetComponent<PassengerAudio>().CheckBump();
        }

    }

    void Start() {
		rb = GetComponent<Rigidbody>();
        aggro_next = Time.time + aggro_cooldown * Random.value;
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector3 target = new Vector3(0.0f, 0.0f, 0.0f);
		switch (state) {
			case State.SearchDoor:
				target = new Vector3(-100000.0f, 0.0f, 0.0f);
				GameObject[] doors = GameObject.FindGameObjectsWithTag("door");
				foreach (GameObject d in doors) {
					Vector3 door_pos = d.GetComponent<Transform>().position;
					if ((rb.position - door_pos).magnitude < (rb.position - target).magnitude) {
						target = door_pos;
					}
				}
				break;

			case State.SearchSeat:
				//GetComponent<Renderer>().color = new Color(0,1,0);
				target = new Vector3(-100000.0f, 0.0f, 0.0f);
				GameObject[] seats = GameObject.FindGameObjectsWithTag("seat");
				foreach (GameObject s in seats) {
					Vector3 seat_pos = s.GetComponent<Transform>().position;
					if ((rb.position - seat_pos).magnitude < (rb.position - target).magnitude) {
						target = seat_pos;
					}
				}
				break;

            case State.Push:
                target = push.GetComponent<Transform>().position;
                if (Time.time > aggro_next - aggro_cooldown + 1.0f) {
                    state = State.SearchDoor; // only push for 0.5s
                    dst_speed /= 2;
                }
                break;

			case State.Done:
				break;
		}


        Vector3 dst_dir = new Vector3();
        Vector3 center_dir = new Vector3();
        Vector3 escape_dir = new Vector3();

        //if (state != State.Push) {
        //    // I don't like other passengers
        //    /*             _1
        //    *     	/\
        //    *____/  \____ _0
        //    */
        //    Collider[] others = Physics.OverlapSphere(rb.position, 20); // just some randius
        //    foreach (Collider o in others)
        //    {
        //        if (o.gameObject.tag != "enemy") continue;
        //        if (!o.GetComponent<Rigidbody>()) continue;
        //        Vector3 o_pos = o.GetComponent<Rigidbody>().position;
        //        Vector3 o_dist = o_pos - rb.position;
        //        o_dist.y = 0.0f;
        //        float dst_sq;
        //        dst_sq = o_dist.sqrMagnitude;
        //        //float dst_sq = o_dist.magnitude;
        //        if (dst_sq != 0.0f)
        //        {
        //            float weight = Mathf.Max(0.0f, 1.0f - esc_eps * dst_sq);
        //            escape_dir += weight * -o_dist.normalized;
        //        }
        //    }
        //    escape_dir.y = 0.0f;
        //}

        if (state != State.Done) {
			// go to target
			dst_dir = (target -  rb.position).normalized;
			dst_dir.y = 0.0f;

			// I want to stay to the center of the door
			/* ___         __ _1
			 *    \      /
			 *     \____/     _0
			 */
			center_dir = new Vector3(0.0f, 0.0f, 0.0f);
			float center_dist = target.x - rb.position.x;
			float center_weight = Mathf.Max(0.0f, -1.0f + ctr_eps * Mathf.Abs(center_dist) + target.z - rb.position.z);
			center_dir.x = center_weight * Mathf.Sign(center_dist);
			center_dir.x = Mathf.Min(1.0f, center_dir.x);

		}
        //rb.AddForce(dst_speed * dst_dir + esc_speed * escape_dir + ctr_speed * center_dir);
        Vector3 finalVelocity = dst_speed * dst_dir + esc_speed * escape_dir + ctr_speed * center_dir;
        finalVelocity = finalVelocity.normalized * maxVelocity;
        if (finalVelocity.magnitude > 0.01f)
        {
            AddTargetVelocityImpulse(finalVelocity);
            transform.forward = Vector3.Lerp(transform.forward, finalVelocity.normalized, 0.1f);
        }
	}
}
