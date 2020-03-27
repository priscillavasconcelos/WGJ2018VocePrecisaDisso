using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnaObjetos : MonoBehaviour 
{
    //public GameObject porta;
    public List<GameObject> porta, itensGood, itensBad;
    public int qtdItensGood, qtdItensBad;

    // Use this for initialization
    void Awake () 
    {
        SpawnarObjetos(1, porta);
        SpawnarObjetos(qtdItensGood, itensGood);
        SpawnarObjetos(qtdItensBad, itensBad);
    }

    void SpawnarObjetos(int qtd, List<GameObject> prefab)
    {
        for (int x = 0; x < qtd; x++)
        {
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(100, 100)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width-100, 0)).x);

            float spawnY = Random.Range
(Camera.main.ScreenToWorldPoint(new Vector2(100, 100)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height-100)).y);

            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
            int random = Random.Range(0, prefab.Count);
            Instantiate(prefab[random], spawnPosition, Quaternion.identity);
            prefab.RemoveAt(random);
        }
    }
}
