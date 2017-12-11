using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Scene_CTRL : MonoBehaviour {

	public AudioClip logoClip;
	AudioSource logoAudio;

	private float Realtime;
	private float Scenetime = 2.5f;

    bool play = false;

	// Use this for initialization
	void Start () {
		logoAudio = gameObject.AddComponent<AudioSource> ();
		logoAudio.clip = logoClip;
		logoAudio.Play ();
	
	}
	
	// Update is called once per frame
	void Update () {
        if (play == false)
        {
            Invoke("LoadScene", 4.0f);
            play = true;
        }
	}

    void LoadScene()
    {
        SceneManager.LoadScene("MainTitle");
    }
}
