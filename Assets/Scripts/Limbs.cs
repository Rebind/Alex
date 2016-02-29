using UnityEngine;
using System.Collections;

public class Limbs : MonoBehaviour {

    private Rigidbody2D myRigidLimb;
    private Animator myAnim;
    private CameraFollow camScript;
    private Controller2D limb;
    private GameObject myGO;


    // Use this for initialization
    void Start () {
        myGO = GameObject.FindGameObjectWithTag("MainCamera");
        camScript = myGO.GetComponent<CameraFollow>();
        myRigidLimb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

     
        
	
	}
	
	// Update is called once per frame
	void Update () {

        limb = camScript.target;
 
        if (limb.name == gameObject.name)
        {
            myAnim.SetBool("active", true);
            float horizontal = Input.GetAxis("Horizontal");
            myAnim.SetFloat("spped", Mathf.Abs(horizontal));
        }
        else
        {
            myAnim.SetFloat("spped", 0);
            myAnim.SetBool("active", false);

        }

    }
}
