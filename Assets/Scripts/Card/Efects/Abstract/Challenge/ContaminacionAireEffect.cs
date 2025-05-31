using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ContaminacionAireEffect : IEffect, IRemobableEffect
{

    public Card card;
    private bool _isActive = true;

    public bool AplyEfect(PlayerStats player)
    {
        player.ReduceSustentabilityPoints(10);

        return true;
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
