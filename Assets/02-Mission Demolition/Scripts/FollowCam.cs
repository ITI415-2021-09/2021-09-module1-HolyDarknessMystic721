using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {
    static public GameObject POI; // the static point of interest

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ; // desired Z position of the camera

    void Awake() {
        camZ = this.transform.position.z;
    }

    void FixedUpdate() {
        // if there is only one line following an if, it doesnt' need braces
        //if (POI == null) return; //return if no POI

        //get position of the POI
        //Vector3 destination = POI.transform.position;

        Vector3 destination;
        // if there is no POI, return to P: [0, 0, 0]
        if (POI == null) {
            destination = Vector3.zero;
        } else {
            //get the position of POI
            destination = POI.transform.position;
            //if POI is a projectile, check to see if its at rest
            if (POI.tag == "Projectile") {
                //if its sleepimg (not moving)
                if (POI.GetComponent<Rigidbody>().IsSleeping()) {
                    POI = null;
                    // in the next update
                    return;
                }
            }
        }
        //limit the X and Y to minimum values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //interpolate fromthe current Camera position destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        //force destination.z to be camZ to keep the camera far enough away.
        destination.z = camZ;
        //set the camera to the destination
        transform.position = destination;
        // set the orthographicSize of the Camera to keep Ground in view
        Camera.main.orthographicSize = destination.y + 10;
    }
}
