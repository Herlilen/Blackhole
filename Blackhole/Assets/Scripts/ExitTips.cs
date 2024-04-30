using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExitTips : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI treason;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        treason.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        treason.enabled = false;
    }
}
