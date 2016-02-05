using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D{
	public class SwitchControl : MonoBehaviour {
		//Instance of Camera2DFollow script
		public CameraFollow CameraScript;
		//Instance of certain game objects
		public GameObject 	cameraObject, 
							playerObject, 
							armObject, 
							arm1Object, 
							torsoObject, 
							legObject, 
							leg1Object;
		//Instance of certain game objects scripts
		public Player 	playerScript, 
						armScript, 
						arm1Script, 
						torsoScript, 
						legScript, 
						leg1Script;
		//string to keep track of which body part is being controlled
		public string inControl 	= "";
		//set distance between two objects that can be switch control to
		public float distBetween 	= 20f;
		//function to calculate distance between two game objects
		private float calcDistance(string tmp1, string tmp2)
		{
			Transform tr1 	= GameObject.Find(tmp1).transform;
			Transform tr2 	= GameObject.Find(tmp2).transform;
			float distance 	= Vector3.Distance(tr1.position, tr2.position);
			return distance; 
		}
		//function to toggle player script on or off
		private void toggleScript(string targetobj, int OnOff)
		{
			if (OnOff == 1)
			{
				switch(targetobj)
				{
					case "Player": 	playerScript.enabled = true;
									break;
					case "Arm":		armScript.enabled = true;
									break;
					case "Arm (1)":	arm1Script.enabled = true;
									break;
					case "Torso":	torsoScript.enabled = true;
									break;
					case "Leg":		legScript.enabled = true;
									break;
					case "Leg (1)":	leg1Script.enabled = true;
									break;			
				}
			} else 
			{
				switch(targetobj)
				{
					case "Player": 	playerScript.enabled = false;
									break;
					case "Arm":		armScript.enabled = false;
									break;
					case "Arm (1)":	arm1Script.enabled = false;
									break;
					case "Torso":	torsoScript.enabled = false;
									break;
					case "Leg":		legScript.enabled = false;
									break;
					case "Leg (1)":	leg1Script.enabled = false;
									break;			
				}
			}
		}
		// Use this for initialization
		void Start () {
			//initializing instances of game objects
			cameraObject = GameObject.Find("Main Camera");
			CameraScript = cameraObject.GetComponent<CameraFollow>();

			playerObject	= GameObject.Find("Player");
			armObject 		= GameObject.Find("Arm");
			arm1Object	 	= GameObject.Find("Arm (1)");
			torsoObject 	= GameObject.Find("Torso");
			legObject 		= GameObject.Find("Leg");
			leg1Object 		= GameObject.Find("Leg (1)");
			//initializing the instances of game object scripts
			playerScript 	= playerObject.GetComponent<Player>();
			armScript 		= armObject.GetComponent<Player>();
			arm1Script 		= arm1Object.GetComponent<Player>();
			torsoScript 	= torsoObject.GetComponent<Player>();
			legScript 		= legObject.GetComponent<Player>();
			leg1Script 		= leg1Object.GetComponent<Player>();

			inControl = "Player";
			//toggleScript("Player", 1);
			//OneScriptOn("Player");
		}
	
		// Update is called once per frame
		void Update () {
			Debug.Log (distBetween);
			distBetween = 20f;
			if (Input.GetKeyUp(KeyCode.Q) && inControl != "Player"){
				
					//Debug.Log("Q is pressed");
					//changing the "target" variable inside Camera2DFollow
					//script to change thet target of the camera
					CameraScript.ChangeTarget("Player");
					//switch on the script of this certain game object
					toggleScript("Player", 1);
					toggleScript(inControl, 0);
					inControl = "Player";

			}
			if (Input.GetKeyUp(KeyCode.E) && inControl != "Arm (1)"){
				if(calcDistance("Arm (1)", inControl) <= distBetween){
					//Debug.Log("E is pressed");
					CameraScript.ChangeTarget("Arm (1)");
					toggleScript("Arm (1)", 1);
					toggleScript(inControl, 0);
					inControl = "Arm (1)";
				}
			}
			if (Input.GetKeyUp(KeyCode.R) && inControl != "Arm"){
				if(calcDistance("Arm", inControl) <= distBetween){
					//Debug.Log("R is pressed");
					CameraScript.ChangeTarget("Arm");
					toggleScript("Arm", 1);
					toggleScript(inControl, 0);
					inControl = "Arm";
				}
			}
			if (Input.GetKeyUp(KeyCode.T) && inControl != "Torso"){
				if(calcDistance("Torso", inControl) <= distBetween){
					//Debug.Log("T is pressed");
					CameraScript.ChangeTarget("Torso");
					toggleScript("Torso", 1);
					toggleScript(inControl, 0);
					inControl = "Torso";
				}
			}
			if (Input.GetKeyUp(KeyCode.Y) && inControl != "Leg"){
				if(calcDistance("Leg", inControl) <= distBetween){
					//Debug.Log("Y is pressed");
					CameraScript.ChangeTarget("Leg");
					toggleScript("Leg", 1);
					toggleScript(inControl, 0);
					inControl = "Leg";
				}
			}
			if (Input.GetKeyUp(KeyCode.U) && inControl != "Leg (1)"){
				if(calcDistance("Leg (1)", inControl) <= distBetween){
					//Debug.Log("U is pressed");
					CameraScript.ChangeTarget("Leg (1)");
					toggleScript("Leg (1)", 1);
					toggleScript(inControl, 0);
					inControl = "Leg (1)";
				}
			}
		}
	}
}

