using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public Animator Buttons;
    public GameObject pressAny;

    Canvas canvas;
    bool on;

    void Start () {
        canvas = GetComponent<Canvas>();
	}
	
	void Update () {
        if (Input.anyKey)
        {
            pressAny.SetActive(false);
            Buttons.Play("MenuButtons");
            on = true;
        }
        
	}

    public void Play()
    {
        Debug.Log("Jogo");
        //SceneManager.LoadScene("Jogo");
    }

    public void Credits()
    {
        Debug.Log("Creditos");
        //SceneManager.LoadScene("Credits");
    }

    public void Exit()
    {
        Debug.Log("Sair");
        Application.Quit();
    }
}
