using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour 
{
    AudioSource source;
    public CanvasGroup inicial, creditos;
	// Use this for initialization
	void Start () 
    {
        source = GetComponent<AudioSource>();
	}

    public void Sair()
    {
        Application.Quit();
    }

    public void Jogar()
    {
        source.Play();
        StartCoroutine(EsperaAudio());
    }

    IEnumerator EsperaAudio()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Tutorial");
    }

    public void Creditos()
    {
        creditos.alpha = 1f;
        creditos.interactable = true;
        creditos.blocksRaycasts = true;
        inicial.alpha = 0f;
        inicial.interactable = false;
        inicial.blocksRaycasts = false;
    }

    public void Voltar()
    {
        creditos.alpha = 0f;
        creditos.interactable = false;
        creditos.blocksRaycasts = false;
        inicial.alpha = 1f;
        inicial.interactable = true;
        inicial.blocksRaycasts = true;
    }
}
