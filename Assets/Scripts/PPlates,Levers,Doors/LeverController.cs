using UnityEngine;
using System.Collections;

public class LeverController : MonoBehaviour {

	BoxCollider2D myCollider;


	public bool moveDoor;
	public bool movePlatform;

	public float maximumActivationDistance;

	public GameObject[] affectedDoors;
	public GameObject[] affectedPlatforms;

	[SerializeField]
	public GameObject camera;

	private CameraFollow myCamera;



	bool onPlate;
	bool oneTime;



	// Use this for initialization
	public void Start () {
		maximumActivationDistance = 1.5f;
		myCamera = camera.GetComponent<CameraFollow> ();
		oneTime = false;
		onPlate = false;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Z) && nearLever()){
			toggleLever ();
			Debug.Log("Lever Tripped");
			//lever toggle
		}

	}


	bool nearLever(){
		if (myCamera.target.tag == "Player" || myCamera.target.tag == "arm") {
			if (Vector3.Distance (this.transform.position, myCamera.targetTransform.position) < maximumActivationDistance) {
				return true;
			} else {
				return false;
			}
		} else
			return false;

	}


	public void toggleLever(){

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


}