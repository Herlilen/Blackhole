using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using System.Collections;

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
    public Button playButton;
    public Button exitButton;
    
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
        //disable the buttons
        exitButton.interactable = false;
        
        //change color filter to red
        _colorAdjustments.colorFilter.value = Color.red;

        //set bool
        startGame = true;

        //hide the cursor
        Cursor.visible = false;
        
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
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EaseOut()
    {
        _colorAdjustments.contrast.value -= 50 * Time.deltaTime;
    }
}
