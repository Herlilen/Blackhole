using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Obj_Blackhole : MonoBehaviour
{
    [SerializeField] private BoxCollider trigger;
    private bool interactable;
    public bool upgradeable;
    public GameObject interaction;
    public TextMeshProUGUI cost;
    public float rpCost;
    public float creditCost;
    public float researchTime;
    public float costFactor;
    public float contributionOutput;
    
    // Start is called before the first frame update
    void Start()
    {
        interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        //set ui
        cost.text = "UPGRADE COST" + "\n" + "R.P * " + rpCost + "\n" + "CREDITS * " + creditCost;
        
        //set the cost based on blackhole status
        creditCost = Mathf.Round(25 * ResourceManager.instance.resourceOwned["BLACKHOLE MASS"] / 10);
        rpCost = Mathf.Round(5 * ResourceManager.instance.resourceOwned["BLACKHOLE MASS"] / 10);
        
        //interaction
        if (ResourceManager.instance.HasEnoughResource("CREDITS", creditCost) &&
            ResourceManager.instance.HasEnoughResource("RESEARCH POINT", rpCost) && interactable)
        {
            //trigger the ui
            interaction.SetActive(true);
            //remove the cost
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerStatus.instance.researching = true;
                Invoke("costAndGenerate", researchTime);
            }
        }
        else
        {
            interaction.SetActive(false);
        }
    }

    void costAndGenerate()
    {
        ResourceManager.instance.RemoveResource("CREDITS", creditCost);
        ResourceManager.instance.RemoveResource("RESEARCH POINT", rpCost);
        ResourceManager.instance.AddResource("BLACKHOLE MASS", 1);
        PlayerStatus.instance.AddResource("CONTRIBUTION", contributionOutput);
        PlayerStatus.instance.researching = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            interactable = true;
            interaction.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interactable = false;
            interaction.SetActive(false);
        }
    } 
}
