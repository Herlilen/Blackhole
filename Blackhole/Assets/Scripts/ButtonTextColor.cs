using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI buttonText;
    public Color defaultColor;
    public Color hoverColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = defaultColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonText.color = defaultColor;
    }
}
