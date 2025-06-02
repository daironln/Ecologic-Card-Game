using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private Stack<EcologicCradType> CardsTypes = new();

    // void Start()
    // {
    //     CardsTypes = new();
    // }

    public void AddCard(EcologicCradType type)
    {
        CardsTypes.Push(type);
    }

    public EcologicCradType GetCard()
    {

        if (CardsTypes.Count <= 0)
        {
            Debug.LogWarning("No hay cartas en el mazo");

            if(GameManager.Instance._player.SustainabilityPoints >= GameManager.Instance._pcPlayer.SustainabilityPoints)
                GameManager.Instance.WinGame(false);
            else
                GameManager.Instance.WinGame(true);
        }

        return CardsTypes.Pop();
    }

    public int CardsCount()
    {
        return CardsTypes.Count;
    }

    public void Shuffle()
    {
        // //Fisher-Yates
        // for(int i = CardsTypes.Count - 1; i > 0; i--)
        // {
        //     int j = Random.Range(0, i + 1);

        //     var temp = CardsTypes[i];
        //     CardsTypes[i] = CardsTypes[j];
        //     CardsTypes[j] = temp;

        // }

        // CardsTypes = CardsTypes.OrderBy(x => Random.Range(0.0f, 1.0f)).ToList();

        // var a = CardsTypes;

        // a = a.OrderBy(x => Random.Range(0.0f, 1.0f)).ToList();

        // CardsTypes = CardsTypes.Concat(a).ToList();

        // var b = a;

        // b = b.OrderBy(x => Random.Range(0, 9999999)).ToList();

        // CardsTypes = CardsTypes.Concat(b).ToList();

        // for(int i = 0; i < 2; i++)
        //     CardsTypes.Insert(Random.Range(0, CardsTypes.Count - 1), EcologicCradType.EnergiaLimpia);


        // Debug.Log($"{a.Count}, {b.Count}, {CardsTypes.Count}");
    }
}
