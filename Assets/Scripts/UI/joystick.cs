using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystick : MonoBehaviour
{

    public RectTransform rect;
    private PlayerCRTL Player;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerCRTL>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void move()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Stationary && rect.transform.position.x >35);
        {
            Player.Player_R();
        }
    }
}
