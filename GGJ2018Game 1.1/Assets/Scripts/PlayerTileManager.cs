using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerTileManager : MonoBehaviour {

	[HideInInspector]
    public GameObject mainGameManager;

	public AudioClip Auch1;
	public AudioClip Auch2;
	public AudioClip Auch3;

	public AudioSource audioSource;

	public GameObject BloodParticle;

	// Use this for initialization
	void Start () {
		mainGameManager = GameObject.FindGameObjectWithTag("GameManager");
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "DieLayer" || collision.gameObject.tag == "Death")
        {
            mainGameManager.GetComponent<MainGameManager>().hasDied = true;
			//todo maybe check if we have lives
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
			int rand = Random.Range(0, 3);
			switch(rand)
			{
				case 0:
					audioSource.clip = Auch1;
					break;
				case 1:
					audioSource.clip = Auch2;
					break;
				case 2:
					audioSource.clip = Auch3;
					break;
			}
			audioSource.Play();

			Instantiate(BloodParticle, transform.position, Quaternion.identity);
			mainGameManager.GetComponent<MainGameManager>().hasDied = true;
        }
        if (collision.gameObject.name == "EndLevelLayer")
        {
            print("Level Completed, Loading Next Level");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
    }
}