using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	// Use this for initialization

	public bool open;
	public bool requireMultiplePlates;
	public bool mouthDoor;

    public Animator mouth;
	private bool startState;
	private bool opening;
	private bool closing;
	private BoxCollider2D myCollider;
    private Rigidbody2D myRigidBody;


	public int neededToOpen;

	[HideInInspector]
	public int platesActivated;


	void Start () {

		if (mouthDoor) {
			mouth = GetComponent<Animator> ();
		}
		assignState ();
		myCollider = gameObject.GetComponent<BoxCollider2D>() as BoxCollider2D;
        startState = open;

    }



    void assignState(){
		myCollider = gameObject.GetComponent<BoxCollider2D>() as BoxCollider2D;

		if (open) {
			
			Debug.Log ("open");
			myCollider.size = new Vector2(0,0);
			if (mouthDoor) {
				mouth.SetBool ("open", true);		//gameObject.GetComponent<MeshRenderer> ().enabled = false;
				myRigidBody.gravityScale = 1;
			}

			if (!mouthDoor) {
				opening = true;
			}
				
		} else if (!open) {
			Debug.Log ("closed");
            //gameObject.GetComponent<MeshRenderer> ().enabled = true;
//            mouth.SetBool("open", false);

			if (!mouthDoor) {
				closing = true;
			}

            if (gameObject.name == "mouth_door")
            {
                myCollider.size = new Vector2(1.5f, 0.8f);
            }
            else
            {
                myCollider.size = new Vector2(1, 1);
            }
        }
    }

	public void toggle(){
		open = !open;
		assignState ();

	}



    public void turnOn()
    {
		if (!requireMultiplePlates) {
			open = !startState;
			assignState ();
		} else if (requireMultiplePlates) {
			Debug.Log (platesActivated);
			if (platesActivated == neededToOpen) {
				open = !startState;
				assignState ();
			}
		}
    }

    public void turnOff()
    {
        open = startState;
        assignState();
    }


	public void Update(){
		Color color = this.gameObject.GetComponent<MeshRenderer> ().material.color;
		Debug.Log (color.a);


		if (opening && open) {

			this.gameObject.GetComponent<MeshRenderer> ().material.color = new UnityEngine.Color (color.r, color.g, color.b, color.a - .025f);   
			if (this.gameObject.GetComponent<MeshRenderer> ().material.color.a <= 0f) {
				opening = false;
			}
		}
		if (closing && !open) {

			this.gameObject.GetComponent<MeshRenderer> ().material.color = new UnityEngine.Color (color.r, color.g, color.b, color.a + .025f);   
			if (this.gameObject.GetComponent<MeshRenderer> ().material.color.a >= 1f) {
				closing = false;
			}
		}

	}
}
