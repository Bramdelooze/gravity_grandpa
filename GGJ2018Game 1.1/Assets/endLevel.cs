using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endLevel : MonoBehaviour {

    public Sprite DoorOpen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<SpriteRenderer>().sprite = DoorOpen;
        Debug.Log("Winning da game");



        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);



    }
}
