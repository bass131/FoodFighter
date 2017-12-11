using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene2 : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Invoke("SceneEnter", 8.0f);
    }

    void SceneEnter()
    {
        SceneManager.LoadScene("Stage2");
    }
}
