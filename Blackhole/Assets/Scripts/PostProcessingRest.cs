using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingRest : MonoBehaviour
{
    private Volume _Volume;
    private ColorAdjustments _colorAdjustments;
    private float transitionDuration = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        _Volume = GameManager.instance.GetComponent<Volume>();
        
        if (!_Volume.profile.TryGet(out _colorAdjustments))
        {
            return;
        }

        StartCoroutine(TransionColor());
    }

    // Update is called once per frame
    void Update()
    {
        if (_colorAdjustments.contrast.value < 0)
        {
            _colorAdjustments.contrast.value += 50 * Time.deltaTime;
        }

        if (_colorAdjustments.contrast.value >= 0)
        {
            _colorAdjustments.contrast.value = 0;
        }

        if (_colorAdjustments.contrast.value == 0)
        {
            Invoke("DestoryThis", 5f);
        }
    }

    IEnumerator TransionColor()
    {
        float time = 0;
        Color startColor = Color.red;
        Color endColor = Color.white;

        while (time < transitionDuration)
        {
            _colorAdjustments.colorFilter.value = Color.Lerp(startColor, endColor, time / transitionDuration);
            time += Time.deltaTime;
            yield return null;
        }

        _colorAdjustments.colorFilter.value = endColor;
    }
    
    void DestoryThis()
    {
        Destroy(gameObject);
    }
}
