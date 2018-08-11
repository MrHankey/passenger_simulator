using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {
	SearchDoor,
	SearchSeat,
	Done,
}

public class passenger : MonoBehaviour {

	public float dst_speed;
	public float esc_speed;
	public float esc_eps = 1;
	public float ctr_speed;
	public float ctr_eps = 1;
	private Rigidbody rb;
	private State state = State.SearchDoor;

	void Start() {
		rb = GetComponent<Rigidbody>();
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

				// check if we are already inside
				if (rb.position.x < 0.0 && (target - rb.position).sqrMagnitude < 1.0) {
					state = State.SearchSeat;
					goto case State.SearchSeat;
				}
				break;

			case State.SearchSeat:
				renderer.GetComponent<Renderer>().color = new Color(0,1,0);
				target = new Vector3(-100000.0f, 0.0f, 0.0f);
				GameObject[] seats = GameObject.FindGameObjectsWithTag("seat");
				foreach (GameObject s in seats) {
					Vector3 seat_pos = s.GetComponent<Transform>().position;
					if ((rb.position - seat_pos).magnitude < (rb.position - target).magnitude) {
						target = seat_pos;
					}
				}

				// check if we are already inside
				if (rb.position.x < 0.0 && (target - rb.position).sqrMagnitude < 1.0) {
					state = State.Done;
					goto case State.Done;
				}
				break;

			case State.Done:
				break;
		}

		if (state != State.Done) {
			// go to target
			Vector3 dst_dir = (target -  rb.position).normalized;
			dst_dir.y = 0.0f;

			// I don't like other passengers
			/*             _1
		   *     	/\
			 *____/  \____ _0
			 */
			Vector3 escape_dir = new Vector3(0.0f, 0.0f, 0.0f);
			Collider[] others = Physics.OverlapSphere(rb.position, 20); // just some randius
			foreach (Collider o in others) {
				if (o.gameObject.tag != "enemy")  continue;
				if (!o.GetComponent<Rigidbody>()) continue;
				Vector3 o_pos  = o.GetComponent<Rigidbody>().position;
				Vector3 o_dist = o_pos - rb.position;
				o_dist.y = 0.0f;
				float dst_sq;
				dst_sq = o_dist.sqrMagnitude;
				//float dst_sq = o_dist.magnitude;
				if (dst_sq != 0.0f) {
					float weight = Mathf.Max(0.0f, 1.0f - esc_eps * dst_sq);
					escape_dir += weight * -o_dist.normalized;
				}
			}
			escape_dir.y= 0.0f;

			// I want to stay to the center of the door
			/* ___         __ _1
			 *    \      /
			 *     \____/     _0
			 */
			Vector3 center_dir = new Vector3(0.0f, 0.0f, 0.0f);
			float center_dist = target.z - rb.position.z;
			float center_weight = Mathf.Max(0.0f, -1.0f + target.x - rb.position.x + ctr_eps * Mathf.Abs(center_dist));
			center_dir.z = center_weight * Mathf.Sign(center_dist);
			center_dir.z = Mathf.Min(1.0f, center_dir.z);

			rb.AddForce(dst_speed * dst_dir + esc_speed * escape_dir + ctr_speed * center_dir);
		}
	}
}
