using UnityEngine;
using System.Collections;

//using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private SwitchControl ControlScript;

    private GameObject player;
    private GameObject Wreck;
    private GameObject breakable;

    public GameObject deathParticle;
	public GameObject exit;
    public GameObject WreckParticle;
    private GameObject[] Legs;
    private GameObject[] Arms;
    Animator playerAnimator;
    float minimumDistance = 10.5f;
    private LinkedList Limbs;
	private PressurePlateController pp;
	public string [] Levels;
	private Loading loading;
	
    public class Node {
        public Node next;
        public Node last;
        public GameObject data;
        public Vector3 position;
    }
    //LinkedList data structure to hold gameobjects and position
    public class LinkedList
    {
        private Node head;
        private Node last;
        //attach a new gameobject to the last element
        public void append(GameObject tmp) {
            Node toAdd = new Node();
            toAdd.data = tmp;
            toAdd.position = tmp.transform.position;
            if (head == null) {
                head = toAdd;
                last = toAdd;
                toAdd.next = null;
            } else {
                last.next = toAdd;
                last = toAdd;
                toAdd.next = null;
            }
        }
        //find the position of a game object
        public Vector3 getPosition(GameObject tmp) {
            Node iter = head;
            Vector3 position = Vector3.zero;
            while(head != null) {
                if(iter.data == tmp) {
                    position = iter.position;
                    return position;
                } else {
                    iter = iter.next;
                }
            }
            return position;
        }
    }

    public float respawnDelay;
    // Use this for initialization
    void Start()
    {
        Limbs = new LinkedList();

        player = GameObject.Find("Player");
        Wreck = GameObject.Find("BreakTerrain");
		exit = GameObject.Find("Exit");
        breakable = GameObject.FindGameObjectWithTag("breakable");
        ControlScript = player.GetComponent<SwitchControl>();
        //find all objects with tags "leg" and "arm"
        Legs = GameObject.FindGameObjectsWithTag("leg");
        Arms = GameObject.FindGameObjectsWithTag("arm");
        playerAnimator = player.GetComponent<Animator>();

		Levels = new string[]{"AlexFerr2DLevel", "Showcase"};
        for (int i = 0; i < Legs.Length; i++) 
        {
            Limbs.append(Legs[i]);
        }

        for (int i = 0; i < Arms.Length; i++) 
        {
            Limbs.append(Arms[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
		
        if (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("Xbox_XButton"))
        {
            playerAnimator.SetTrigger("attack");
        }
        if(Vector3.Distance(breakable.transform.position, player.transform.position) <= minimumDistance)
        {
            playerAnimator.SetLayerWeight(5, 1);
        }
        else{
            playerAnimator.SetLayerWeight(5, 0);
        }
		
		
    }

    public void respawnPlayer()
    {
        StartCoroutine("respawnPlayerCo");

    }

    public void destroyWall()
    {
        Wreck.SetActive(false);
        Instantiate(WreckParticle, Wreck.transform.position, Wreck.transform.rotation);
    }

    public IEnumerator respawnPlayerCo()
    {
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        player.SetActive(false);
        player.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(respawnDelay);
        Application.LoadLevel(Application.loadedLevel);
        yield return new WaitForSeconds(respawnDelay);
        player.SetActive(true);
        player.GetComponent<Renderer>().enabled = true;
        
        //Instantiate(respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
        //player.transform.position = currentCheckpoint.transform.position;
    }

    public void respawnLimb(string target)
    {
        GameObject tmp = GameObject.Find(target);
        Instantiate(deathParticle, tmp.transform.position, tmp.transform.rotation);
        tmp.transform.position = Limbs.getPosition(tmp);
        ControlScript.switchToHead();
    }
	
	
	

}