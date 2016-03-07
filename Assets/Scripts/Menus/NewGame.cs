using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewGame : MonoBehaviour {

	string[] buttons = new string[3] {"Start", "Level Select", "Exit"};
 
	int selected = 0;
	bool inputVertical = false;
	bool userHasHitReturn = false;
	
	public Texture2D resumeButton; 
	public Texture2D quitButton; 
	
	public Texture2D playButton;
	
	
	public bool clickedStart = false;
 
	private AsyncOperation async = null; // When assigned, load is in progress.
	private LevelManager lvlmanager;
	private Loading loading;

 
	int menuSelection (string[] buttonsArray, int selectedItem, string direction) {
 
		if (direction == "up") {
 
			if (selectedItem == 0) {
 
				selectedItem = buttonsArray.Length - 1;
 
			} else {
 
			selectedItem -= 1;
 
			}
	
		}
 
		if (direction == "down") {
 
			if (selectedItem == buttonsArray.Length - 1) {
 
				selectedItem = 0;
 
			} else {
 
				selectedItem += 1;
 
			}
 
		}
 
		return selectedItem;
 
	}
 
	void Update(){
 
		//StartCoroutine(LoadALevel("Showcase"));
		
		
		if(Input.GetAxisRaw("Vertical") == 1){
			if(inputVertical == false){
				selected = menuSelection(buttons, selected, "up");
			}
			inputVertical = true;
 
		}
 
		if(Input.GetAxisRaw("Vertical") == -1){
			if(inputVertical == false){
				selected = menuSelection(buttons, selected, "down");
			}
			inputVertical = true;
 
		}
		
		if(Input.GetAxisRaw("Vertical") == 0){
			inputVertical = false;
		}
 
	}
 

	IEnumerator Start() {
		async = Application.LoadLevelAsync("Showcase");
		async.allowSceneActivation = false;
		yield return async;
	}
	
	void OnGUI(){
		//GUI.DrawTexture(new Rect(500, 200, 100, 50), emptyProgressBar);
		
		GUI.SetNextControlName(buttons[0]);
		Event e = Event.current;
       
		
		if(GUI.Button(new Rect(Screen.width/2,250,100,50), playButton )){
 
			//when selected Start button
			//Application.LoadLevel("Showcase");
			Debug.Log("Clicked Start");
 
		}
		
		if(GUI.GetNameOfFocusedControl() == "Start" && e.keyCode == KeyCode.Return){
			
			//StartCoroutine(LoadALevel ("Showcase"));
			clickedStart = true;
			Application.LoadLevel("LoadingScene");
			loading.setLevelName(lvlmanager.Levels[1]);
		}
 
		GUI.SetNextControlName(buttons[1]);
 
		if(GUI.Button(new Rect(Screen.width/2,320,100,50), resumeButton)){
 
			//when selected Options button
			Debug.Log("Clicked Options");
 
		}
 
		//Players select the levels they want
		if(GUI.GetNameOfFocusedControl() == "Level Select" && e.keyCode == KeyCode.Return){
			
			//StartCoroutine(LoadALevel ("Showcase"));
			//clickedStart = true;
			//Application.LoadLevel("LoadingScene");
			//loading.getLevelName(lvlmanager.Levels[1]);
		}
		GUI.SetNextControlName(buttons[2]);
 
		if(GUI.Button(new Rect(Screen.width/2,390,100,50), quitButton)){
 
			//when selected Exit button
			Debug.Log("Exit");
 
		}
 
		if(GUI.GetNameOfFocusedControl() == "Exit" && e.keyCode == KeyCode.Return){
			
			//StartCoroutine(LoadALevel ("Showcase"));
			//clickedStart = true;
			//Application.LoadLevel("LoadingScene");
			//loading.getLevelName(lvlmanager.Levels[1]);
		}
		GUI.FocusControl(buttons[selected]);
 
	}
}
