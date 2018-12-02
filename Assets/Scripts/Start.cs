using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Start : MonoBehaviour {

	// Use this for initialization

    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
	
	public void ExitGame()
    {
        Application.Quit();
    }
}
