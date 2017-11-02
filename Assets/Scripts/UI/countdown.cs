using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class countdown : MonoBehaviour
{
    private float time = 30 * (60); // 30분.
    public Text scoreText;
    void Update()
    {
        time -= 1 * Time.deltaTime;

        int min = (int)(time / 60);
        int sec = (int)(time % 60);
        scoreText.text =""+ min ;
    }

}
