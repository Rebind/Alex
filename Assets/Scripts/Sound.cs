using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	public AudioClip attachSound;
	public AudioClip detachSound;
	public AudioClip headRoll;
	public AudioClip torsoWalk;
	public AudioClip footsteps;
	public AudioClip feet;
	public AudioClip jump;

	public AudioSource audioAttach;
	public AudioSource audioDetach;
	public AudioSource audioHeadRoll;
	public AudioSource audioTorso;
	public AudioSource audioFoot;
	public AudioSource audioFeet;
	public AudioSource audioJump;
	public AudioSource[] playerMovementAudioSources;


	public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) { 
		AudioSource newAudio = gameObject.AddComponent<AudioSource>();
		newAudio.clip = clip; 
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol; 
		return newAudio; 
	}

	public void Awake(){
		// add the necessary AudioSources:
		audioAttach = AddAudio(attachSound, false, true, 0.5f);
		audioDetach = AddAudio(detachSound, false, true, 0.5f);
		audioHeadRoll = AddAudio(headRoll, false, true, 2.0f); 
		audioTorso = AddAudio(torsoWalk, false, true, 0.2f); 
		audioFoot = AddAudio(footsteps, true, true, 0.1f);
		//audioFoot.pitch = 0.35f;
		audioFeet = AddAudio(feet, true, true, 3.0f);
		audioJump = AddAudio(jump, true, true, 5.0f);

		playerMovementAudioSources = new AudioSource[4] { audioHeadRoll, audioTorso, audioFeet, audioFoot };
	} 
}