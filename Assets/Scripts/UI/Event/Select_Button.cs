using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class Select_Button : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{

    public AudioClip buttonSeClip; // 피격 사운드 클립.
    AudioSource buttonSeAudio; // 피격 사운드 오디오.

    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log(transform.name);
    }

    public void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("MouseOver");
    }
    // Use this for initialization
    void Awake () {
        Screen.SetResolution(1280, 720, true);

        buttonSeAudio = gameObject.AddComponent<AudioSource>();
        buttonSeAudio.clip = buttonSeClip; // 아이템 획득 사운드 클립 초기화.
        buttonSeAudio.loop = false; // 아이템 획득 사운드 루프  = false.
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartButton()
    {
        buttonSeAudio.Play();
        Invoke("getStart",1.5f);
    }

    public void ExitButton()
    {
        buttonSeAudio.Play();
        Invoke("quit", 1.5f);
    }

    void getStart()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    void quit()
    {
        Application.Quit();
    }
}
