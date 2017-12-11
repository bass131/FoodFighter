using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public static TouchManager instance = null;

    public Touch touch;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Touch();
    }

    void Touch()
    {
        // 현재 터치되어 있는 카운트 가져오기

        int cnt = Input.touchCount;
        //     Debug.Log( "touch Cnt : " + cnt );

        // 동시에 여러곳을 터치 할 수 있기 때문.
        for (int i = 0; i < cnt; ++i)
        {
            // i 번째로 터치된 값 이라고 보면 된다.
            touch = Input.GetTouch(i);
            Vector2 pos = touch.position;

            // 조금 더 디테일하게!
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("시작점 : (" + i + ") : x = " + pos.x + ", y = " + pos.y);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("끝점 : (" + i + ") : x = " + pos.x + ", y = " + pos.y);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
            }
        }
    }
}
