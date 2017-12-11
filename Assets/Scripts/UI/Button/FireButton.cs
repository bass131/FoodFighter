using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour
{

    public Animator Fire;

    public void FireButtonAnim()
    {
        Fire.SetTrigger("Push");
    }
}
