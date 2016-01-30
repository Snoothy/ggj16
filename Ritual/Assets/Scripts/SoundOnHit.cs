using UnityEngine;
using System.Collections;

public class SoundOnHit : MonoBehaviour {

	AudioSource audio;
	bool firstPlay = true;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other){
		if (other.transform.tag != "GrabFix" && other.transform.tag != "Player"  && !audio.isPlaying && !firstPlay) {
			Debug.Log (other.transform.name);

			audio.Play ();
		}
		firstPlay = false;
	}
}
