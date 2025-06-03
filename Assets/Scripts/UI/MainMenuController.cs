using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject OptionPanne;

    public Slider brightnessSlider;
    private float defaultBrightness;

    void Start()
    {

        defaultBrightness = Screen.brightness;
        
  
        brightnessSlider.minValue = 0.1f; 
        brightnessSlider.maxValue = 1f;
        brightnessSlider.value = defaultBrightness;
        

        brightnessSlider.onValueChanged.AddListener(SetBrightness);
    }

    public void SetBrightness(float value)
    {
    
        Screen.brightness = value;
    }

    void OnApplicationQuit()
    {
 
        Screen.brightness = defaultBrightness;
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void Galeria()
    {
        SceneManager.LoadScene(2);
    }

    public void Option()
    {
        OptionPanne.GetComponent<Image>().raycastTarget = true;

        OptionPanne.transform.GetComponent<CanvasGroup>().DOFade(1, 0.4f).SetEase(Ease.OutBack);
    }

    public void CerrarOption()
    {
        OptionPanne.GetComponent<Image>().raycastTarget = false;

        OptionPanne.transform.GetComponent<CanvasGroup>().DOFade(0, 0.4f).SetEase(Ease.OutBack);

    }
}
