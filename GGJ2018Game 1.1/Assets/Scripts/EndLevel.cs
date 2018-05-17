using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {

    public Sprite DoorOpen;

	public AudioSource audioSource;

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

		audioSource.Play();
		Time.timeScale = 0;
		StartCoroutine(WaitForEndJingle());
    }

	IEnumerator WaitForEndJingle()
	{
		yield return new WaitForSecondsRealtime(3);
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
