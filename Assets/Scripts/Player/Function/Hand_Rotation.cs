using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Rotation : MonoBehaviour {

    public PlayerCRTL Player;

    private Quaternion None;
    private Quaternion z_raw_1;
    private Quaternion z_raw_2;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerCRTL>();
		
	}
	
	// Update is called once per frame
	void Update () {
        H_Rotate();
	}

    void H_Rotate()
    {
        None = Quaternion.identity;
        z_raw_1.eulerAngles = new Vector3(0, 0, 35);
        z_raw_2.eulerAngles = new Vector3(0, 0, -35);
        if (Player.ImageVec == 1)
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, z_raw_2, Time.deltaTime * 5.0f);
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, z_raw_1, Time.deltaTime * 5.0f);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, None, Time.deltaTime * 5.0f);
            }
        }
        else if(Player.ImageVec == -1)
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, z_raw_1, Time.deltaTime * 5.0f);
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, z_raw_2, Time.deltaTime * 5.0f);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, None, Time.deltaTime * 5.0f);
            }
        }
    }
}
