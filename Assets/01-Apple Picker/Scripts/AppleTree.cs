using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]

    //Prefab for instantiating apples
    public GameObject applePrefab;

    //Speed that the AppleTree moves
    public float speed = 1f;

    //Distance where AppleTree turns around
    public float leftAndRightEdge = 10f;

    //chance that the AppleTree will change directions
    public float chanceToChangeDirections = 0.1f;

    //Rate at which Apples will be instantiated
    public float secondsBetweenAppleDrops = 1f;

    // Start is called before the first frame update
    void Start() {
        //Needs apples dropping every second
        Invoke ("DropApple", 2f);
    }
    void DropApple() {
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke ("DropApple", secondsBetweenAppleDrops);
    }

    // Update is called once per frame
    void Update() { 
        // Below is for basic movement.
        Vector3 pos = transform.position; 
        pos.x += speed * Time.deltaTime; 
        transform.position = pos;

        /*Below is for changing directions randomly. 
        The first one is to move to the right. 
        The second one is to move to the left.*/
        if (pos.x < -leftAndRightEdge) { 
        speed = Mathf.Abs(speed);

        } else if (pos.x > leftAndRightEdge) { 
            speed = -Mathf.Abs(speed);
        }

    }
    void FixedUpdate() {
        //To change directions randomly is time-based
        if (Random.value < chanceToChangeDirections) {
            speed *= -1;
        }
    }

}
