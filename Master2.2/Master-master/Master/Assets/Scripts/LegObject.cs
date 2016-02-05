/*
 * 
 * This is the leg object. Check the conditions of the
 * attachment and detachment of the leg object. 
 * 
 * 
 * 
 * */

using UnityEngine;
using System.Collections;

public class LegObject : MonoBehaviour
{


    //Create sprites here
    public bool oneLeg;
    public bool twoLegs;

    public float legCount;
    public Arms arm;
    public Torso body;
    public bool collideLegObj;

    public SpriteRenderer sprRend;
    public Sprite oneLegSpr;
    public Sprite twoLegsSpr;
    public Sprite oneArmOneLeg;
    public Sprite oneArmTwoLegs;
    public Sprite twoArmsOneLeg;
    public Sprite twoArmsTwoLegs;
    public Sprite torsoOnly;

    bool nearLeg;
    public GameObject[] goWithTag;
    public GameObject legObj;
    public GameObject[] allLegs;
    Transform player;
	private int trackOfLegs;

    Vector3 pos;

    // Use this for initialization
    void Start()
    {
		trackOfLegs = 0;
        arm = GetComponent<Arms>();
        body = GetComponent<Torso>();
        oneLeg = false;
        twoLegs = false;
        legCount = 0;
        player = GameObject.Find("Player").transform;
        sprRend = GetComponent<SpriteRenderer>();
        goWithTag = new GameObject[100];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && nearLeg && legCount != 2)
        {
            attachLeg();
			goWithTag[trackOfLegs] = legObj;
			trackOfLegs++;
		
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            detach();
        }
        changeSprites();
        nearLeg = CheckCloseToTag("leg", 3.0f);
    }


    //Drawing the different sprites according to the different parts
    void changeSprites()
    {
        //no arm, no legs
        if (arm.armCount == 0 && legCount == 0 && body.hasTorso)
        {
            sprRend.sprite = torsoOnly;
        }
        //no arm, one leg
        if (arm.armCount == 0 && legCount == 1)
        {
            sprRend.sprite = oneLegSpr;
        }
        //no arm, two legs
        if (arm.armCount == 0 && legCount == 2)
        {
            sprRend.sprite = twoLegsSpr;
        }
        //only one arm
        if (arm.armCount == 0 && legCount == 1)
        {
            sprRend.sprite = oneLegSpr;
        }
        //two Arms and no leg
        if (arm.armCount == 0 && legCount == 2)
        {
            sprRend.sprite = twoLegsSpr;
        }
        //one arm and one leg
        if (arm.armCount == 1 && legCount == 1)
        {
            sprRend.sprite = oneArmOneLeg;
        }
        //one arm and two legs
        if (arm.armCount == 1 && legCount == 2)
        {
            sprRend.sprite = oneArmTwoLegs;
        }
        //two arms and one leg
        if (arm.armCount == 2 && legCount == 1)
        {
            sprRend.sprite = twoArmsOneLeg;
        }
        //two arms and two legs
        if (arm.armCount == 2 && legCount == 2)
        {
            sprRend.sprite = twoArmsTwoLegs;
        }
    }


    void CreateLegObj()
    {
			pos = player.position;
			legObj.SetActive (true);
			Instantiate (legObj, pos, Quaternion.Euler (0f, 0f, 0f));
			Destroy (legObj);
		
    }

    //Checking if the player is near the leg body part
    bool CheckCloseToTag(string tag, float minimumDistance)
    {
        allLegs = GameObject.FindGameObjectsWithTag("leg");

        for (int i = 0; i < allLegs.Length; ++i)
        {
            if (Vector3.Distance(transform.position, allLegs[i].transform.position) <= minimumDistance)
            {
                legObj = allLegs[i];
                //goWithTag[i] = allLegs[i];
                Debug.Log("Press X to pick up. Leg");
                return true;
            }
        }
        return false;
    }


    //attaching leg object
    void attachLeg()
    {
        if (body.hasTorso && arm.armCount == 0 && (legCount == 0))
        {
            //change sprite to head with torso and one leg
            //change movement/jump speed
            legCount = 1;
            oneLeg = true;
			legObj.SetActive(false);
        }
        else if (body.hasTorso && (legCount == 1) && arm.armCount == 0)
        {
            //change sprite to head with torso and two legs
            //change movement/jump speed
            legCount = 2;
            twoLegs = true;
            oneLeg = false;
			legObj.SetActive(false);
        }
        //Attach leg
        else if (body.hasTorso && (legCount == 0) && arm.armCount == 2)
        {
            //change sprite to head with torso two arm and one leg
            //change movement/jump speed
            legCount = 1;
            oneLeg = true;
            legObj.SetActive(false);
        }
        else if (body.hasTorso && (legCount == 1) && arm.armCount == 1)
        {
            //change sprite to head with torso one arm and one leg
            //change movement/jump speed
            legCount = 2;
            oneLeg = true;
            legObj.SetActive(false);
        }
        //Attach leg
        else if (body.hasTorso && (legCount == 1) && arm.armCount == 2)
        {
            //change sprite to head with torso two arms and two legs
            //change movement/jump speed
            legCount = 2;
            oneLeg = true;
            legObj.SetActive(false);
        }
        else if (body.hasTorso && (legCount == 0) && arm.armCount == 1)
        {
            //change sprite to head with torso one arm and one leg
            //change movement/jump speed
            legCount = 1;
            oneLeg = true;
            legObj.SetActive(false);
        }

    }

    void detach()
    {
        //Detach one leg
        if (body.hasTorso && arm.armCount == 0 && legCount == 1)
        {
            //Change sprite to head with torso, no arms, no legs
            //change movement or jump speed
            oneLeg = false;
			CreateLegObj();
            legCount = 0;
            

        }
        //Detach one leg
        else if (body.hasTorso && arm.armCount == 0 && legCount == 2)
        {
            //Change sprite to head with torso with one leg
            //change movement or jump speed
            twoLegs = false;
            CreateLegObj();
            legCount = 1;
        }
        else if (body.hasTorso && arm.armCount == 2 && legCount == 1)
        {
            //Change sprite to head with torso and two arms
            //change movement or jump speed
            oneLeg = false;
			CreateLegObj();
            legCount = 0;
            
        }
        else if (body.hasTorso && arm.armCount == 1 && legCount == 1)
        {
            //Change sprite to head with torso and an arm
            //change movement or jump speed
            oneLeg = false;
			CreateLegObj();
            legCount = 0;
            
        }
        //Detach one leg
        else if (body.hasTorso && arm.armCount == 2 && legCount == 1)
        {
            //Change sprite to head with torso and two arms
            //change movement or jump speed
            oneLeg = false;
			CreateLegObj();
            legCount = 0;
            
        }
        else if (body.hasTorso && arm.armCount == 2 && legCount == 2)
        {
            //Change sprite to head with torso, 2 arms, 1 leg
            //change movement or jump speed
            oneLeg = false;
			CreateLegObj();
            legCount = 1;
            
        }
    }
}
