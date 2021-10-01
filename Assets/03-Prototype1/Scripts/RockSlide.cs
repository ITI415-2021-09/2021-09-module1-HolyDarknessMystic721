using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSlide : MonoBehaviour {

    [Header("Set in Inspector")]

    public GameObject enemiesPrefab;

    public float secondsBetweenEnemiesSpawn = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke ("DropRock", 2f);
    }

    void DropRock() {

        GameObject rock = Instantiate<GameObject>(enemiesPrefab);
        rock.transform.position = transform.position;
        Invoke("DropRock", secondsBetweenEnemiesSpawn);

    }
}
