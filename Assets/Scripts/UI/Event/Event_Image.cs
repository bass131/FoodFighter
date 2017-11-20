using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Image : MonoBehaviour {

    [SerializeField]
    protected SpriteRenderer _spriteRenderer;

    [SerializeField]
    protected Image _image;


    // Use this for initialization
    void Start()
    {

        if (_spriteRenderer == null)
            _spriteRenderer = this.GetComponent<SpriteRenderer>();

        _spriteRenderer.enabled = true;

        if (_image == null)
            _image = this.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {

        _image.sprite = _spriteRenderer.sprite;
        _image.color = _spriteRenderer.color;

    }
}
