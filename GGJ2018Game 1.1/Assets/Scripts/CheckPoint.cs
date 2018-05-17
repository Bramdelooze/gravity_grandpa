using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
	[HideInInspector]
	public bool hasBeenChecked = false;

    [SerializeField]
    private Sprite checkPointActive;

	public AudioSource audioSource;

	public enum Orientation
	{
		Down,
		Left,
		Up,
		Right
	}

	public Orientation orientation;

	// Use this for initialization
	void Start()
	{

	}

	public void Reset()
	{
		hasBeenChecked = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!hasBeenChecked)
		{
			audioSource.Play();
		}

		hasBeenChecked = true;
        GetComponent<SpriteRenderer>().sprite = checkPointActive;
		

	}
}
