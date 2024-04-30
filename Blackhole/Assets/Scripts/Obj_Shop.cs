using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Obj_Shop : MonoBehaviour
{
    [SerializeField] private BoxCollider trigger;
    public GameObject shopMenu;
    private bool interactable;
    public GameObject interaction;

    // Start is called before the first frame update
    void Start()
    {
        interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        //interaction with the shop
        if (interactable)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //open shop menu
                if (shopMenu.activeSelf)    //if menu is already out
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    shopMenu.SetActive(false);
                }
                else
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    shopMenu.SetActive(true);
                }
            }
        }
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
