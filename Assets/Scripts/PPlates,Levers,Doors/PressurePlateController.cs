using UnityEngine;
using System.Collections;

public class PressurePlateController : MonoBehaviour
{

    BoxCollider2D myCollider;


    public bool moveDoor;
    public bool movePlatform;
    public Sprite activeSprite;
    public Sprite inactiveSprite;

    public GameObject[] affectedDoors;
    public GameObject[] affectedPlatforms;
    SpriteRenderer spriteRenderer;



    bool onPlate;
    bool oneTime;



    // Use this for initialization
    public void Start()
    {
        oneTime = false;
        onPlate = false;
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

    }



    void OnTriggerStay2D(Collider2D other)
    {
        if (!oneTime)
        {
            if (other.CompareTag("Player") || other.CompareTag("arm") || other.CompareTag("torso") || other.CompareTag("leg"))
            {

                spriteRenderer.color = new Color(0f, 1f, 0f, 1f);
                //spriteRenderer.sprite = activeSprite;
                onPlate = true;

                if (moveDoor)
                { //toggles the door(s) states

                    foreach (GameObject door in affectedDoors)
                    {

                        if (!door.GetComponent<DoorController>().requireMultiplePlates)
                        {
                            door.GetComponent<DoorController>().turnOn();
                        }
                        else if (door.GetComponent<DoorController>().requireMultiplePlates)
                        {
                            door.GetComponent<DoorController>().platesActivated++;
                            door.GetComponent<DoorController>().turnOn();


                        }
                    }




                }
                if (movePlatform)
                { //toggles the state of the platform(s)
                    foreach (GameObject platform in affectedPlatforms)
                    {

                        platform.GetComponent<PlatformController>().turnOn();
                    }
                }
            }
            oneTime = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("arm") || other.CompareTag("torso") || other.CompareTag("leg"))
        {
            spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
            //spriteRenderer.sprite = inactiveSprite;

            onPlate = false;
            Debug.Log("Off Plate");
            //Destroy (this.gameObject);

            if (moveDoor)
            { //toggles the door(s) states

                foreach (GameObject door in affectedDoors)
                {

                    if (!door.GetComponent<DoorController>().requireMultiplePlates)
                    {
                        door.GetComponent<DoorController>().turnOff();
                    }
                    else if (door.GetComponent<DoorController>().requireMultiplePlates)
                    {
                        door.GetComponent<DoorController>().platesActivated--;
                        door.GetComponent<DoorController>().turnOff();

                    }
                }




            }
            if (movePlatform)
            { //toggles the state of the platform(s)
                foreach (GameObject platform in affectedPlatforms)
                {

                    platform.GetComponent<PlatformController>().turnOff();
                }

            }
        }
        oneTime = false;
    }


    // Update is called once per frame






    //returns true or false if plate is colliding with an object

}