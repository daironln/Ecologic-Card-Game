using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResiduosToxicosEffect : IEffect, IRemobableEffect
{
    public Card card;
    private bool _isActive = true;
    public bool AplyEfect(PlayerStats player)
    {
        Debug.Log($"ResidupsToxicos to {player.isPc}");

        ElegirOpc(player);

        return true;
    }

    private void ElegirOpc(PlayerStats playerStats)
    {
        if(playerStats.HandsCards.Count < 3)
        {
            playerStats.GetResiduos(3);
            DesaplyEfect(playerStats);
                
            return;
        }

        if(playerStats.isPc)
        {
            var a = Random.Range(0, 101);

            if(a < 50)
            {
                playerStats.Descard(playerStats.HorizontalCardHolder.cards[Random.Range(0, playerStats.HorizontalCardHolder.cards.Count)]);
                playerStats.Descard(playerStats.HorizontalCardHolder.cards[Random.Range(0, playerStats.HorizontalCardHolder.cards.Count)]);

                DesaplyEfect(playerStats);


                return;
            }

            playerStats.GetResiduos(3);
            DesaplyEfect(playerStats);


            return;
        }

        void Descartar()
        {

            playerStats.Descard(playerStats.HorizontalCardHolder.cards[Random.Range(0, playerStats.HorizontalCardHolder.cards.Count)]);
            playerStats.Descard(playerStats.HorizontalCardHolder.cards[Random.Range(0, playerStats.HorizontalCardHolder.cards.Count)]);

            UIManager.Instance.SelectionPanneButnA.onClick.RemoveAllListeners();
            UIManager.Instance.SelectionPanneButnB.onClick.RemoveAllListeners();

            DesaplyEfect(playerStats);


            UIManager.Instance.FadeOutSelectionPanne();

        }

        void Recibir()
        {
            playerStats.GetResiduos(3);

            UIManager.Instance.SelectionPanneButnA.onClick.RemoveAllListeners();
            UIManager.Instance.SelectionPanneButnB.onClick.RemoveAllListeners();

            DesaplyEfect(playerStats);


            UIManager.Instance.FadeOutSelectionPanne();

        }

        UIManager.Instance.FadeInSelectionPanne();

        UIManager.Instance.CardTittle.SetText(card.CardTittle);
        UIManager.Instance.CardTipe.SetText(card.Clasification);
        UIManager.Instance.CardDescription.SetText(card.CardDescription);

        
        UIManager.Instance.SelectionPanneButnA.onClick.AddListener(Descartar);
        UIManager.Instance.SelectionPanneButnB.onClick.AddListener(Recibir);

        UIManager.Instance.SelectionPanneTittle.SetText("Efecto de RESIDUOS TOXICOS");
        UIManager.Instance.SelectionPanneDescripcion.SetText("Debes elegir si descartar 2 cartas aleatorias de tu mano (A) o recibir 3 Residuos (B)");
        UIManager.Instance.SelectionPanneImage.sprite = GameManager.Instance.Images[(int)EcologicCradType.ResiduosToxicos];
    }

    

    

    public bool DesaplyEfect(PlayerStats player)
    {
        player.HorizontalCardHolderChallenge.RemoveCard(card);
        // player.Effects[player.Effects.IndexOf(this)] = null;

        _isActive = false;

        
        return true;
    }

    public int GetCost()
    {
        return 0;
    }
    public bool IsActive()
    {
        return _isActive;
    }

    public bool IsChallemge()
    {
        return true;
    }

    public void SetCard(Card card)
    {
        this.card = card;
    }
}
