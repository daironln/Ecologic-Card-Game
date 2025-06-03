using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int SustainabilityPoints = 0;
    public int WastePoints = 0;
    public int RawMaterials = 0;
    public List<IEffect> Effects;

    public HorizontalCardHolder HorizontalCardHolder, HorizontalCardHolderTemporal, HorizontalCardHolderPermanent, HorizontalCardHolderChallenge;
    public List<Card> HandsCards;
    public PlayerStats contrario;
    public bool canPlay = false;
    public bool isPc = false;



    private void Awake()
    {
        Effects = new();
        HandsCards = new();
    }

    public void DescartarResiduo(int amount)
    {
        
        WastePoints -= amount;

        if(WastePoints < 0)
            WastePoints = 0;

        UIManager.Instance.UpdateUI();
        
    }

    public void GetResiduos(int amount)
    {
        WastePoints += amount;

        UIManager.Instance.UpdateUI();
    }

    public void GetRawMaterials(int amount)
    {
        RawMaterials += amount;

        UIManager.Instance.UpdateUI();

    
    }


    public void DesecharRawMaterials(int amount)
    {
        if(RawMaterials - amount >= 0)
            RawMaterials -= amount;

        UIManager.Instance.UpdateUI();
        
    }

    public void GetSustentabilityPoints(int amount)
    {
        SustainabilityPoints += amount;

        UIManager.Instance.UpdateUI();

    }

    public void ReduceSustentabilityPoints(int amount)
    {
        SustainabilityPoints -= amount;

        if(SustainabilityPoints < 0)
            SustainabilityPoints = 0;

        UIManager.Instance.UpdateUI();
        
    }

    private void ApplyEffects()
    {
        foreach(var eff in Effects)
        {
            Debug.Log($"Aplying effects {eff} to Player: {this} ");

            if(eff is IInfraestructureEffect)
            {
                if(((IInfraestructureEffect)eff).Generate())
                {
                    eff.AplyEfect(this);
                }

            }

            else
            {
                if(eff.IsActive())
                {
                    eff.AplyEfect(this);

                }

            }
        }

        Effects.RemoveAll(eff => !eff.IsActive());


        if(WastePoints >= GameManager.Instance.WasteToLose)
            GameManager.Instance.WinGame(contrario.isPc, true);



    }

    public void PassTurn()
    {
        if(!canPlay)
            return;

            
        WastePoints += 3;
        canPlay = !canPlay;
        contrario.GetCard();

        ApplyEffects();

        UIManager.Instance.UpdateUI();

        contrario.canPlay = !contrario.canPlay;
        GameManager.Instance.PlayerTurn = !GameManager.Instance.PlayerTurn;

    }

    public void Descard(Card card)
    {
        if(HandsCards.Contains(card))
            HandsCards.Remove(card);

        HorizontalCardHolder.RemoveCard(card);

        UIManager.Instance.UpdateUI();
    }

    public void SacrifyCard(Card card, int amaunt)
    {
        RawMaterials += amaunt;
        canPlay = !canPlay;

        if(HandsCards.Contains(card))
            HandsCards.Remove(card);

        HorizontalCardHolder.RemoveCard(card);

        contrario.GetCard();

        ApplyEffects();

        UIManager.Instance.UpdateUI();

        contrario.canPlay = !contrario.canPlay;
        GameManager.Instance.PlayerTurn = !GameManager.Instance.PlayerTurn;
    }
    public void PlayCard(Card card)
    {
        if(!canPlay)
            return;

        if(card.Effect.GetCost() > RawMaterials)
            return;

        RawMaterials -= card.Effect.GetCost();
        
        canPlay = !canPlay;
    
        if(card.FieldZone == "Challenge")
        {
            // HorizontalCardHolderChallenge.AddCard(card.Type, canHold: false);

            contrario.Effects.Add(HorizontalCardHolderChallenge.AddCard(card.Type, canHold: false).Effect);

        }

        else if(card.FieldZone == "Permanent")
        {
            // HorizontalCardHolderPermanent.AddCard(card.Type, canHold: false);

            Effects.Add(HorizontalCardHolderPermanent.AddCard(card.Type, canHold: false).Effect);

        }
            
        else if(card.FieldZone == "Temporal")
        {
            // HorizontalCardHolderTemporal.AddCard(card.Type, canHold: false);

            Effects.Add(HorizontalCardHolderTemporal.AddCard(card.Type, canHold: false).Effect);

        }

        else if(card.FieldZone == "Inmediate")
        {
            card.Effect.AplyEfect(this);
        }

        if(HandsCards.Contains(card))
            HandsCards.Remove(card);

        HorizontalCardHolder.RemoveCard(card);

        contrario.GetCard();

        ApplyEffects();

        UIManager.Instance.UpdateUI();

        contrario.canPlay = !contrario.canPlay;
        GameManager.Instance.PlayerTurn = !GameManager.Instance.PlayerTurn;

    }

    private IEnumerator ThiefCard()
    {
        yield return new WaitForSeconds(0.5f);

        // if(GameManager.Instance.DeckCard.CardsCount() > 0)
        HandsCards.Add(HorizontalCardHolder.AddCard(GameManager.Instance.DeckCard.GetCard(), isPc: isPc));
        
        yield return new WaitForSeconds(0.3f);
    }

    public void GetCard()
    {

        if(HandsCards.Count >= 7)
            return;

       StartCoroutine(ThiefCard());
    }
}

