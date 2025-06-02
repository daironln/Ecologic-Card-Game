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


    public string[] frasesSostenibilidad = 
    {
    "Reciclar 1 tonelada de papel salva 17 árboles (EPA)",
    "La energía solar cubrirá 45% de electricidad mundial en 2050 (IEA)",
    "Un grifo que gotea pierde 75L agua/día (OMS)",
    "La moda rápida genera 10% de emisiones globales (ONU)",
    "1/3 de los alimentos se desperdicia mundialmente (FAO)",
    "Edificios consumen 36% de energía global (GlobalABC)",
    "Reciclar aluminio ahorra 95% de energía (IAI)",
    "Océanos absorben 25% del CO₂ humano (UNESCO)",
    "Economía circular reduce 48% emisiones UE (Ellen MacArthur)",
    "LEDs consumen 85% menos que bombillas incandescentes (DoE)",
    "80% plásticos oceánicos viene de tierra (NatGeo)",
    "Reusar 1kg textiles evita 25kg CO₂ (WRAP UK)",
    "Vehículos eléctricos reducen 60% emisiones (IEA)",
    "Agricultura regenerativa captura CO₂ en suelos (Drawdown)",
    "Solo 40% de electrónicos se recicla (UNU)",
    "1 millón botellas plásticas compradas/minuto (UNEP)",
    "Humedales almacenan 55x más carbono que bosques (Ramsar)",
    "Doblar vida de smartphone reduce 40% emisiones (EEB)",
    "Energía eólica marina podría abastecer mundo x18 (IEA)",
    "Compostaje reduce 50% residuos en vertederos (EPA)",
    "Deforestación causa 15% emisiones globales (IPCC)",
    "Transporte público reduce 67% huella carbono (UITP)",
    "1 árbol absorbe 22kg CO₂/año (Arbor Day)",
    "Consumo textil aumentó 400% en 20 años (Ellen MacArthur)",
    "Renovables crearon 12M empleos en 2023 (IRENA)",
    "Reciclar vidrio ahorra 30% energía (GPI)",
    "Carne bovina genera 60kg CO₂/kg (Science)",
    "Ciudades consumen 78% energía mundial (ONU-Hábitat)",
    "Restaurar 15% ecosistemas evita 60% extinciones (CDB)",
    "Agua reciclada cubriría 40% demanda industrial (Banco Mundial)"
    };







    
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
