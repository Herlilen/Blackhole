using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop/Item")]
public class ShopItems : ScriptableObject
{
    public string itemName;
    public string discripiton;
    public float price;
    public float satietyRegen;
    public float concentrationRegen;
    public float sanityRegin;
    public int researchUpgrade;
}
