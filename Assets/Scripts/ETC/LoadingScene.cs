using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Invoke ("SceneEnter", 8.0f);
	
	}

	void SceneEnter()
	{
		SceneManager.LoadScene ("Stage");
	}
}
