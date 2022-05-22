using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerAction : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Vector3 positionToSpawn;
    private bool alreadySpawned = false;

    void Start()
    {
        if (prefabToSpawn == null)
        {
            prefabToSpawn = GameObject.Find("Empty");
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            switch(gameObject.tag)
            {
                case "SpawnBandit":
                    if (!alreadySpawned)
                    {
                        alreadySpawned = true;
                        Instantiate(prefabToSpawn, positionToSpawn, Quaternion.identity);
                        Destroy(gameObject);
                    }
                    break;
                case "NextLevel":
                    Globals.currentLevel += 1;
                    SceneManager.LoadSceneAsync(Globals.currentLevel);
                    break;
                case "RecallToBaseCamp":
                    SceneManager.LoadSceneAsync("EndGame");
                    break;
            }
        }
        // if (col.gameObject.tag == "Player" && !alreadySpawned)
        // {
        //     alreadySpawned = true;
        //     Instantiate(prefabToSpawn, positionToSpawn, Quaternion.identity);
        //     Destroy(gameObject);
        // }
    }
}
