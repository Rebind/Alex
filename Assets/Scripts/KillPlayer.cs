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
        if (other.tag == "Player")
        {
            Debug.Log("player respawn here");
            mylevelmanager.respawnPlayer();
        } 
        if (other.tag == "leg" || other.tag == "arm") {
            mylevelmanager.respawnLimb(other.name);
        }

    }

}