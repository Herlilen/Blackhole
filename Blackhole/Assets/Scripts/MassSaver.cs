using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System.IO;

public class MassSaver : MonoBehaviour
{
    private int mass = 0;

    int Mass
    {
        get
        {
            mass = Mathf.RoundToInt(ResourceManager.instance.blackholeMass);
            return mass;
        }
        set
        {
            mass = value;

            if (mass > HighMass)
            {
                HighMass = mass;
            }
        }
    }

    private int highMass;
    
    int HighMass
    {
        get
        {
            if (File.Exists(DATA_FULL_HS_FILE_PATH))
            {
                string fileContents = File.ReadAllText(DATA_FULL_HS_FILE_PATH);
                highMass = Int32.Parse(fileContents);
            }

            return highMass;
        }
        
        set
        {
            highMass = value;
            string fileContent = "" + highMass;

            if (!Directory.Exists(Application.dataPath + DATA_DIR))
            {
                Directory.CreateDirectory(Application.dataPath + DATA_DIR);
            }

            File.WriteAllText(DATA_FULL_HS_FILE_PATH, fileContent);
        }
    }
    
    const string DATA_DIR = "/Data/";
    const string DATA_HS_FILE = "hs.txt";
    
    string DATA_FULL_HS_FILE_PATH;
    
    // Start is called before the first frame update
    void Start()
    {
        DATA_FULL_HS_FILE_PATH = Application.dataPath + DATA_DIR + DATA_HS_FILE;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(highMass);
    }
}
