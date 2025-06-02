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


    public void FadeInPanne(CardVisual visual)
    {
        EducativePhrase.SetText(GameManager.Instance.frasesSostenibilidad[Random.Range(0, GameManager.Instance.frasesSostenibilidad.Length)]);
        Panne.transform.GetComponent<CanvasGroup>().DOFade(1, 0.4f).SetEase(Ease.OutBack);


        CardImage.sprite = visual.cardImage.sprite;
        CardTittle.SetText(visual.parentCard.CardTittle);
        CardDescription.SetText(visual.parentCard.CardDescription);
        CardType.SetText($"•{visual.parentCard.Clasification}•");
        CardInsideTittle.SetText(visual.parentCard.CardTittle.ToUpper());
        CardInsideTipo.SetText(visual.parentCard.Clasification);
        CardInsideDescription.SetText(visual.parentCard.CardDescription);
    }

    public void FadeOutPanne()
    {
        Panne.transform.GetComponent<CanvasGroup>().DOFade(0, 0.4f).SetEase(Ease.OutBack);

        CardTittle.SetText("");
        CardType.SetText("");
        CardDescription.SetText("");
    }
}
