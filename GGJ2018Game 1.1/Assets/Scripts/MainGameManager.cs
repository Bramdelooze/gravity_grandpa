using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour {

	[HideInInspector]
    public bool hasDied;
    private bool spawnDelayOn = false;

    public bool die = false;

	private GameObject checkpointManager;
	private GameObject mPlayer;

    public GameObject fadeInPanel;

	private float spawnDelay = .8f;

	private MeshRenderer renderer;




	public void Start()
    {
        
        fadeInPanel.SetActive(true);
		checkpointManager = GameObject.FindGameObjectWithTag("CheckPointManager");
		mPlayer = GameObject.FindGameObjectWithTag("Player");
		renderer = mPlayer.GetComponentInChildren<MeshRenderer>();
		Respawn();
    }

    public void Update()
    {
        die = false;
        if (hasDied)
        {
            hasDied = false;
            Debug.Log("player is dead");
			renderer.enabled = false;
			spawnDelayOn = true;
        }
        if (spawnDelayOn)
        {
            spawnDelay -= Time.deltaTime;
        }
        if (spawnDelay <= 0)
        {
            die = true;
			fadeInPanel.SetActive(false);
			fadeInPanel.SetActive(true);
			spawnDelayOn = false;
            spawnDelay = .8f;
            Respawn();
        }
    }

    void Respawn()
    {
		renderer.enabled = true;
		checkpointManager.GetComponent<CheckPointManager>().RespawnAtCheckpoint(mPlayer);
    }
}