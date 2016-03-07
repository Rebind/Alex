using UnityEngine;
 using System.Collections;
 
 public class Loading : MonoBehaviour
 {
     // Set this in inspector.
     public Texture2D emptyProgressBar;
     public Texture2D fullProgressBar;
     public int LevelName;
     public Rect position = new Rect(0, 0, 100, 50);
     public bool fullScreen = true;
     public bool debug = true;
 
     // When assigned, load is in progress.
     private AsyncOperation async = null;
 
     IEnumerator Start()
     {
		LevelName = PlayerPrefs.GetInt("Level");
		//getLevelName();
		Debug.Log("Level Name: " + LevelName);
        dbg("Level '{0}' loading...", LevelName);
		
        yield return async = Application.LoadLevelAsync(LevelName);
     }
 
	public void  setLevelName(string lvlName){
		//this.LevelName = lvlName;
		
		//return lvlName;
	}
	
	public void getLevelName(){
		//LevelName = lvlName;
		//return lvlName;
	}
 
     void OnGUI()
     {
         if (fullScreen)
             ProgressBar(0, 0, Screen.width, Screen.height);
         else
             ProgressBar(position.x, position.y, 
                 position.width, position.height);
     }
 
     void ProgressBar(float x, float y, float width, float height)
     {
         if (async == null)
             return;
 
         if (Event.current.type == EventType.Repaint)
             dbg("Level progress: {0:N0}%", async.progress * 100f);
 
         //GUI.DrawTexture(new Rect(x, y, width, height), emptyProgressBar);
         GUI.DrawTexture(new Rect(x, y, width * async.progress, height), fullProgressBar);
         GUI.skin.label.alignment = TextAnchor.MiddleCenter;
         //GUI.Label(new Rect(x, y, width, height), string.Format("{0:N0}%", async.progress * 100f
		 GUI.Label(new Rect(x, y, width, height), string.Format("{0:N0}", "LOADING...."));
     }
 
     void dbg(string fmt, params object[] args)
     {
         if (debug)
             print(string.Format(fmt, args));
     }
 }