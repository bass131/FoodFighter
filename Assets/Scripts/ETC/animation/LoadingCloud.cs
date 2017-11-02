using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCloud : MonoBehaviour { // 구름 움직임 관련 스크립트.

    public float moveSpeed = 0.1f; // 구름의 속도.

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () { // 매회 프레임에 반복 함수.
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime); // 왼쪽 방향 * 이동 속도 * 매회 프레임.
	}
}
