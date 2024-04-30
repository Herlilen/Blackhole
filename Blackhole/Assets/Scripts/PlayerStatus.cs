using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;
    
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
    
    private Animator _animator;
    
    //json data saving
    private const string FILE_DIR = "/SAVE_DATA/";
    private string FILE_NAME = "<name>.json";
    private string FILE_PATH;

    [Header("Player Status")] 
    public float satietyDecreaseRatePerSec;
    public float concentrationRegen;
    public float sanityDecreaseRatePerSec;
    public float contributionRate;
    public bool researching;
    private bool dead = false;
    public int deathTimes;
 
    [Header("Status UI")]
    public GameObject statusUI;
    public GameObject i_satiety;
    public GameObject i_energy;
    public GameObject i_sanity;
    public GameObject i_contribution;
    private float minDistance = 1.7f;
    public float scaleFactor = 1f;

    private Dictionary<string, float> resourceOwned = new()
    {
        { "SATIETY", 100 },
        { "CONCENTRATION", 100 },
        { "SANITY", 100 },
        { "CONTRIBUTION", 100}
    };
    
    // Start is called before the first frame update
    void Start()
    {
        statusUI.SetActive(true);
        
        _animator = GetComponent<Animator>();
        
        FILE_NAME = FILE_NAME.Replace("<name>", name);

        FILE_PATH = Application.dataPath + FILE_DIR + FILE_NAME;

        if (File.Exists(FILE_PATH))
        {
            string jsonString = File.ReadAllText(FILE_PATH);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            return;
        }
        
        //SATIETY//
        //satiety decreases per 2 sec
        if (resourceOwned["SATIETY"] > 0)
        {
            RemoveResource("SATIETY", satietyDecreaseRatePerSec * Time.deltaTime);
        }
        
        //set the cap
        if (resourceOwned["SATIETY"] <= 0) //min
        {
            resourceOwned["SATIETY"] = 0;
        }
        if (resourceOwned["SATIETY"] >= 100) //max
        {
            resourceOwned["SATIETY"] = 100;
        }
        
        //CONCENTRATION//
        //regen per 2 sec
        if (resourceOwned["CONCENTRATION"] < 100)
        {
            AddResource("CONCENTRATION", concentrationRegen * Time.deltaTime);
        }

        if (resourceOwned["CONCENTRATION"] <= 0)
        {
            resourceOwned["CONCENTRATION"] = 0;
        }
        if (resourceOwned["CONCENTRATION"] >= 100)
        {
            resourceOwned["CONCENTRATION"] = 100;
        }
        
        //SANITY//
        if (resourceOwned["SANITY"] > 0)
        {
            RemoveResource("SANITY", sanityDecreaseRatePerSec * Time.deltaTime);
        }
        
        //set the cap
        if (resourceOwned["SANITY"] <= 0) //min
        {
            resourceOwned["SANITY"] = 0;
        }
        if (resourceOwned["SANITY"] >= 100) //max
        {
            resourceOwned["SANITY"] = 100;
        }
        
        //CONTRIBUTION//
        if (resourceOwned["CONTRIBUTION"] > 0)
        {
            RemoveResource("CONTRIBUTION", contributionRate * Time.deltaTime);
        }
        if (resourceOwned["CONTRIBUTION"] <= 0) //min
        {
            resourceOwned["CONTRIBUTION"] = 0;
        }
        if (resourceOwned["CONTRIBUTION"] >= 100) //max
        {
            resourceOwned["CONTRIBUTION"] = 100;
        }
        
        //STATUS UI//
        i_satiety.transform.localScale = new Vector3(resourceOwned["SATIETY"], 1, 1);
        i_sanity.transform.localScale = new Vector3(resourceOwned["SANITY"], 1, 1);
        i_energy.transform.localScale = new Vector3(resourceOwned["CONCENTRATION"], 1, 1);
        i_contribution.transform.localScale = new Vector3(resourceOwned["CONTRIBUTION"], 1, 1);
        
        //ACTIONS//
        _animator.SetBool("researching", researching);
        
        //DEATH//
        if (resourceOwned["SATIETY"] <= 0 || resourceOwned["CONCENTRATION"] <= 0 || resourceOwned["SANITY"] <= 0 || resourceOwned["CONTRIBUTION"] <= 0)
        {
            if (!dead)
            {
                dead = true;
                Death();
            }
        }
    }

    public void AddResource(string resourceType, float amount)
    {
        resourceOwned[resourceType] = resourceOwned[resourceType] + amount;
    }
    
    public void RemoveResource(string resourceType, float cost)
    {
        resourceOwned[resourceType] = resourceOwned[resourceType] - cost;
    }
    
    void Death()
    {
        //death animation
        _animator.SetBool("death", true);
        //add up death count
        deathTimes += 1;
        //save death times;
        Invoke("EndGame", 3f);
    }

    void EndGame()
    {
        //END GAME
        Destroy(gameObject);
        Destroy(ResourceManager.instance.gameObject);
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene(0);
    }
}
