using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Obj_ResearchDesk ObjResearchDesk;
    public ShopItems ShopItems;
    public TextMeshProUGUI name;
    public TextMeshProUGUI description;
    public TextMeshProUGUI creditCost;

    public void Start()
    {
        name.text = ShopItems.itemName;
    }

    public void Purchase()
    {
        if (ResourceManager.instance.HasEnoughResource("CREDITS", ShopItems.price)) //check if purchase able
        {
            ResourceManager.instance.RemoveResource("CREDITS", ShopItems.price + ObjResearchDesk.researchPointOutPut*2);
            PlayerStatus.instance.AddResource("SATIETY", ShopItems.satietyRegen);
            PlayerStatus.instance.AddResource("CONCENTRATION", ShopItems.concentrationRegen);
            PlayerStatus.instance.AddResource("SANITY", ShopItems.sanityRegin);

            ObjResearchDesk.researchPointOutPut += ShopItems.researchUpgrade;
        }
        EventSystem.current.SetSelectedGameObject(null);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        description.text = ShopItems.discripiton;
        creditCost.text = "CREDITS: " + (ShopItems.price + ObjResearchDesk.researchPointOutPut*2);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        description.text = ""; // Clear text or set to default text when not hovering
        creditCost.text = "";
    }
}
