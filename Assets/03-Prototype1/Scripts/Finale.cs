using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finale : MonoBehaviour {

    static public bool  goalHit = false;

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Player") {
            Finale.goalHit = true;
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
        }
    }
}
