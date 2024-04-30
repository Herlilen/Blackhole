using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Obj_ResearchDesk : MonoBehaviour
{
    [SerializeField] private BoxCollider trigger;
    public int researchPointOutPut;
    public float contributionPointOutPut;
    public float concentrationCost;
    public float sanityCost;
    public float researchTime;
    private bool interactablt;
    public bool researchable;
    public GameObject interaction;
    public TextMeshProUGUI researchPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        interactablt = false;
        researchable = true;
    }

    // Update is called once per frame
    void Update()
    {
        //set ui number
        researchPoint.text = "REWARD: RESEARCH POINT * " + researchPointOutPut;
        
        if (interactablt)
        {
            if (Input.GetKeyDown(KeyCode.Space) && researchable)
            {
                PlayerStatus.instance.researching = true;
                Invoke("costAndGenerate", researchTime);
                researchable = false;
            }
            
            if (researchable)
            {
                interaction.SetActive(true);
            }
            else
            {
                interaction.SetActive(false);
            }
        }
    }

    void costAndGenerate()
    {
        PlayerStatus.instance.RemoveResource("CONCENTRATION", concentrationCost);
        PlayerStatus.instance.RemoveResource("SANITY", sanityCost);
        ResourceManager.instance.AddResource("RESEARCH POINT", researchPointOutPut);
        PlayerStatus.instance.AddResource("CONTRIBUTION", contributionPointOutPut);
        researchable = true;
        PlayerStatus.instance.researching = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            interactablt = true;
            interaction.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interactablt = false;
            interaction.SetActive(false);
        }
    } 
}
