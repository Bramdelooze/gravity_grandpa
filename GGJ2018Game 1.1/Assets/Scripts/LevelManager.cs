using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public Sprite StartSprite;
	public Sprite CreditsSprite;
	public Sprite ExitSprite;

	public Image currentImage;


	public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

	public void SwapImageStart()
	{
		currentImage.sprite = StartSprite;
	}

	public void SwapImageCredits()
	{
		currentImage.sprite = CreditsSprite;
	}

	public void SwapImageExit()
	{
		currentImage.sprite = ExitSprite;
	}
}
