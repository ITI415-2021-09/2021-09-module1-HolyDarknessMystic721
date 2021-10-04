using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour {
    public TextMeshProUGUI scoreCounter;

    private int score;

    public float speed = 0;

    private Rigidbody rb;

    private float movementX;
    private float movementY;

    public GameObject winText;


    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();

        winText.SetActive(false);

        score = 0;

        SetCountText();
    }

    void SetCountText() {

        scoreCounter.text = "Score: " + score.ToString();
    }

    private void OnMove(InputValue movementValue) {

        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate() {

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Collectable")) {

            other.gameObject.SetActive(false);

            score = score + 100;
            
            SetCountText();
        }

        /*if (other.gameObject.CompareTag("Enemies")) {

            score = score - 1;

            SetCountText();

        }*/ // code i tried

        if (other.gameObject.tag == "Goal") {
            
            winText.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision coll) { //to find out what hits the basket
        GameObject collidedWith = coll.gameObject;

        if (collidedWith.tag == "Enemies") {
            Destroy(collidedWith);

            score = score - 1;

            SetCountText();
        }
    }
}
