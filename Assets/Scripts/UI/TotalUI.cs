using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalUI : MonoBehaviour {

    private PlayerCRTL Player;
    public GUITexture ScoreBoard;
    public Transform trScoreBoard;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerCRTL>();
    }

    private void Update()
    {
        if (Player.isEnd == true)
        {
            Instantiate(ScoreBoard, trScoreBoard.position, trScoreBoard.rotation);
        }
    }
}
