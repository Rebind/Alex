using UnityEngine;
using System.Collections;

public class RangeIndicator : MonoBehaviour {
	public SwitchControl ControlScript;
	public GameObject player;
	public GameObject rangeIndicator;

	public GameObject target;
	public Vector3 targetPos;

	public float speed = 50.0f;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		rangeIndicator = GameObject.Find("RangeIndicator");
		ControlScript = player.GetComponent<SwitchControl>();
		target = ControlScript.ClosestLimb;
	}
	
	// Update is called once per frame
	void Update () {
		target = ControlScript.ClosestLimb;
		if (target == null) {
			rangeIndicator.GetComponent<Renderer>().enabled = false;
		} else {
			rangeIndicator.GetComponent<Renderer>().enabled = true;
			moveToClosest();
		}	
	}

	private void moveToClosest () {
        //rangeIndicator.SetActive(true);
        /*targetPos = new Vector3(target.transform.position.x, target.transform.position.y, 
		target.transform.position.z);
		transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);*/
        transform.position = target.transform.position;
    }
}
