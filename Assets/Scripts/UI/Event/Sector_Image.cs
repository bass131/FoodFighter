using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sector_Image : Event_Image {

    public Animator anim; // 애니메이터.


	// Use this for initialization
	void Start () {
        if (_spriteRenderer == null)
            _spriteRenderer = this.GetComponent<SpriteRenderer>();

        _spriteRenderer.enabled = true;

        if (_image == null)
            _image = this.GetComponent<Image>();

        anim = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        _image.sprite = _spriteRenderer.sprite;
        _image.color = _spriteRenderer.color;
	}

    public void Sector_1()
    {
        anim.SetTrigger("Sector_1");
    }

    public void Sector_2()
    {
        anim.SetTrigger("Sector_2");
    }

    public void Sector_3()
    {
        anim.SetTrigger("Sector_3");
    }

    public void Boss_Sector()
    {
        anim.SetTrigger("Boss");
    }


    public void Clear()
    {
        anim.SetTrigger("Clear");
    }

    public void All_Clear()
    {
        anim.SetTrigger("All_Clear");
    }
  
}
