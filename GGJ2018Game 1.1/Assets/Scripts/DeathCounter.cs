using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathCounter : MonoBehaviour {

    private MainGameManager gameManager;
    public static int deadCounter;
    public Text deathText;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameManager = FindObjectOfType<MainGameManager>();
    }
    // Update is called once per frame
    void Update () {
        if (gameManager.die)
        {
            deadCounter++;
        }
        if (SceneManager.GetActiveScene().buildIndex >= 2 && SceneManager.GetActiveScene().buildIndex <= 7)
        {
            deathText.text = "Death Count: " + (deadCounter);
        }
        else deathText.text = "";
	}
}
