using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HuertoUrbanoEffect : IEffect, IRemobableEffect
{
    public Card card;
    private int _turns = 5;
    private bool _isActive = true;
    public bool AplyEfect(PlayerStats player)
    {
        _turns --;

       

        player.GetSustentabilityPoints(1);
        player.DescartarResiduo(1);

        if(_turns <= 0)
        {
            DesaplyEfect(player);


        }


        return true;

        
    }

    public int GetCost()
    {
        return 0;
    }


    public bool DesaplyEfect(PlayerStats player)
    {
        player.HorizontalCardHolderTemporal.RemoveCard(card);
        // player.Effects[player.Effects.IndexOf(this)] = null;

        _isActive = false;

        
        return true;
    }

    public bool IsActive()
    {
        return _isActive;
    }

    public bool IsChallemge()
    {
        return false;
    }

    public void SetCard(Card card)
    {
        this.card = card;
    }
}
