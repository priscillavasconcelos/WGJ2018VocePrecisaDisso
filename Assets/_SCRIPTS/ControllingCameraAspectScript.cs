using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllingCameraAspectScript : MonoBehaviour
{
    public float colThickness = 4f;
    public float zPosition = 0f;
    private Vector2 screenSize;

    // Use this for initialization
    void Awake()
    {
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        //float targetaspect = 9.0f / 16.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        //float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = Camera.main.gameObject.GetComponent<Camera>();
        //Generate world space point information for position and scale calculations
        Vector3 cameraPos = Camera.main.transform.position;

        //Grab the world-space position values of the start and end positions of the screen, then calculate the distance between them and store it as half, since we only need half that value for distance away from the camera to the edge
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        // if scaled height is less than current height, add letterbox
        //if (scaleheight < 1.0f)
        //{
        //    Rect rect = camera.rect;

        //    rect.width = 1.0f;
        //    rect.height = scaleheight;
        //    rect.x = 0;
        //    rect.y = (1.0f - scaleheight) / 2.0f;

        //    camera.rect = rect;
        //}
        //else // add pillarbox
        //{
        //    float scalewidth = 1.0f / scaleheight;

        //    Rect rect = camera.rect;

        //    rect.width = scalewidth;
        //    rect.height = 1.0f;
        //    rect.x = (1.0f - scalewidth) / 2.0f;
        //    rect.y = 0;

        //    camera.rect = rect;
        //}

        //GameObject areaDentro = new GameObject();
        //areaDentro.transform.localScale = new Vector3(screenSize.x * 2, screenSize.y * 2, colThickness);

        //areaDentro.AddComponent<BoxCollider2D>();
        //areaDentro.GetComponent<BoxCollider2D>().isTrigger = true;

        //areaDentro.tag = "Respawn";

        //Dictionary<string, Transform> uppers = new Dictionary<string, Transform>();
        //for (int i = 0; i < 7; i++)
        //{
        //    uppers.Add(i.ToString(), new GameObject().transform);
        //}

        //foreach (KeyValuePair<string, Transform> valPair in uppers)
        //{
        //    valPair.Value.gameObject.AddComponent<BoxCollider2D>(); //Add our colliders. Remove the "2D", if you would like 3D colliders.

        //    valPair.Value.name = valPair.Key + "Collider"; //Set the object's name to it's "Key" name, and take on "Collider".  i.e: TopCollider
        //    valPair.Value.parent = transform; //Make the object a child of whatever object this script is on (preferably the camera)

        //    valPair.Value.localScale = new Vector3(screenSize.x / 2.5f, colThickness / 3, colThickness);
        //}

        //for (int i = 0; i < 7; i++)
        //{
        //    //uppers[i.ToString()].position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (uppers[i.ToString()].localScale.y * 0.5f), zPosition);
        //    if (i == 0)
        //    {
        //        uppers[i.ToString()].position = new Vector3(cameraPos.x - screenSize.x, cameraPos.y + screenSize.y + (uppers[i.ToString()].localScale.y * 0.5f), zPosition);
        //        uppers[i.ToString()].Rotate(new Vector3(0, 0, -30));
        //    }
        //    else
        //    {
        //        int a = i;
        //        a--;

        //        uppers[i.ToString()].position = (uppers[a.ToString()].position + (transform.right * screenSize.x / 3f));
        //        if (i == 6)
        //        {
        //            uppers[i.ToString()].Rotate(new Vector3(0, 0, 30));
        //        }
        //        else
        //        {
        //            uppers[i.ToString()].Rotate(new Vector3(0, 0, Random.Range(-20, 20)));
        //        }
        //    }

        //}


        //Create a Dictionary to contain all our Objects/Transforms
        Dictionary<string, Transform> colliders = new Dictionary<string, Transform>();
        //Create our GameObjects and add their Transform components to the Dictionary we created above
        //colliders.Add("Top", new GameObject().transform);
        colliders.Add("Bottom", new GameObject().transform);
        colliders.Add("Right", new GameObject().transform);
        colliders.Add("Left", new GameObject().transform);


        //For each Transform/Object in our Dictionary
        foreach (KeyValuePair<string, Transform> valPair in colliders)
        {
            valPair.Value.gameObject.AddComponent<BoxCollider2D>(); //Add our colliders. Remove the "2D", if you would like 3D colliders.


            valPair.Value.name = valPair.Key + "Collider"; //Set the object's name to it's "Key" name, and take on "Collider".  i.e: TopCollider
            valPair.Value.parent = transform; //Make the object a child of whatever object this script is on (preferably the camera)

            if (valPair.Key == "Left" || valPair.Key == "Right") //Scale the object to the width and height of the screen, using the world-space values calculated earlier
            {
                valPair.Value.localScale = new Vector3(colThickness, screenSize.y * 2, colThickness);
            }
            else
            {
                valPair.Value.localScale = new Vector3(screenSize.x * 2, colThickness, colThickness);
            }

        }
        //Change positions to align perfectly with outter-edge of screen, adding the world-space values of the screen we generated earlier, and adding/subtracting them with the current camera position, 
        //as well as add/subtracting half out objects size so it's not just half way off-screen
        colliders["Right"].position = new Vector3(cameraPos.x + screenSize.x + (colliders["Right"].localScale.x * 0.5f), cameraPos.y, zPosition);
        colliders["Left"].position = new Vector3(cameraPos.x - screenSize.x - (colliders["Left"].localScale.x * 0.5f), cameraPos.y, zPosition);
        //colliders["Top"].position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (colliders["Top"].localScale.y * 0.5f), zPosition);
        //colliders["Bottom"].position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (colliders["Bottom"].localScale.y * 0.5f), zPosition);
        //Pra ficar em cima do gol. e nao no limite da tela
        colliders["Bottom"].position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (colliders["Bottom"].localScale.y * 0.5f) + 0.5f, zPosition);

    }

}
