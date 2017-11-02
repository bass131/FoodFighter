using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
 
    public static GameManager Instance
    {
        get
        {
            return Instance;
        }
        
    }
    //---------------------------------------
    public static GameManager instance = null;
    //---------------------------------------

    public int HighScore = 0;

    public bool IsPaused = false;

    public bool InputAllowed = true;


    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
