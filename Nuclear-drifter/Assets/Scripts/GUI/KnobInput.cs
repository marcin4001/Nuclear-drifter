using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KnobInput : MonoBehaviour, IPointerClickHandler
{
    public bool isOn = false;
    public UnityEvent<bool> OnValueChange;
    private Image image;
    public Sprite onSprite;
    public Sprite offSprite;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void SetIsOn(bool _isOn)
    {
        image = GetComponent<Image>();
        isOn = _isOn;
        image.overrideSprite = isOn ? onSprite : offSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isOn = !isOn;
        image.overrideSprite = isOn ? onSprite : offSprite;
        OnValueChange?.Invoke(isOn);
    }

}
