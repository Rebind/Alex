using UnityEngine;
using System.Collections;

public class Nose : MonoBehaviour {

	public Player playerScript;
	private GameObject Player;
	private GameObject Particle_water;
	//private Vector3 addHigh = new Vector3(0,20.0f,0);
	private float addHigh;
	private float timer;
	private float timerMax;//time for adding high to the player


	// Use this for initialization
	public void Start () {
		Player = GameObject.Find("Player");
		playerScript = Player.GetComponent<Player>();
		addHigh = 30f;
		timer = 0;
		timerMax = 5;//time for adding high to the player
		Particle_water = GameObject.Find("Particle_water");

	}

	void Update(){
		timer += Time.deltaTime;
		if (timer >= timerMax) {
			//Debug.Log ("timerMax reached !");

			addHigh = 30f;
			Particle_water.GetComponent<Renderer>().enabled = true;
			//playerScript.notOnNose = false;
			// reset timer
		} 
		//stop adding high
		if (timer > timerMax+5) {
			addHigh = 0;
			timer = 0;
			Particle_water.GetComponent<Renderer>().enabled = false;
			//playerScript.notOnNose = true;
		}
		//Debug.Log (addHigh);
	}
	void OnTriggerStay2D(Collider2D other){

		if (other.CompareTag ("Player") || other.CompareTag ("arm") || other.CompareTag ("torso") || other.CompareTag ("leg")) {
			Debug.Log (playerScript.velocity.y);
			if (addHigh > 0){
				playerScript.notOnNose = false;
			}	
			if (playerScript.velocity.y < 0 && addHigh > 0) {
				playerScript.velocity.y = 0;
			}
			playerScript.velocity.y += addHigh * Time.deltaTime;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		playerScript.notOnNose = true;
	}

}