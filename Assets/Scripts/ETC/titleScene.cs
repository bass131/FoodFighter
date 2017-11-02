using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class titleScene : MonoBehaviour {


	public AudioClip titleClip;
	AudioSource titleAudio;

    private float Delay = 3.0f;
    private float realTime = 0;

	void Start()
	{
		titleAudio = gameObject.AddComponent<AudioSource> ();
		titleAudio.clip = titleClip;
		titleAudio.loop = false;



	}

	void Update()
	{
        realTime += Time.deltaTime;
		if (Input.anyKeyDown && realTime > Delay) 
		{
			
			titleAudio.Play ();
			Invoke ("enterStage", 1.6f);

		}
	}
	void enterStage()
	{
		SceneManager.LoadScene ("LoadingScene");
		Debug.Log ("GO Stage");
	}
}
