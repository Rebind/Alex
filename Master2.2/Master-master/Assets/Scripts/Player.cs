using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
	public bool enabled;

	private float maxJumpHeight =4;
	private float minJumpHeight =0;

    private float timeToJumpApex = .4f;

    public float moveSpeed;
    private bool facingRight;
	private bool jumpFacing;
    private float gravity;

	private float oldMoveSpeed;

    private float minJumpVelocity;
	private float maxJumpVelocity;

    public Vector3 velocity;
    private BoxCollider2D myBoxcollider;
    private Controller2D myController;
    private Animator myAnimator;
   // private Animator limbAnimator;
    private bool canJump;
    private int state;
	public LayerMask layer;

    private CameraFollow camScript;
    private Controller2D myTarget;
    private GameObject myGO;
    private Boolean canBump1;
    private Boolean canBump2;

	public bool isJumping;





    void Start()
    {
        myGO = GameObject.FindGameObjectWithTag("MainCamera");
        camScript = myGO.GetComponent<CameraFollow>();
        moveSpeed = 10f;
        facingRight = true;
        myBoxcollider = gameObject.GetComponent<BoxCollider2D>() as BoxCollider2D;
        myAnimator = GetComponent<Animator>();
        //limbAnimator = GetComponent<Animator>("Arm");
        myController = GetComponent<Controller2D>();
		print ("Gravity: " + gravity + "  Jump Velocity: " + maxJumpVelocity);
    }

   

    void Update()
    {
        myTarget = camScript.target;
		if(enabled){
        state = myAnimator.GetInteger("state");
        HandleMovments();
        Flip();
        HandleJumps();
        handleBodyCollisions();
        handleBuffsDebuffs();
		//pushBox ();
		}
    }

    private void HandleMovments()
    {
        if (myController.collisions.above || myController.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //get input from the player (left and Right Keys)

        if (Input.GetKeyDown(KeyCode.Space) && myController.collisions.below && myAnimator.GetInteger("state") != 0)  //if spacebar is pressed, jump
        {
			isJumping = true;
			jumpFacing = facingRight;
            velocity.y = maxJumpVelocity;

        }
		if(Input.GetKeyUp(KeyCode.Space)){
			isJumping = false;
			if(velocity.y > minJumpVelocity){
				velocity.y = minJumpVelocity;
			}
		}
        velocity.x = input.x * moveSpeed;

        velocity.y += gravity * Time.deltaTime;
        myController.Move(velocity * Time.deltaTime);
        //Debug.Log("target = "+ myTarget.name);
        //Debug.Log("gameobject = " + gameObject.name);
        if (myTarget.name == this.gameObject.name)
        {

            //Debug.Log("lol");

            myAnimator.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        }
        else
        {
            //Debug.Log("gg");
            myAnimator.SetFloat("speed", 0);
        }
        //myAnimator.SetFloat("sppeed", Mathf.Abs(Input.GetAxis("Horizontal")));
       /* if (myAnimator.GetFloat("speed") != 0)
        {
            myAnimator.SetBool("isMoving", true);
        }
        else
        {
            myAnimator.SetBool("isMoving", false);
        }*/
    }

    private void handleBuffsDebuffs()
    {
		if (isJumping && (jumpFacing != facingRight)) {
			moveSpeed = 4f;

		}
        else if (state == 1 || state == 2 || state == 3)
        {
            moveSpeed = 5f;
          //  jumpHeight = 3f;
        }
        else if (state == 4 || state == 6 || state == 8)
        {
            moveSpeed = 7.5f;
           // jumpHeight = 6f;
        }
        else if (state == 7 || state == 5 || state == 9)
        {
            moveSpeed = 12.5f;
           // jumpHeight = 9f;
        }
        else
        {
            moveSpeed = 10f;
        }
    }

    private void Flip()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0 && !facingRight || horizontal<0 && facingRight)
         {
			if (isJumping) {
				jumpFacing = !facingRight;
			}
			if (!isJumping) {
				facingRight = !facingRight;
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
			}
          }
        }

    private void HandleJumps()
    {
        if (state == 0)
        {
            maxJumpHeight = 1;
        }
        else if (state == 1 || state == 2 || state == 3)
        {
            maxJumpHeight = 2;
        }
        else if (state == 4 || state == 6 || state == 8)
        {
            maxJumpHeight = 4;
        }
        else if (state == 5 || state == 7 || state == 9)
        {
            maxJumpHeight = 7;
        }
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    private void handleBodyCollisions()
    {
        //Debug.Log(myTarget.name);

        if (state == 1 || state == 2 || state == 3)
        {
            canBump1 = true;
        }
        if (state == 1)
        {
            canBump2 = true;
        }

        if (myAnimator.GetInteger("state") == 0)
        {
			changeBoxCollider (2.2f, 2.1f, 0f, 1f);
			myController.CalculateRaySpacing ();

        }
        else if(myAnimator.GetInteger("state") == 1)
        {
			changeBoxCollider (2.2f, 3.32f, -0.09f, 1.49f);
			myController.CalculateRaySpacing ();
        }
         else if (myAnimator.GetInteger("state") == 2)
         {
            if (canBump2)
            {
                Vector3 temp = new Vector3(0, 1f, 0);
                gameObject.transform.position += temp;
                canBump2 = false;
            }
            changeBoxCollider (3.45f, 2.27f, 0.48f, 0.07f);
			myController.CalculateRaySpacing ();

        }
         else if (myAnimator.GetInteger("state") == 3)
         {

            changeBoxCollider(3.45f, 2.27f, 0.48f, 0.07f);
            myController.CalculateRaySpacing ();

        }
         else if (myAnimator.GetInteger("state") == 4)
         {
            if (canBump1)
            {
                Vector3 temp = new Vector3(0, 2f, 0);
                gameObject.transform.position += temp;
                canBump1 = false;
            }
            changeBoxCollider (2.22f,4.25f, -0.08f, 0f);
			myController.CalculateRaySpacing ();

        }
         else if (myAnimator.GetInteger("state") == 5)
         {
            changeBoxCollider(2.22f, 4.25f, -0.08f, 0f);
            myController.CalculateRaySpacing ();
        }
         else if (myAnimator.GetInteger("state") == 6)
         {
            if (canBump1)
            {
                Vector3 temp = new Vector3(0, 2f, 0);
                gameObject.transform.position += temp;
                canBump1 = false;
            }
            changeBoxCollider(2.22f, 4.25f, -0.09f, 0f);
            myController.CalculateRaySpacing ();
        }
         else if (myAnimator.GetInteger("state") == 7)
         {

            changeBoxCollider(2.22f, 4.25f, -0.08f, 0f);
            myController.CalculateRaySpacing ();
        }
        else if (myAnimator.GetInteger("state") == 8)
         {
            if (canBump1)
            {
                Vector3 temp = new Vector3(0, 2f, 0);
                gameObject.transform.position += temp;
                canBump1 = false;
            }
            changeBoxCollider(2.22f, 4.25f, -0.08f, 0f);
            myController.CalculateRaySpacing ();
        }
        else if (myAnimator.GetInteger("state") == 9)
         {
            changeBoxCollider(2.22f, 4.25f, -0.08f, 0f);
            myController.CalculateRaySpacing ();
        }
        if (myTarget.name == "Arm" || myTarget.name == "Arm (1)")
        {
            changeBoxCollider(1.53f, 0.49f, -0.02f, -.2f);
            myController.CalculateRaySpacing();
        }
        if (myTarget.name == "Leg" || myTarget.name == "Leg (1)")
        {
            changeBoxCollider(0.61f, 1.21f, 0.16f, -0.65f);
            myController.CalculateRaySpacing();
        }
    }



	private void changeBoxCollider(float xSize, float ySize, float xOffset, float yOffset){

		myBoxcollider.size = new Vector2(xSize,ySize);
		myBoxcollider.offset = new Vector2(xOffset,yOffset);

	}

    private void helperBoxCollider(float someFloat, string type, string var)
    {
        if (type.Equals("size"))
        {
            Vector3 size = myBoxcollider.size;
            if (var.Equals("x"))
            {
                size.x = someFloat;
            }
            else if (var.Equals("y"))
            {
                size.y = someFloat;
            }
            myBoxcollider.size = size;
        }
        else if (type.Equals("offset"))
        {
            Vector3 offset = myBoxcollider.offset;
            if (var.Equals("x"))
            {
                offset.x = someFloat;
            }
            else if (var.Equals("y"))
            {
                offset.y = someFloat;
            }
            myBoxcollider.offset = offset;
        }
    }

	//control the collision mask
	private void pushBox(){
		if (Input.GetKeyDown(KeyCode.H)) //&& (arm.hasArm || arm.hasSecondArm))
		{
			myController.collisionMask.value = -3640;
			//Debug.Log(myController.collisionMask.value);
		}
		if (Input.GetKeyUp(KeyCode.H))
		{
			myController.collisionMask.value = -1592;
		}
	}

}
