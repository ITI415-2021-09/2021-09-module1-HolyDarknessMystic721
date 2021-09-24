using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode {
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour {

    static private MissionDemolition S; //a private Singleton

    [Header("Set in Inspector")]
    public Text uitLevel; // UIText_Level Text
    public Text uitShots; // UIText_Shots Text
    public Text uitButton; // Text on UIButton_View
    public Vector3 castlePos; // place to put castles
    public GameObject[] castles; // array of the castles

    [Header("Set Dynamically")]
    public int level; // the current level
    public int levelMax; //number of levels
    public int shotsTaken; 
    public GameObject castle; // the current castle
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; //FollowCam mode

    // Start is called before the first frame update
    void Start() {
        S = this; //Define the Singleton

        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel() {
        // get rid of the old castle 
        if (castle != null) {
            Destroy(castle);
        }

        //destroy old projectiles if they exist
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos) {
            Destroy(pTemp);
        }

        //instantiate teh new castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        //reset the camera
        SwitchView("Show Both");
        ProjectileLine.S.Clear();

        //reset the goal
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    }

    void UpdateGUI() {
        //Show the data in the GUITexts
        uitLevel.text = "Level: " + (level+1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }

    void Update() {
        UpdateGUI();

        // check for level end
        if ( (mode == GameMode.playing) && Goal.goalMet) {
            //change mode to stop checking for level end
            mode = GameMode.levelEnd;
            //zoom out 
            SwitchView("Show Both");
            //Start the next level in 2 seconds
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel() {
        level++;
        if (level == levelMax) {
            level = 0;
        }
        StartLevel();
    }

    public void SwitchView (string eView = "") {
        if (eView == "") {
            eView = uitButton.text;
        }
        showing = eView;
        switch (showing) {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;

            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;

            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }

    // static method that allows code anywhere to increment shotsTaken 
    public static void ShotFired() {
        S.shotsTaken++;
    }
}
