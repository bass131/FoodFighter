using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Scene_CTRL : MonoBehaviour {

	public AudioClip logoClip;
	AudioSource logoAudio;

	private float Realtime;
	private float Scenetime = 2.5f;

	// Use this for initialization
	void Start () {
		logoAudio = gameObject.AddComponent<AudioSource> ();
		logoAudio.clip = logoClip;
		logoAudio.Play ();
	
	}
	
	// Update is called once per frame
	void Update () {
		Realtime += Time.deltaTime;


		if (Scenetime < Realtime) {
			SceneManager.LoadScene ("Title");
		}
	}
}
