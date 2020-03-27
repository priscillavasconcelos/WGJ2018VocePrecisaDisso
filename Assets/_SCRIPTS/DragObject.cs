using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour 
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool noLixo = false, noArmario = false, comMouse = false;

    void UnityApiMouseEvents()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

        if (hit.rigidbody != null)
        {
            hit.rigidbody.gameObject.SendMessage("OnMouseDown");
        } 
        else
        {
            hit.collider.SendMessage("OnMouseDown");
        }
            
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        if (noLixo)
        {
            if (transform.CompareTag("Guardar"))
            {
                GameManager.gm.itensDaCaroxinha--;
                GameManager.gm.timeLeft -= 5;
            }
            GameManager.gm.JogouFora(transform.name, transform.tag);
            Destroy(gameObject);
        }

        if (noArmario)
        {
            if (transform.CompareTag("Guardar"))
            {
                print("Eu gosto muito desse item");
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Lixeira":
                noLixo = true;
                break;
            case "Armario":
                noArmario = true;
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Lixeira":
                noLixo = false;
                break;
            case "Armario":
                noArmario = false;
                break;
        }
    }
}
