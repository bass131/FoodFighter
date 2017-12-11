using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_CoolTime : MonoBehaviour, IPointerDownHandler {

    /*
     * 스킬 사용 가능
     * ㄴ카운트 x , 스킬 이름 o
     */

    public Image skillFilter;
    public Text coolTimeCounter; //남은 쿨타임을 표시할 텍스트
    public Text Skill_Name; // 쿨타임 초 시간.

    public float coolTime;
    private float currentCoolTime; //남은 쿨타임을 추적 할 변수

    public bool canUseSkill = true; //스킬을 사용할 수 있는지 확인하는 변수

    void Start()
    {
        skillFilter.fillAmount = 0; //처음에 스킬 버튼을 가리지 않음
    }
    void Update()
    {
        Text();
    }

    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("Input");
        UseSkill();
    }

    public void Text()
    {
        if (canUseSkill)
        {
            Skill_Name.color = new Color(255, 255, 255, 255);
            coolTimeCounter.color = new Color(255, 255, 255, 0);
        }
        else
        {
            Skill_Name.color = new Color(255, 255, 255, 0);
            coolTimeCounter.color = new Color(255, 255, 255, 255);
        }
    }

    public void UseSkill()
    {
        if (canUseSkill)
        {
            canUseSkill = false; //스킬을 사용하면 사용할 수 없는 상태로 바꿈

            Debug.Log("Use Skill");
            skillFilter.fillAmount = 1; // 스킬 버튼을 가림
            StartCoroutine("Cooltime");  // 스킬 필터 코루틴 시작.


            currentCoolTime = coolTime; // 현재 쿨타임 = 쿨타임.
            coolTimeCounter.text = "" + currentCoolTime; // 쿨타임 카운터의 텍스트 = 현재 쿨타임의 텍스트.

            StartCoroutine("CoolTimeCounter"); // 스킬 카운터 코루틴 시작.

        }
        else
        {
            Debug.Log("아직 스킬을 사용할 수 없습니다.");
        }
    }

    IEnumerator Cooltime() // 필터 코루틴.
    {
        while (skillFilter.fillAmount > 0)
        {
            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
            yield return null;
        }

        canUseSkill = true; //스킬 쿨타임이 끝나면 스킬을 사용할 수 있는 상태로 바꿈

        yield break;
    }

    //남은 쿨타임을 계산할 코르틴을 만들어줍니다.
    IEnumerator CoolTimeCounter() // 쿨타임 계산 코루틴
    {
        while (currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);

            currentCoolTime -= 1.0f;
            coolTimeCounter.text = "" + currentCoolTime;
        }

        yield break;
    }
}
