using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour
{

    public LevelManager mylevelmanager;

    // Use this for initialization
    void Start()
    {
        mylevelmanager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("player respwan here");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            Debug.Log("player respwan here");
            mylevelmanager.respawnPlayer();
        }

    }

}