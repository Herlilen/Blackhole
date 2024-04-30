using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    [Header("Resources")] 
    public float creditsIncreaseRate;
    public float researchPoint;
    public float energyLevel;
    public float blackholeMass;

    //what resources they have: money, energy, mass, research point
    public Dictionary<string, float> resourceOwned = new()
    {
        { "CREDITS", 0 },
        { "RESEARCH POINT", 0 },
        { "ELECTRICITY", 20},
        { "BLACKHOLE MASS", 10},
        { "CONTRIBUTION", 100}
    };
    
    [Header("Resource UI")] 
    public TextMeshProUGUI t_crecit;
    public TextMeshProUGUI t_researchPoint;
    public TextMeshProUGUI t_energyLevel;
    public TextMeshProUGUI t_blackholeMass;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //UPDATE RESOURCE UI//
        t_crecit.text = "" + Mathf.Round(resourceOwned["CREDITS"]);
        t_researchPoint.text = "" + Mathf.Round(resourceOwned["RESEARCH POINT"]);
        t_energyLevel.text = "" + Mathf.Round(resourceOwned["ELECTRICITY"]);
        t_blackholeMass.text = "" + Mathf.Round(resourceOwned["BLACKHOLE MASS"]);
        
        //RESOURCE SETTING
        AddResource("CREDITS", creditsIncreaseRate * Time.deltaTime);
        creditsIncreaseRate = 1 + (blackholeMass / 10) * (blackholeMass / 10);
    }

    public void AddResource(string resourceType, float amount)
    {
        resourceOwned[resourceType] = resourceOwned[resourceType] + amount;
    }

    public void RemoveResource(string resourceType, float cost)
    {
        if (!HasEnoughResource(resourceType, cost))
        {
            return;
        }

        resourceOwned[resourceType] = resourceOwned[resourceType] - cost;
    }

    public bool HasEnoughResource(string resourceType, float amount)
    {
        if (resourceOwned[resourceType] < amount)
        {
            return false;
        }

        return true;
    }
}
