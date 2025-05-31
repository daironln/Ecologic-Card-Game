using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : GenericSingleton<UIManager>
{
   public TextMeshProUGUI PcPoints;
   public TextMeshProUGUI PcMaterials;
   public TextMeshProUGUI PcResiduos;

    public TextMeshProUGUI PlayerPoints;
    public TextMeshProUGUI PlayerMaterials;
    public TextMeshProUGUI PlayerResiduos;

    public GameObject SelectionPanne;

    public TextMeshProUGUI SelectionPanneTittle;
    public TextMeshProUGUI SelectionPanneDescripcion;
    public Button SelectionPanneButnA;
    public Button SelectionPanneButnB;
    public Image SelectionPanneImage;
    public Sprite Logo;


    public void UpdateUI()
    {
        UpdatePoints();
    }

    public void PauseMenu()
    {
        SelectionPanneImage.sprite = Logo;
        SelectionPanneTittle.SetText("Desea ir al menu inicial? ");
        SelectionPanneDescripcion.SetText("Seleccione A para salir al menu o B para seguir jugando");

        SelectionPanneButnA.onClick.AddListener(ToMenu);
        SelectionPanneButnB.onClick.AddListener(Continue);

        FadeInSelectionPanne();

    }

    public void ToMenu()
    {

        SelectionPanneButnA.onClick.RemoveListener(ToMenu);
        SelectionPanneButnB.onClick.RemoveListener(Continue);

        FadeOutSelectionPanne();

        SceneManager.LoadScene(0);

    }

    public void Continue()
    {
        SelectionPanneButnA.onClick.RemoveListener(ToMenu);
        SelectionPanneButnB.onClick.RemoveListener(Continue);

        FadeOutSelectionPanne();



    }
    private void UpdatePoints()
    {
        PcPoints.SetText(GameManager.Instance._pcPlayer.SustainabilityPoints.ToString());
        PcMaterials.SetText(GameManager.Instance._pcPlayer.RawMaterials.ToString());
        PcResiduos.SetText(GameManager.Instance._pcPlayer.WastePoints.ToString());

        PlayerPoints.SetText(GameManager.Instance._player.SustainabilityPoints.ToString());
        PlayerMaterials.SetText(GameManager.Instance._player.RawMaterials.ToString());
        PlayerResiduos.SetText(GameManager.Instance._player.WastePoints.ToString());
    }

    public void FadeInSelectionPanne()
    {
        SelectionPanne.GetComponent<Image>().raycastTarget = true;

        GameManager.Instance.GameState = GameState.InSelectionMenu;
        SelectionPanne.GetComponent<CanvasGroup>().DOFade(1, 0.4f).SetEase(Ease.OutBack);
    }

    public void FadeOutSelectionPanne()
    {
        SelectionPanne.GetComponent<Image>().raycastTarget = false;

        GameManager.Instance.GameState = GameState.InPlay;

        SelectionPanne.GetComponent<CanvasGroup>().DOFade(0, 0.4f).SetEase(Ease.OutBack);
    }

}
