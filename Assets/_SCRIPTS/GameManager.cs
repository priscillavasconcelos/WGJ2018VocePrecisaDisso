using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public bool gameOn = true;
    public float timeLeft = 60.0f;
    public int objetosNaFrente = 0;
    public int itensDaCaroxinha = 0, caroxinhaTotal;
    public static GameManager gm;
    public Slider tempo;
    public Image vovo, telaFim;
    public Sprite vovoRindo, vovoTriste, telaPerdeu, telaGanhou;
    public Text textoVovo;
    public CanvasGroup balao, panelEndGame;
    public RectTransform agulha;
    public AudioClip calcinha, dentadura, guitarra, novelo, pizza, perdeu, ganhou;
    public AudioSource cuco, porta;
    AudioSource som;
    float proporcao;
    void Awake()
    {
        gm = this;
    }

    // Use this for initialization
    void Start () 
    {
        som = GetComponent<AudioSource>();
        itensDaCaroxinha = GameObject.FindGameObjectsWithTag("Guardar").Length;
        tempo = GameObject.FindWithTag("Relogio").GetComponent<Slider>();
        tempo.maxValue = timeLeft;
        tempo.value = timeLeft;
        caroxinhaTotal = itensDaCaroxinha;
        proporcao = ((-90 * (itensDaCaroxinha-1)) / caroxinhaTotal);
        proporcao = proporcao + 90;
    }

    void Update()
    {
        if (gameOn)
        {
            timeLeft -= Time.deltaTime;
            tempo.value = timeLeft;
            if (timeLeft < 0)
            {
                som.clip = perdeu;
                som.Play();
                gameOn = false;
                textoVovo.text = "Droga...";
                StartCoroutine(SomeTexto());
                panelEndGame.alpha = 1f;
                panelEndGame.interactable = true;
                panelEndGame.blocksRaycasts = true;
                telaFim.sprite = telaPerdeu;
            }
            else
            {
                CucoTime();
            }

            if (itensDaCaroxinha <= 0)
            {
                som.clip = perdeu;
                som.Play();
                gameOn = false;
                textoVovo.text = "Droga...";
                StartCoroutine(SomeTexto());
                panelEndGame.alpha = 1f;
                panelEndGame.interactable = true;
                panelEndGame.blocksRaycasts = true;
                telaFim.sprite = telaPerdeu;
            }

            if (objetosNaFrente == 0)
            {
                if (!porta.isPlaying)
                {
                    porta.Play();
                    som.clip = ganhou;
                    som.Play();
                    gameOn = false;
                    textoVovo.text = "Ah, muito obrigada, viu?";
                    StartCoroutine(SomeTexto());
                    panelEndGame.alpha = 1f;
                    panelEndGame.interactable = true;
                    panelEndGame.blocksRaycasts = true;
                    telaFim.sprite = telaGanhou;
                }

            }
        }

    }

    void CucoTime()
    {
        if (timeLeft > 20)
        {
            if ((int)timeLeft % 10 == 0)
            {
                if (!cuco.isPlaying)
                {
                    cuco.Play();
                }
            }
        }
        else if (timeLeft > 10)
        {
            if ((int)timeLeft % 5 == 0)
            {
                if (!cuco.isPlaying)
                {
                    cuco.Play();
                }
            }
        }
        else
        {
            if ((int)timeLeft % 2 == 0)
            {
                if (!cuco.isPlaying)
                {
                    cuco.Play();
                }
            }
        }
    }

    public void JogouFora(string nome, string tag)
    {
        switch (nome)
        {
            case "Pizza(Clone)":
                som.clip = pizza;
                som.Play();
                textoVovo.text = "Ah não, adoro pizza de anchova.";
                StartCoroutine(SomeTexto());
                break;
            case "Dentadura(Clone)":
                som.clip = dentadura;
                som.Play();
                textoVovo.text = "Oh, lá foi embora meu sorriso...";
                StartCoroutine(SomeTexto());
                break;
            case "Calcinha(Clone)":
                som.clip = calcinha;
                som.Play();
                textoVovo.text = "Ah, que pouca vergonha!";
                StartCoroutine(SomeTexto());
                break;
            case "Guitarra(Clone)":
                som.clip = guitarra;
                som.Play();
                textoVovo.text = "Oh, como vou fazer meus rocks agora?";
                StartCoroutine(SomeTexto());
                break;
            case "Novelo(Clone)":
                som.clip = novelo;
                som.Play();
                textoVovo.text = "Droga, minhas futuras meias...";
                StartCoroutine(SomeTexto());
                break;
        }

        if (tag == "Guardar")
        {
            if (itensDaCaroxinha < caroxinhaTotal / 2)
            {
                vovo.sprite = vovoTriste;
            }
            agulha.rotation = Quaternion.Euler(0, 0, agulha.rotation.eulerAngles.z - proporcao);
        }
    }

    IEnumerator SomeTexto()
    {
        balao.alpha = 1f;
        yield return new WaitForSeconds(3.0f);
        balao.alpha = 0f;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Sair()
    {
        Application.Quit();
    }
}
