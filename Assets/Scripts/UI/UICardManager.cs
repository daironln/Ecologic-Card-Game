using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class UICardManager : GenericSingleton<UICardManager>
{
    public GameObject Panne;
    public TextMeshProUGUI CardTittle;
    public TextMeshProUGUI CardDescription;
    public TextMeshProUGUI EducativePhrase;
    public TextMeshProUGUI CardType;


    public TextMeshProUGUI CardInsideTittle;
    public TextMeshProUGUI CardInsideTipo;
    public TextMeshProUGUI CardInsideDescription;


    public Image CardImage;


    public void FadeInPanne()
    {
        Panne.transform.GetComponent<CanvasGroup>().DOFade(1, 0.4f).SetEase(Ease.OutBack);
    }

    public void FadeOutPanne()
    {
        Panne.transform.GetComponent<CanvasGroup>().DOFade(0, 0.4f).SetEase(Ease.OutBack);
    }
}
