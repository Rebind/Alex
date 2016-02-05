using UnityEngine;
using System.Collections;

public class PressurePlateController : MonoBehaviour {

	BoxCollider2D myCollider;


	public bool moveDoor;
	public bool movePlatform;

	public GameObject[] affectedDoors;
	public GameObject[] affectedPlatforms;


	bool onPlate;
	bool oneTime;



	// Use this for initialization
	public void Start () {
		oneTime = false;
		onPlate = false;
		myCollider = GetComponent<BoxCollider2D>();
	}



	void OnTriggerStay2D(Collider2D other)
	{
		if (!oneTime) {
			if (other.CompareTag ("Player") || other.CompareTag ("arm") || other.CompareTag ("torso") || other.CompareTag ("leg")) {
			
				onPlate = true;
				Debug.Log ("onplate");
				//Destroy (this.gameObject);

				if (moveDoor) { //toggles the door(s) states
					foreach (GameObject door in affectedDoors) {

						door.GetComponent<DoorController> ().toggle ();
					}

				}
				if (movePlatform) { //toggles the state of the platform(s)
					foreach (GameObject platform in affectedPlatforms) {

						platform.GetComponent<PlatformController> ().toggle ();
					}
				}
			}
			oneTime = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player")|| other.CompareTag("arm") || other.CompareTag("torso") || other.CompareTag("leg"))
		{
			onPlate = false;
			Debug.Log ("Off Plate");
			//Destroy (this.gameObject);

			if (moveDoor) { //toggles the door(s) states
				foreach (GameObject door in affectedDoors) {

					door.GetComponent<DoorController> ().toggle ();
				}

			} if (movePlatform) { //toggles the state of the platform(s)
				foreach (GameObject platform in affectedPlatforms) {

					platform.GetComponent<PlatformController> ().toggle ();
				}

			}
		} oneTime = false;
	}


	// Update is called once per frame


	//handles logic of what to do if plate is pressed
	void plateLogic(){


	}


	//returns true or false if plate is colliding with an object

}