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
        "Reciclar una tonelada de papel salva 17 árboles y ahorra 26,500 litros de agua (EPA).",
        "La energía solar podría satisfacer el 45% de la demanda eléctrica mundial para 2050 (Agencia Internacional de Energía).",
        "Un grifo que gotea pierde hasta 75 litros de agua al día (Organización Mundial de la Salud).",
        "La producción de ropa genera el 10% de las emisiones globales de carbono (ONU Medio Ambiente).",
        "Cada año se desperdician 1,300 millones de toneladas de alimentos, equivalente a US$1 billón (FAO).",
        "Los edificios consumen el 36% de la energía global y emiten el 39% del CO₂ (Global Alliance for Buildings and Construction).",
        "Reciclar aluminio ahorra el 95% de la energía necesaria para producirlo nuevo (Instituto del Aluminio).",
        "Los océanos absorben el 25% del CO₂ emitido por humanos, acidificando sus aguas (UNESCO).",
        "La economía circular podría reducir un 48% las emisiones de la UE para 2030 (Fundación Ellen MacArthur).",
        "Una bombilla LED consume un 85% menos de energía que una incandescente (Departamento de Energía de EE.UU.).",
        "El 80% de los plásticos oceánicos provienen de fuentes terrestres (National Geographic).",
        "Reutilizar 1 kg de textiles evita 25 kg de CO₂ y reduce 6,000 litros de consumo de agua (WRAP UK).",
        "Los vehículos eléctricos reducen emisiones en un 50-70% comparados con los de gasolina (Agencia Internacional de Energía).",
        "La agricultura regenerativa podría secuestrar 250 millones de toneladas de CO₂ anuales en suelos (Drawdown Project).",
        "El 60% de los residuos electrónicos no se recicla formalmente (Global E-waste Monitor).",
        "Cada minuto se compran 1 millón de botellas plásticas en el mundo (UNEP).",
        "Los humedales naturales purifican agua y almacenan carbono 55 veces más rápido que bosques (Convención Ramsar).",
        "Duplicar la vida útil de un smartphone reduce sus emisiones en un 40% (European Environmental Bureau).",
        "Los parques eólicos marinos podrían generar 18 veces la demanda eléctrica global actual (IEA).",
        "El compostaje reduce un 50% el volumen de residuos en vertederos (EPA).",
        "La deforestación causa el 12-20% de las emisiones globales de GEI (IPCC).",
        "Usar transporte público reduce tu huella de carbono en un 67% vs. auto privado (UITP).",
        "Un árbol adulto absorbe 22 kg de CO₂ al año y libera oxígeno para 2 personas (Arbor Day Foundation).",
        "La moda rápida ha aumentado un 400% el consumo textil en 20 años (Ellen MacArthur Foundation).",
        "Las energías renovables crearon 12 millones de empleos globales en 2023 (IRENA).",
        "Reciclar vidrio ahorra un 30% de energía vs. producción nueva (Glass Packaging Institute).",
        "La carne bovina genera 60 kg de GEI por kg producido, 300 veces más que legumbres (Science Journal).",
        "Las ciudades ocupan el 3% del planeta pero consumen el 78% de la energía (ONU-Hábitat).",
        "Restaurar el 15% de ecosistemas degradados evitaría el 60% de extinciones (Convención de Biodiversidad).",
        "El agua reciclada podría cubrir el 40% de la demanda industrial mundial (Banco Mundial)."
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

            _player.SacrifyCard(PlayerSelectedCard, 1);

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
            var r = Random.Range(0, 101);

            if(r < 50)
                _pcPlayer.PassTurn();
            else 
                _pcPlayer.SacrifyCard(_pcPlayer.HandsCards[Random.Range(0, _pcPlayer.HandsCards.Count)], 1);
            
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
        _player.SacrifyCard(PlayerSelectedCard, 1);

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
