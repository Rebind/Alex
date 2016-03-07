using UnityEngine;
using System.Collections;

public class PushBox : MonoBehaviour
{
    private GameObject Player;
    private LimbController arm;
    private Rigidbody2D rgbd;
    private Animator myAnimator;
	private Vector3 addGap = new Vector3(.3f,0,0);
	private Vector3 temPosition;
	private float temSpeed;
	public Player playerScript;
    private Animator playerAnimator;
	
	public AudioClip rockSound;
	public AudioSource audioRock;
	

    void Start()
    {
        Player = GameObject.Find("Player");
        arm = Player.GetComponent<LimbController>();
        rgbd = GetComponent<Rigidbody2D>();
		playerScript = Player.GetComponent<Player>();
		temSpeed = playerScript.moveSpeed;
        playerAnimator = Player.GetComponent<Animator>();
		audioRock = this.gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        //Debug.Log(gameObject.transform.position.x);
		pushController();
    }

	private void pushController(){
		if ((Input.GetKeyDown(KeyCode.H) || Input.GetButtonDown("Xbox_XButton")) && (arm.hasArm || arm.hasSecondArm) && playerAnimator.GetFloat("speed") > 0.1)
		{
			//get the position when player press "h"
			temPosition = gameObject.transform.position;
			rgbd.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            /*if (playerAnimator.GetFloat("speed") > 0.1)
            {*/
                playerAnimator.SetLayerWeight(4, 1);
           // }
                gameObject.layer = 11;
				
			//Play sound here
			playSoundEffect();
		}
		if ((Input.GetKeyUp(KeyCode.H) || Input.GetButtonUp("Xbox_XButton")) && (arm.hasArm || arm.hasSecondArm))
		{
			//Stop sound from playing
			stopSoundEffect();
			rgbd.constraints = RigidbodyConstraints2D.FreezeAll;
            playerAnimator.SetLayerWeight(4, 0);
            gameObject.layer = 8;
			//make a gap 
			if (gameObject.transform.position.x > temPosition.x) {
				gameObject.transform.position += addGap;
			} else if (gameObject.transform.position.x < temPosition.x) {
				gameObject.transform.position -= addGap;
			}
			
			
		}
	}

	private void playSoundEffect(){
		audioRock.clip = rockSound;
		audioRock.Play();
	}
	
	private void stopSoundEffect(){
		audioRock.Stop();
	}
	
	//no press key require
	private void pushController2(){
		if (arm.hasArm || arm.hasSecondArm)
		{
			rgbd.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
			gameObject.layer = 11;
		}
		if (arm.hasArm == false && arm.hasSecondArm == false)
		{
			rgbd.constraints = RigidbodyConstraints2D.FreezeAll;
			gameObject.layer = 8;
		}
	}

	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag == "wall") {
			gameObject.layer = 8;
            playerAnimator.SetLayerWeight(4, 0);
            //print (playerScript.velocity.x);
            if (temSpeed > 0) {
				playerScript.moveSpeed = -(temSpeed + 8);
			} else if (temSpeed < 0) {
				playerScript.moveSpeed = -(temSpeed - 8);
			}
			//playerScript.velocity.x =-20;
			//print (playerScript.moveSpeed);
			Destroy(GetComponent<Rigidbody2D>());
		}
	}
}

