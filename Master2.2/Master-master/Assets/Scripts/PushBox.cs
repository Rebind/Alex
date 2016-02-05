using UnityEngine;
using System.Collections;

public class PushBox : MonoBehaviour
{
    private GameObject Player;
    private MergeAttachDetach arm;
    private Rigidbody2D rgbd;
    private Animator myAnimator;
	private Vector3 addGap = new Vector3(.3f,0,0);
	private Vector3 temPosition;
	public Player playerScript;

    void Start()
    {
        Player = GameObject.Find("Player");
        arm = Player.GetComponent<MergeAttachDetach>();
        rgbd = GetComponent<Rigidbody2D>();

		playerScript = Player.GetComponent<Player>();
    }

    void Update()
    {
        //Debug.Log(gameObject.transform.position.x);
		pushController();
    }

	private void pushController(){
		if (Input.GetKeyDown(KeyCode.H) && (arm.hasArm || arm.hasSecondArm))
		{
			//get the position when player press "h"
			temPosition = gameObject.transform.position;
			rgbd.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
			gameObject.layer = 11;
		}
		if (Input.GetKeyUp(KeyCode.H) && (arm.hasArm || arm.hasSecondArm))
		{
			rgbd.constraints = RigidbodyConstraints2D.FreezeAll;
			gameObject.layer = 8;
			//make a gap 
			if (gameObject.transform.position.x > temPosition.x) {
				gameObject.transform.position += addGap;
			} else if (gameObject.transform.position.x < temPosition.x) {
				gameObject.transform.position -= addGap;
			}
		}
	}

	//no press key requise
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
			//print (playerScript.velocity.x);
			if (playerScript.moveSpeed > 0) {
				playerScript.moveSpeed = -(playerScript.moveSpeed + 7);
			} else if (playerScript.moveSpeed < 0) {
				playerScript.moveSpeed = -(playerScript.moveSpeed - 7);
			}
			//playerScript.velocity.x =-20;
			//print (playerScript.moveSpeed);
			Destroy(GetComponent<Rigidbody2D>());
		}
	}
}

