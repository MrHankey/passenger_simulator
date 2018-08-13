using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipPool : MonoBehaviour {

    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip clip6;
    public AudioClip clip7;
    public AudioClip clip8;
    public AudioClip clip9;
    public AudioClip clip10;
    public AudioClip clip11;
    public AudioClip clip12;
    public AudioClip clip13;
    public AudioClip clip14;
    public AudioClip clip15;
    public AudioClip clip16;
    public AudioClip clip17;
    public AudioClip clip18;
    public AudioClip clip19;
    public AudioClip clip20;
    public AudioClip clip21;
    public AudioClip clip22;
    public AudioClip clip23;
    public AudioClip clip24;
    public AudioClip clip25;
    public AudioClip clip26;
    public AudioClip clip27;
    public AudioClip clip28;
    public AudioClip clip29;
    public AudioClip clip30;
    public AudioClip clip31;
    public AudioClip clip32;
    public AudioClip clip33;
    public AudioClip clip34;
    public AudioClip clip35;

    AudioClip[] pool;

    void Start() {
        pool = new AudioClip[] {
            clip1, clip2, clip3, clip4, clip5,
            clip6, clip7, clip8, clip9, clip10,
            clip11, clip12, clip13, clip14, clip15,
            clip16, clip17, clip18, clip19, clip20,
            clip21, clip22, clip23, clip24, clip25,
            clip26, clip27, clip28, clip29, clip30,
            clip31, clip32, clip33, clip34, clip35
        };
    }

    public AudioClip GetRandomIdleClip() {
        int choice = -1;
        while (choice < 0 || choice > 21) {
            choice = (int)System.Math.Floor(Random.value * 22f);
        }
        return pool[choice];
    }

    public AudioClip GetRandomBumpClip() {
        int choice = -1;
        while (choice < 22 || choice > 28) {
            choice = (int)System.Math.Floor(Random.value * 29f);
        }
        return pool[choice];
    }

    public AudioClip GetRandomAggroClip() {
        int choice = -1;
        while (choice < 29 || choice > 34) {
            choice = (int)System.Math.Floor(Random.value * 35f);
        }
        return pool[choice];
    }
}
