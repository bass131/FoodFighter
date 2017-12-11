using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class UI_Manager : MonoBehaviour {

    public static UI_Manager instance = null;  //Static instance of GameManager which allows it to be accessed by any other script.

    public Player Players;

    public GameObject Result;
    public GameObject Panel;
    public GameObject Movement;
    public GameObject EventUI;

    public GameObject Caution;

    public Text scoreText; //점수를 표시하는 Text객체를 에디터에서 받아옵니다.
    public Text Result_Score;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        Players = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void AddScore(int num) //점수를 추가해주는 함수를 만들어 줍니다.
    {
        StageManager.instance.Score += num; //점수를 더해줍니다.
        scoreText.text = "Score : " + StageManager.instance.Score;
    }

    public void ResultScore()
    {
        Result_Score.text = "" + StageManager.instance.Score;
    }

    // Update is called once per frame
    void Update () {
        Results();
	}

    void Results() // 결과창 표시.
    {
        if (StageManager.instance.Stage_All_Clear == true || Players.Die == true)
        {
            ResultScore();
            Result.SetActive(true);
            Panel.SetActive(true);
            Movement.SetActive(false);
            EventUI.SetActive(false);
        }
        else
        {
            Result.SetActive(false);
            Panel.SetActive(false);
        }
    }

    public void NextButton()
    {
        SceneManager.LoadScene("LoadingScene_2");
    }

    public void ExitButton()
    {
        StageManager.instance.StageReset();
        Application.Quit();
    }

    public void RestartButton()
    {
        StageManager.instance.StageReset();
        SceneManager.LoadScene("Stage2");
        Time.timeScale = 1.0f;
    }
}
