using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LimbController : MonoBehaviour
{

	[SerializeField]
	public GameObject leg, arm, torso, twoLegs, twoArms;
	[SerializeField]
	public Sprite[] bodyStates;

	List<GameObject> armsList;
	List<GameObject> torsoList;
	List<GameObject> legsList;

	public AudioClip attachSound;


	private float timer;


	string objectTag;
	private Sprite currentBodyState; //stores the current state of the body, gets from the array
	private Animator myAnimator; //Animator for the different states
	private GameObject[] nearbyLimbsofType;
	public bool hasTorso, hasArm, hasSecondArm, hasLeg, hasSecondLeg;
	public bool hasBoot, hasTorch, hasShovel, hasPickaxe;
	private Player player;
	private float minimumDistance = 2.5f;
	private Vector3 pos;
	private Sound sounds;
	private bool playSound;
	private LimbController checkLimbs;
	private bool dpadY = false;
	private bool dpadX = false;

	// Use this for initialization
	void Start()
	{
		objectTag = "";
		armsList = new List<GameObject> ();
		legsList = new List<GameObject> ();
		torsoList = new List<GameObject> ();
		myAnimator = GetComponent<Animator>();
		player = GetComponent<Player>();
		bodyStates = new Sprite[10];
		torso = GameObject.Find("Torso");
		arm = GameObject.Find("Arm");
		leg = GameObject.Find("Leg");
		twoArms = GameObject.Find("Arm");
		twoLegs = GameObject.Find("Leg");
		sounds = player.GetComponent<Sound>();
		playSound = false;
		assignState();

	}

	void Update()
	{
		if (player.enabled) {
		
			addLimbsToLists ();
			if (Input.GetKeyDown (KeyCode.X) || Input.GetButtonDown ("Xbox_BButton")) {
				whichLimb ();
			} else if (Input.GetAxisRaw ("XBox_DPadX") != 0 || Input.GetAxisRaw ("XBox_DPadY") != 0 || 
			       Input.GetKeyDown (KeyCode.Alpha1) || Input.GetKeyDown (KeyCode.Alpha2) || Input.GetKeyDown (KeyCode.Alpha3)) {
			
				detach ();
			}

			//Players automatically has a pickaxe if they have an arm
			if (!hasArm && !hasSecondArm)
				hasPickaxe = false;
			//This is to make sure the axis on the xbox is pressed only once. 
			if(Input.GetAxisRaw("XBox_DPadY") == 0){
				dpadY = false;
			}
			if(Input.GetAxisRaw("XBox_DPadX") == 0){
				dpadX = false;
			}

			//handleSounds ();
		
		}
		timer += Time.deltaTime;
			
	}

	/*
	 * 
	 * This is changing the players body states to whatever 
	 * the player attaches itself to. 
	 * 
	 * 
	 * */

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
	private void assignState() //assigns the state of the sprite
	{
		if (hasTorso && hasLeg && hasSecondLeg && hasArm && hasSecondArm)
		{
			myAnimator.SetInteger("state", 5);
			currentBodyState = bodyStates[5];
		}
		else if (hasTorso && hasLeg && !hasSecondLeg && hasArm && !hasSecondArm){
			myAnimator.SetInteger("state", 8);
			currentBodyState = bodyStates[8];
		}
		else if (hasTorso && hasLeg && hasSecondLeg && !hasArm && !hasSecondArm)
		{
			myAnimator.SetInteger("state", 7);
			currentBodyState = bodyStates[7];
		}
		else if (hasTorso && hasLeg && !hasSecondLeg && !hasArm && !hasSecondArm)
		{
			myAnimator.SetInteger("state", 6);
			currentBodyState = bodyStates[6];
		}
		else if (hasTorso && hasLeg && !hasSecondLeg && hasArm && hasSecondArm)
		{
			myAnimator.SetInteger("state", 4);
			currentBodyState = bodyStates[4];
		}
		else if (hasTorso && !hasLeg && !hasSecondLeg && hasArm && !hasSecondArm)
		{
			myAnimator.SetInteger("state", 2);
			currentBodyState = bodyStates[2];
		}
		else if (hasTorso && !hasLeg && !hasSecondLeg && hasArm && hasSecondArm)
		{
			myAnimator.SetInteger("state", 3);
			currentBodyState = bodyStates[3];
		}
		else if (!hasTorso && !hasLeg && !hasSecondLeg && !hasArm && !hasSecondArm)
		{
			myAnimator.SetInteger("state", 0);
			currentBodyState = bodyStates[0];
		}
		else if (hasTorso && !hasLeg && !hasSecondLeg && !hasArm && !hasSecondArm)
		{
			myAnimator.SetInteger("state", 1);
			currentBodyState = bodyStates[1];
		}
		else if (hasTorso && hasLeg && hasSecondLeg && hasArm && !hasSecondArm)
		{
			myAnimator.SetInteger("state", 9);
			currentBodyState = bodyStates[9];
		}
		else if (!hasTorso)
			myAnimator.SetInteger("state", 0);
		currentBodyState = bodyStates[0];
	}

	/*
	 * 
	 * finds and attaches the nearest limb
	 * 
	 * 
	 * */
	private void whichLimb()
	{
		if (nearbyLimbOfType("arm") )//&& canAttach(arm))
		{
			sounds.audioAttach.Play();
			attachLimb(armsList);	
		}
		else if  ((nearbyLimbOfType("pickaxe")))
		{
			sounds.audioAttach.Play();
			attachLimb(armsList);
		}
		else if (nearbyLimbOfType("leg") )//&& canAttach(leg))
		{
			sounds.audioAttach.Play();
			attachLimb(legsList);
		}
		else if (nearbyLimbOfType("torso") )//&& canAttach(torso))
		{
			sounds.audioAttach.Play();
			attachLimb(torsoList);
		}

	}

	/*
	* 
	* This returns true if player is near a certain limb object
		* 
		* 
		* */
		bool nearbyLimbOfType(string tag)
	{
		List<GameObject> whichList = new List<GameObject>();


		if (tag == "torso") {
			whichList = torsoList;

		} else if (tag == "arm" || tag == "pickaxe") {

			whichList = armsList;

		} else if (tag == "leg") {

			whichList = legsList;

		}
		
		//Checking if players are near an arm or legs they can attach to
		for (int i = 0; i < whichList.Count; ++i)
		{

			if (Vector3.Distance(transform.position, whichList[i].transform.position) <= minimumDistance)
			{
				if (whichList[i].tag == "torso" && !hasTorso && (whichList[i].gameObject.activeSelf == true))
				{
					torso = whichList[i].gameObject;
					objectTag = "torso";
					return true;
				}
				else if (whichList[i].tag == "arm" && !hasArm && !hasSecondArm && hasTorso && (whichList[i].gameObject.activeSelf == true))
				{
					arm = whichList[i].gameObject;
					objectTag = "arm";
					return true;
				}

				else if(whichList[i].tag == "arm" && hasArm && !hasSecondArm && hasTorso && (whichList[i].gameObject.activeSelf == true)){
					twoArms = whichList[i].gameObject;
					objectTag = "arm";
					return true;
				}
				else if (whichList[i].tag == "leg" && !hasLeg && !hasSecondLeg && hasTorso && (whichList[i].gameObject.activeSelf == true))
				{
					leg = whichList[i].gameObject;
					objectTag = "leg";
					return true;
				}
				else if(whichList[i].tag == "leg"  && hasLeg && !hasSecondLeg && hasTorso && (whichList[i].gameObject.activeSelf == true)){
					twoLegs = whichList[i].gameObject;
					objectTag = "leg";
					return true;
				}
			}
		}
		return false;
	}

	/*
	 * 
	 * Multiple Limbs store into an array list
	 * 
	 * 
	 * */

	void addLimbsToLists(){
		Transform[] hinges = GameObject.FindObjectsOfType(typeof(Transform)) as Transform[];
		foreach (Transform go in hinges) {
			if(go.tag == "arm"){
				armsList.Add(go.gameObject);
			}
			else if(go.tag == "torso"){
				torsoList.Add (go.gameObject);
			}
			else if(go.tag == "leg"){
				legsList.Add (go.gameObject);
			}
		}

	}


	/*
	 * 
	 * This is attaching the limb.
	 * It will disable the object in the game if player decides to attach.
	 * Then, it will change the player's state.
	 * 
	 * */
	public void attachLimb(List<GameObject> limb)
	{

		if (!hasTorso && objectTag == "torso")
		{
			hasTorso = true;
			assignState();
			//torso.SetActive (false);
			torso.transform.Translate(new Vector3(999999,99999,9999));

			//torso.GetComponent<Player>().enabled = false;



		}
		else if (hasTorso && objectTag == "arm")
		{
			
			if (!hasArm)
			{

				arm.transform.Translate(new Vector3(999999,99999,9999));
	
				arm.GetComponent<Player>().enabled = false;

				
				hasArm = true;

			}
			else if (hasArm && !hasSecondArm)
			{
				hasSecondArm = true;
				twoArms.transform.Translate(new Vector3(999999,99999,9999));
				twoArms.GetComponent<Player>().enabled = false;

			}
			hasPickaxe = true;
			assignState();
		}
		else if (objectTag == "leg" && hasTorso)
		{
			if (!hasLeg)
			{
				hasLeg = true;
				leg.transform.Translate(new Vector3(999999,99999,9999));
				leg.GetComponent<Player>().enabled = false;

			}
			else if (hasLeg && !hasSecondLeg)
			{

	
				twoLegs.transform.Translate(new Vector3(999999,99999,9999));
				twoLegs.GetComponent<Player>().enabled = false;

				hasSecondLeg = true;
			}

			assignState();


		}
	}

	/*
	 * 
	 * This is the detach part.
	 * Checks if player has the necessary parts to detach
	 * Then, the limb will respawn where players are at. 
	 * 
	 * 
	 * */
	public void detach()
	{
		
		if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetAxisRaw("XBox_DPadY") == 1) && hasTorso && !hasArm && !hasLeg)
		{
			if (dpadY == false) {
				if(player.isClimbing){
					player.isClimbing = false;
				}
				torso.SetActive (true); 
				//checkForDifferentLimbs ();
				instantiateBodyParts (torso);
				hasTorso = false;
				assignState ();
				sounds.audioDetach.Play ();
				player.isClimbing = false;
				dpadY = true;
			}
		}
		if ((Input.GetKeyDown(KeyCode.Alpha2) || Input.GetAxisRaw("XBox_DPadX") == 1 || Input.GetAxisRaw("XBox_DPadX") == -1)   && hasArm && !hasSecondArm )
		{
			if (dpadX == false) {
				arm.SetActive (true);
				instantiateBodyParts (arm);
				hasArm = false;
				assignState ();
				sounds.audioDetach.Play ();
				
				//Change animations?
				dpadX = true;
			}
			
		}
		if ((Input.GetKeyDown(KeyCode.Alpha2) || Input.GetAxisRaw("XBox_DPadX") == 1 || Input.GetAxisRaw("XBox_DPadX") == -1) && hasArm && hasSecondArm)
		{
			if (dpadX == false) {
				if(player.isClimbing){
					player.isClimbing = false;
				}
				twoArms.SetActive (true);
				instantiateBodyParts (twoArms);
				hasSecondArm = false;
				assignState ();
				sounds.audioDetach.Play ();
				//Change animations here?
				
				dpadX = true;
			}
			
			
			
		}
		else if ((Input.GetKeyDown(KeyCode.Alpha3) || Input.GetAxisRaw("XBox_DPadY") == -1) && hasLeg && !hasSecondLeg)
		{
			if (dpadY == false) {
				leg.SetActive (true);
				hasLeg = false;
				instantiateBodyParts (leg);
				assignState ();
				sounds.audioDetach.Play ();
				dpadY = true;
			}
		}
		else if ((Input.GetKeyDown(KeyCode.Alpha3) || Input.GetAxisRaw("XBox_DPadY") == -1) && hasSecondLeg)
		{
			if (dpadY == false) {
				twoLegs.SetActive (true);
				hasSecondLeg = false;
				sounds.audioDetach.Play ();
				instantiateBodyParts (twoLegs);
				assignState ();
				dpadY = true;
			}
		}


	}


	

	/*
	 * 
	 * This is checking if players have a torso, arms, and legs, and
	 * they want to detach the torso. If players detach the torso, 
	 * everything will fall apart. 
	 * 
	 * 
	 * */
	public void checkForDifferentLimbs()
	{

		if (hasArm && !hasSecondArm)
		{
			arm.SetActive(true);
			instantiateBodyParts(arm);
		}
		else if (hasArm && hasSecondArm)
		{
			twoArms.SetActive(true);
			arm.SetActive(true);
			instantiateBodyParts(twoArms);
			instantiateBodyParts(arm);
		}
		if (hasLeg && !hasSecondLeg)
		{
			leg.SetActive(true);
			instantiateBodyParts(leg);
		}
		else if (hasLeg && hasSecondLeg)
		{
			twoLegs.SetActive(true);
			leg.SetActive(true);
			instantiateBodyParts(twoLegs);
			instantiateBodyParts(leg);
		}
		hasTorso = false;
		hasArm = false;
		hasLeg = false;
		hasSecondArm = false;
		hasSecondLeg = false;
	}

	/*
	
		If players detach an arm or leg, that will set the game object active
		and it will detach near the player. 
	
	*/
	private void instantiateBodyParts(GameObject limb)
	{
		pos = player.transform.position;
		if (limb.tag != "torso") {
			limb.transform.position = pos;
		}
		else if (limb.tag == "torso") {
			limb.transform.position = new Vector3(pos.x,pos.y+4f,pos.z);

		}

	}
	
	/*
	 *
	 * Handle sound effects here. Play the sound when players go left or right. Stop the sound when 
	 * players are not moving or pressing the arrow key
	 * 
	 * 
	 * 
	private void handleSounds()
	{
		if(Input.GetAxisRaw("Horizontal") == 1 && !playSound)
		{
			playSoundDifferentLimbs();
			playSound = true;
		}
		else if (Input.GetAxisRaw("Horizontal") == 0 || (Input.GetAxisRaw("Horizontal") == 1 && (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("Xbox_LeftButton"))) ||
				(Input.GetAxisRaw("Horizontal") == -1 && (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("Xbox_LeftButton"))))
		{

			playSound = false;
			stopSound();
		}
		else if (Input.GetAxisRaw("Horizontal") == -1 && !playSound)
		{
			playSoundDifferentLimbs();
			playSound = true;
		}	
		
		


	}

	/*
	 * Checking for the different limbs in order to play some sounds according to 
	 * the respective body states
	 * 
	 * 
	private void playSoundDifferentLimbs()
	{
		if(!hasTorso)
		{
			sounds.audioHeadRoll.Play();
		}
		else if(hasTorso && (!hasLeg && !hasSecondLeg)){
			sounds.audioFoot.Stop();
			sounds.audioHeadRoll.Stop();
			sounds.audioTorso.Play();
		}
		else if(hasTorso && (hasLeg || hasSecondLeg))
		{
			sounds.audioHeadRoll.Stop();
			sounds.audioTorso.Stop();
			sounds.audioFoot.Play();
		}


	}*/

	/*
	 * This is to stop the sound from playing when players release the keya
	 * 
	 * 
	 * 
	private void stopSound()
	{
		if(!hasTorso)
		{
			sounds.audioHeadRoll.Stop();
		}
		else if(hasTorso && (!hasLeg && !hasSecondLeg)){
			sounds.audioTorso.Stop();
		}
		else if(hasTorso && (hasLeg || hasSecondLeg))
		{
			sounds.audioFoot.Stop();
		}

	}*/

}