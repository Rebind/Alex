using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	// Use this for initialization

	public bool open;
	private BoxCollider2D myCollider;
	private MeshRenderer myRenderer;

	void Start () {
		assignState ();		
		myCollider = gameObject.GetComponent<BoxCollider2D>() as BoxCollider2D;
		myRenderer = gameObject.GetComponent<MeshRenderer> () as MeshRenderer;
	}
	


	void assignState(){
		myCollider = gameObject.GetComponent<BoxCollider2D>() as BoxCollider2D;

		if (open) {
			Debug.Log ("open");

			gameObject.GetComponent<MeshRenderer> ().enabled = false;
			myCollider.size = new Vector2(0,0);

		} else if (!open) {
			Debug.Log ("closed");

			gameObject.GetComponent<MeshRenderer> ().enabled = true;
			myCollider.size = new Vector2(1,1);
		}
	}

	public void toggle(){
		open = !open;
		assignState ();
	}
}
