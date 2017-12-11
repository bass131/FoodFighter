using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class titleScene : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler { 

    public AudioClip titleClip;
	AudioSource titleAudio;

    private float Delay = 3.0f;
    private float realTime = 0;

    bool TouchTrigger = false;

    private void Awake()
    {
        Screen.SetResolution(1280, 720, true);
    }
    // 인터페이스 트리거 관련
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log(transform.name);
        GoNextDown();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("MouseOver");
    }

    void Start()
	{
		titleAudio = gameObject.AddComponent<AudioSource> ();
		titleAudio.clip = titleClip;
		titleAudio.loop = false;
	}

    private void Update()
    {
        GoNext();
    }

    public void GoNext()
	{
        if (TouchManager.instance.touch.phase == TouchPhase.Ended)
        {
            titleAudio.Play();
            Invoke("Select", 2.0f);
        }
	}

   public void GoNextDown()
    {
        if (!TouchTrigger)
        {
            TouchTrigger = true;
            titleAudio.Play();
            Invoke("Select", 2.0f);
        }
    }

	void Select()
	{
		SceneManager.LoadScene ("Select");
		Debug.Log ("GO Stage");
	}
}
