using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SequiaExtremaEffect : IEffect, IRemobableEffect
{

    private int _turns = 5;
    public Card card;
    private bool _isActive = true;
    public bool AplyEfect(PlayerStats player)
    {
        if(_turns <= 0)
        {
            DesaplyEfect(player);
            return false;
        }

        //Bloquear generacion

        foreach(var e in player.Effects)
        {
            if(e is IInfraestructureEffect)
            {
                if(e is EdificioVerdeEffect)
                    ((EdificioVerdeEffect)e).generate = false;

                if(e is ParqueEolicoEffect)
                    ((ParqueEolicoEffect)e).generate = false;

                if(e is TransporteElectricoEffect)
                    ((TransporteElectricoEffect)e).generate = false;
            }
        }

        _turns --;

        return true;
    }

    public int GetCost()
    {
        return 0;
    }

    public bool DesaplyEfect(PlayerStats player)
    {

        foreach(var e in player.Effects)
        {
            if(e is IInfraestructureEffect)
            {
                if(e is EdificioVerdeEffect)
                    ((EdificioVerdeEffect)e).generate = true;

                if(e is ParqueEolicoEffect)
                    ((ParqueEolicoEffect)e).generate = true;

                if(e is TransporteElectricoEffect)
                    ((TransporteElectricoEffect)e).generate = true;
            }
        }

        player.HorizontalCardHolderChallenge.RemoveCard(card);
        // player.Effects[player.Effects.IndexOf(this)] = null;

        _isActive = false;

        return true;
    }

    public bool IsChallemge()
    {
        return true;
    }

    public void SetCard(Card card)
    {
        this.card = card;
    }

    public bool IsActive()
    {
        return _isActive;
    }
}
