using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject OptionPanne;
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
