using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Menu : MonoBehaviour
{
    [Header("Audio")]
    private AudioSource _audioSource;
    public AudioClip[] _Clips;
    public AudioSource _musicAudio;
    
    [Header("PostProcessing")]
    public Volume _volume;
    private ColorAdjustments _colorAdjustments;

    [Header("Game Status")] 
    public bool startGame;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (!_volume.profile.TryGet(out _colorAdjustments))
        {
            return;
        }
    }

    private void Update()
    {
        if (startGame)
        {
            _musicAudio.volume -= Time.deltaTime;
            Invoke("EaseOut", 2f);
        }
    }

    public void PlayGame()
    {
        //change color filter to red
        _colorAdjustments.colorFilter.value = Color.red;

        startGame = true;
        
       //delay to load the level
       Invoke("PlayFirstLevel", 4f);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void HoverSound()
    {
        _audioSource.PlayOneShot(_Clips[0]);
    }

    public void ClickSound()
    {
        _audioSource.PlayOneShot(_Clips[1]);
    }

    public void PlayFirstLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EaseOut()
    {
        _colorAdjustments.contrast.value -= 50 * Time.deltaTime;
    }
}
