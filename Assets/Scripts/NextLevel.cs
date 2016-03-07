using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

	public Loading loading;
	public GameObject player;
	public LevelManager lvlmanager;
	//private LoadingScene nextScene;
	// Use this for initialization
	void Start () {
		 player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(this.transform.position, player.transform.position) <= 4.0f){
			Debug.Log("in exit");
			nextLevel();
			
		}
	}
	
	private void nextLevel(){
		string currLevel =  Application.loadedLevelName;
		Debug.Log("String: " + currLevel);
		
		switch (currLevel) {
			case "Showcase":
				PlayerPrefs.SetInt("Level", 1);
				Application.LoadLevel("LoadingScene");
				
				//Application.LoadLevel(1);
				break;
			case "AlexFerr2DLevel":
				PlayerPrefs.SetInt("Level", 4);
				Application.LoadLevel("LoadingScene");
				//Application.LoadLevel(4);
				break;
			case "BeggLevel":
				PlayerPrefs.SetInt("Level", 4);
				Application.LoadLevel("LoadingScene");
				break;
			default:
				print ("Incorrect intelligence level.");
				break;
		
		}
	}
}
