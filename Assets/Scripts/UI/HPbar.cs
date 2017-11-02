using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPbar : MonoBehaviour
{

    public Image hpmask;
    public Image heartmask;
    private RectTransform hpmaskRect;
    private RectTransform heartmaskRect;
    public float damge;
    public float maxHP;
    public float maxheart;
    private float HP;
    private float hearthp;
    private float maxHpBarWidth;
    private float maxheartbarwidth;

    // Use this for initialization
    void Start()
    {
        hpmaskRect = hpmask.GetComponent<RectTransform>();
        heartmaskRect= heartmask.GetComponent<RectTransform>();
        maxHpBarWidth = hpmaskRect.sizeDelta.x;
        maxheartbarwidth = heartmaskRect.sizeDelta.x;
        HP = maxHP;
        hearthp = maxheart;
    }

    /*   
          public void HpUP()
        {
            currentHP += 10;

            if (currentHP > maxHP)
            {
                currentHP = maxHP;
            }


            float deltaSize = currentHP / maxHP;

            maskRect.sizeDelta = new Vector2(maxHpBarWidth * deltaSize, maskRect.sizeDelta.y);
        }
    */
    public void HpDown()
    {

        if (HP < 0)
        {
            HP = 5;
            hearthp--;
            float heartdeltaSize = hearthp / maxheart;
            heartmaskRect.sizeDelta = new Vector2(maxheartbarwidth * heartdeltaSize, heartmaskRect.sizeDelta.y);
        }
        
        float deltaSize = HP / maxHP;
        hpmaskRect.sizeDelta = new Vector2(maxHpBarWidth * deltaSize, hpmaskRect.sizeDelta.y);
    }
}



