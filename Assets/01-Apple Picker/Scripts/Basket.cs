using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour {
    [Header("Set Dynamically")]
    public Text scoreGT;

    void Start() {
        // to find a reference to the ScoreConter Game Object
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        // to get the Text Component of that Game Object
        scoreGT = scoreGO.GetComponent<Text>();
        //to set the starting number of points to 0
        scoreGT.text = "0";
    }

    // Update is called once per frame
    void Update() {
        Vector3 mousePos2D = Input.mousePosition;

        mousePos2D.z = -Camera.main.transform.position.z;

        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    void OnCollisionEnter(Collision coll) { //to find out what hits the basket
        GameObject collidedWith = coll.gameObject;

        if (collidedWith.tag == "Apple") {
            Destroy(collidedWith);

            // to parse the text of the scoreGT into an int
            int score = int.Parse(scoreGT.text);
            // to add points for catching the apple
            score += 100;
            // to convert the score back to a string and then displays it
            scoreGT.text = score.ToString();
        }
    }
}
