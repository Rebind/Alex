using UnityEngine;
using System.Collections;

public class FollowerScript : MonoBehaviour {
	public SwitchControl ControlScript;
	public GameObject tmp;

	public GameObject target;
	public Vector3 targetPos;

	public float speed = 50.0f;
	// Use this for initialization
	void Start () {
		tmp = GameObject.Find("Player");
		ControlScript = tmp.GetComponent<SwitchControl>();
		target = ControlScript.inControl;
	}
	
	// Update is called once per frame
	void Update () {
		target = ControlScript.inControl;
		targetPos = new Vector3(target.transform.position.x, target.transform.position.y + 5, 
			target.transform.position.z);
		transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
	}
}
