using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bgImg;
    private Image joystickImg;

    public Vector3 InputDirection { set; get; }

    private void Start()
    {
        bgImg = GetComponent<Image>(); // 배경 이미지 컴포넌트 초기화.
        joystickImg = transform.GetChild(0).GetComponent<Image>(); // 조종 스틱 이미지 컴포넌트 초기화.
    }

    public virtual void OnDrag(PointerEventData _ped)
    {
        Debug.Log("OnDrag");
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform,_ped.position,_ped.pressEventCamera, out pos))
        {
            Debug.Log("hey");
        }

    }
    public virtual void OnPointerDown(PointerEventData _ped)
    {
        Debug.Log("OnPointerDown");
        OnDrag(_ped);
    }

    public virtual void OnPointerUp(PointerEventData _ped)
    {
        Debug.Log("OnPointerUp");
    }
}
