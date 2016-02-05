using UnityEngine;
using System.Collections;

public class MergeAttachDetach : MonoBehaviour
{
	
	[SerializeField]
	public GameObject	leg, arm, torso;
	[SerializeField]
	public Sprite[] bodyStates;//{head, headTorso, headTorsoArm, headTorsoTwoArms,headTorsoTwoArmsLeg, headTorsoTwoArmsTwoLegs, headTorsoLeg, headTorsoTwoLegs, headTorsoArmLeg, headTorsoArmTwoLegs};
	
	/*
	 * 0 -  just the head
	 * 1 - head and torso
	 * 2 - head torso and one arm
	 * 3 - head torso and both arms
	 * 4 - head torso, both arms and one leg
	 * 5 - full body
	 * 6 - head, torso and one leg
	 * 7 - head, torso and both legs
	 * 8 - head, torso, leg and one arm
	 * 9 - head, torso, arm and two legs
	 */
	
	
	private Sprite currentBodyState; //stores the current state of the body, gets from the array
	
	private GameObject[] nearbyLimbsofType;
	public bool hasTorso, hasArm, hasSecondArm, hasLeg, hasSecondLeg;
	private Player player;
	private float minimumDistance = 3.5f;
	private Vector3 pos;
	// Use this for initialization
	void Start ()
	{
		player = GetComponent<Player> ();
		bodyStates =  new Sprite[10];
		//hasTorso = hasArm = hasLeg = hasSecondLeg = true; //for testing purposes start with all body parts except second hand
		assignState ();
		torso = GameObject.Find ("Torso");
		arm = GameObject.Find ("Arm");
		leg = GameObject.Find ("leg");
	}
	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.X)) {
			Debug.Log ("X Pressed");
			whichLimb ();
		} else {
			detach ();
		}
	}


	private void assignState () //assigns the state of the sprite
	{
		if (hasTorso && hasLeg && hasSecondLeg && hasArm && hasSecondArm) {
			currentBodyState = bodyStates [5];
		} else
		if (hasTorso && hasLeg && !hasSecondLeg && hasArm && !hasSecondArm) {
			currentBodyState = bodyStates [8];
		} else
		if (hasTorso && hasLeg && hasSecondLeg && !hasArm && !hasSecondArm) {
			currentBodyState = bodyStates [7];
		} else
		if (hasTorso && hasLeg && !hasSecondLeg && !hasArm && !hasSecondArm) {
			currentBodyState = bodyStates [6];
		} else
		if (hasTorso && hasLeg && !hasSecondLeg && hasArm && hasSecondArm) {
			currentBodyState = bodyStates [4];
		} else
		if (hasTorso && !hasLeg && !hasSecondLeg && hasArm && !hasSecondArm) {
			currentBodyState = bodyStates [2];
		} else
		if (hasTorso && !hasLeg && !hasSecondLeg && hasArm && hasSecondArm) {
			currentBodyState = bodyStates [3];
		} else
		if (!hasTorso && !hasLeg && !hasSecondLeg && !hasArm && !hasSecondArm) {
			currentBodyState = bodyStates [0];
		} else
		if (hasTorso && !hasLeg && !hasSecondLeg && !hasArm && !hasSecondArm) {
			currentBodyState = bodyStates [1];
		} else
		if (hasTorso && hasLeg && hasSecondLeg && hasArm && !hasSecondArm) {
			currentBodyState = bodyStates [9];
		} else if (!hasTorso)
			currentBodyState = bodyStates [0];
	}
	
	private void whichLimb ()
	{
		if (nearbyLimbOfType ("arm") && canAttach (arm)) {
			attachLimb (arm);
		} else if (nearbyLimbOfType ("leg") && canAttach (leg)) {
			attachLimb (leg);
		} else if (nearbyLimbOfType ("torso") && canAttach (torso)) {
			attachLimb (torso);
		}
		
	}
	
	private bool canAttach (GameObject limb)
	{
		if ((limb.tag == "arm") && (!hasSecondArm) && (hasTorso)) {
			return true;
		} else if (limb.tag == "leg" && !hasSecondLeg && hasTorso) {
			return true;
		} else if (limb.tag == "torso" && !hasTorso) {
			return true;
		} else
			return false;
	}
	
	bool nearbyLimbOfType (string tag)
	{
		nearbyLimbsofType = GameObject.FindGameObjectsWithTag (tag);
		
		for (int i = 0; i < nearbyLimbsofType.Length; ++i) {
			if (Vector3.Distance (transform.position, nearbyLimbsofType [i].transform.position) <= minimumDistance) {
				if (tag == "arm") {
					arm = nearbyLimbsofType [i];
					return true;
				} else if (tag == "leg") {
					leg = nearbyLimbsofType [i];
					return true;
				} else if (tag == "torso") {
					torso = nearbyLimbsofType [i];

					return true;
				}
				
			}
		}
		return false;
	}
	
	public void attachLimb (GameObject limb)
	{
		
		if (limb.tag == "torso") {
			hasTorso = true;
			assignState ();
			Debug.Log ("testing in here");
			torso.SetActive (false);
			
		} else if (limb.tag == "arm") {
			
			if (!hasArm) {
				hasArm = true;
			} else if(hasArm && !hasSecondArm){
				hasSecondArm = true;
			}
			
			assignState ();
			
			arm.SetActive (false);
			
		} else if (limb.tag == "leg") {
			if (!hasLeg) {
				hasLeg = true;
			} else if(hasLeg && !hasSecondLeg) {
				hasSecondLeg = true;
			}
			
			assignState ();
			
			leg.SetActive (false);
		}
	}
	
	public void bodyState ()
	{
		
		
		
	}
	
	public void detach ()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1) && hasTorso) {
			torso.SetActive (true);
			arm.SetActive(true);
			leg.SetActive(true);
			instantiateBodyParts(torso);
			hasTorso = false;
			hasArm = false;
			hasLeg = false;
			hasSecondArm = false;
			hasSecondLeg = false;
			assignState();
		} else if (Input.GetKeyDown (KeyCode.Alpha2) && hasArm) {
			arm.SetActive (true);
			instantiateBodyParts(arm);
			hasArm = false;
			assignState();
		} else if (Input.GetKey (KeyCode.Alpha2) && hasSecondArm) {
			arm.SetActive (true);
			hasSecondArm = false;
			instantiateBodyParts(arm);
			assignState();
		} else if (Input.GetKeyDown (KeyCode.Alpha3) && hasLeg) {
			leg.SetActive (true);
			hasLeg = false;
			instantiateBodyParts (leg);
			assignState();
		} else if (Input.GetKeyDown (KeyCode.Alpha3) && hasSecondLeg) {
			leg.SetActive(true);
			hasSecondLeg = false;
			instantiateBodyParts(leg);
			assignState();
		}
		
	}
	public void instantiateBodyParts(GameObject limbs){
		pos = player.transform.position;
		Instantiate (limbs, pos, Quaternion.Euler(0f, 0f, 0f));
		Destroy(limbs);
	}

}

