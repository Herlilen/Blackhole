using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMenu : MonoBehaviour
{
    [Header("Audio")]
    private AudioSource _audioSource;
    public AudioClip[] _Clips;
    
    [Header("Pause Menu")]
    public bool menuCalledOut;
    public GameObject pauseMenuUI;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuCalledOut)
            {
                BringMeTheMenu();
            }
        }
    }

    public void HoverSound()
    {
        _audioSource.PlayOneShot(_Clips[0]);
    }
    
    public void ClickSound()
    {
        _audioSource.PlayOneShot(_Clips[1]);
    }
    
    public void ResumeTheGame()
    {
        pauseMenuUI.SetActive(false);
        menuCalledOut = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    void BringMeTheMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        menuCalledOut = true;
    }
}
