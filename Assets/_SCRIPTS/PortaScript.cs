using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaScript : MonoBehaviour 
{
    public Sprite portaAberta;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Guardar") || collision.CompareTag("Lixo"))
        {
            GameManager.gm.objetosNaFrente++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Guardar") || collision.CompareTag("Lixo"))
        {
            GameManager.gm.objetosNaFrente--;
        }

        if (GameManager.gm.objetosNaFrente <= 0)
        {
            GetComponent<SpriteRenderer>().sprite = portaAberta;
        }
    }
}
