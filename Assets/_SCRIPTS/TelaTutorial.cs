using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TelaTutorial : MonoBehaviour 
{
    public CanvasGroup tut1, tut2;

    public void Proximo()
    {
        tut1.alpha = 0;
        tut1.blocksRaycasts = false;
        tut1.interactable = false;

        tut2.alpha = 1;
        tut2.blocksRaycasts = true;
        tut2.interactable = true;

    }

    public void Comecar()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void Voltar()
    {
        tut1.alpha = 1;
        tut1.blocksRaycasts = true;
        tut1.interactable = true;

        tut2.alpha = 0;
        tut2.blocksRaycasts = false;
        tut2.interactable = false;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
