using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;


public enum GameState
{
    None,
    InMainMenu,
    InPlay, 
    Inpause,
    InGameOver,
    InSelectionMenu,
}

public enum EcologicCardClasification
{
    Accion,
    Infraestructura,
    Desafios,
    Recursos,
    Colaboracion
}

public enum EcologicCradType
{
    EducacionAmbiental,
    EnergiaLimpia,
    ReciclajeUrbano,
    SubvencionGubernamental,
    ContaminacionAire,
    ResiduosToxicos,
    SequiaExtrema,
    AlianzaGlobal,
    CumbreClimatica,
    InnovacionCompartida,
    EdificioVerde,
    ParqueEolico,
    TransporteElectrico,
    CentroReciclaje,
    GranjaSolar,
    HuertoUrbano,
    
    None
}

public class GameManager : GenericSingleton<GameManager>
{
    public GameState GameState {get; set;} = GameState.None;

    public Deck DeckCard;
    public int[] CardsDisponivility = new int[] {6, 6, 6, 6, 
                                                 8, 8, 8, 
                                                 8, 8, 8,
                                                 8, 8, 8,
                                                 8, 8, 8};

    public Sprite[] Images;


    public Card PlayerSelectedCard;

    #region Players

   public PlayerStats _player, _pcPlayer;
   public bool PlayerTurn = true;

    #endregion





    protected override void Awake()
    {
        base.Awake();
        StartGame();


    }

    void Start()
    {
        UIManager.Instance.UpdateUI();
        
    }

    public void WinGame(bool isPc)
    {
        UIManager.Instance.FadeInSelectionPanne();

        if(isPc)
        {
            GameState = GameState.InGameOver;
            _pcPlayer.canPlay = false;
            _player.canPlay = false;

            UIManager.Instance.SelectionPanneTittle.SetText("Que mal has perido!");
            UIManager.Instance.SelectionPanneDescripcion.SetText("Desea volver al menu principal (A) o continuar jugando (B?");


            UIManager.Instance.SelectionPanneImage.sprite = UIManager.Instance.Logo;
            UIManager.Instance.SelectionPanneButnA.onClick.AddListener(UIManager.Instance.ToMenu);
            UIManager.Instance.SelectionPanneButnB.onClick.AddListener(UIManager.Instance.Continue);
        }
        else
        {
            GameState = GameState.InGameOver;
            _pcPlayer.canPlay = false;
            _player.canPlay = false;

            UIManager.Instance.SelectionPanneTittle.SetText("Enhorabuena has Ganado!");
            UIManager.Instance.SelectionPanneDescripcion.SetText("Desea volver al menu principal (A) o continuar jugando (B?");

            UIManager.Instance.SelectionPanneImage.sprite = UIManager.Instance.Logo;
            UIManager.Instance.SelectionPanneButnA.onClick.AddListener(Play);
            UIManager.Instance.SelectionPanneButnB.onClick.AddListener(Sacrify);
        }
    }

    private void Update()
    {

        if(_pcPlayer.canPlay)
        {
            StartCoroutine(PcIA());
        }


        if(Input.GetButtonDown("Jump") && PlayerSelectedCard != null)
        {

            _player.PlayCard(PlayerSelectedCard);
            UICardManager.Instance.FadeOutPanne();

        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {

           UIManager.Instance.PauseMenu();

        }

        if(Input.GetKeyDown(KeyCode.F))
        {

            _player.GetCard();
            _pcPlayer.GetCard();

        }

        if(Input.GetKeyDown(KeyCode.A))
        {

            foreach(var e in _player.Effects)
                Debug.Log($"Effecs player {e}");

        }

        if(Input.GetKeyDown(KeyCode.S))
        {

            _player.SacrifyCard(PlayerSelectedCard, 5);

        }

        if(Input.GetKeyDown(KeyCode.P))
        {

            _player.PassTurn();

        }
    }

    private IEnumerator PcIA()
    {

        bool CanPlay()
        {
            foreach(var e in _pcPlayer.HandsCards)
            {
                if(e.Effect.GetCost() <= _pcPlayer.RawMaterials)
                    return true;
            }

            return false;
        }

        if(!CanPlay())
        {
            _pcPlayer.PassTurn();
            yield return null;
        }
        
        yield return new WaitForSeconds(Random.Range(2f, 3.5f));


        _pcPlayer.PlayCard(_pcPlayer.HandsCards[Random.Range(0, _pcPlayer.HandsCards.Count)]);

        yield return null;


    } 


    private void StartGame()
    {

        GameState = GameState.InPlay;
        _player.canPlay = true;
        _player.isPc = false;
        _pcPlayer.isPc = true;

        MakeMazo();

        // DeckCard.Shuffle();

        StartCoroutine(InitialCardRepart());



    }

    public void Play()
    {
        _player.PlayCard(PlayerSelectedCard);
        
        UICardManager.Instance.FadeOutPanne();
    }

    public void Sacrify()
    {
        _player.SacrifyCard(PlayerSelectedCard, 5);

        UICardManager.Instance.FadeOutPanne();


    }

    public void Pass()
    {
        _player.PassTurn();

        UICardManager.Instance.FadeOutPanne();


    }

    private IEnumerator InitialCardRepart()
    {
        for(int i = 0; i < 4; i ++)
        {
            _player.GetCard();
            yield return new WaitForSeconds(0.2f);
        }
        
        for(int i = 0; i < 4; i++)
        {
            _pcPlayer.GetCard();
            yield return new WaitForSeconds(0.2f);
        }

        yield return null;
    }

    private void MakeMazo()
    { 

        int cont = 0;

        for(int i = 0; i < 100; i ++)
        {
            var j = Random.Range(0, 16);

            while(CardsDisponivility[j] <= 0)
            {
                j = Random.Range(0, 16);

                cont++;

                if(cont >= 1000)
                {
                    Debug.Log(cont);
                    Debug.Log(DeckCard.CardsCount());

                    return;
                }

            }

            CardsDisponivility[j] --;

            DeckCard.AddCard((EcologicCradType)j);
            
        }


        Debug.Log(cont);

        Debug.Log(DeckCard.CardsCount());

        
    }


}
