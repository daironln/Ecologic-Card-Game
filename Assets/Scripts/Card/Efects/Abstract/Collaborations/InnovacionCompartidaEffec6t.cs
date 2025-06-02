using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InnovacionCompartidaEffec6t : IEffect
{
    public bool AplyEfect(PlayerStats player)
    {
        Debug.Log("Innovacion compartida");

        foreach(var e in player.contrario.Effects)
        {
            if(e is CentrodeReciclajeEffect || e is GranjaSolarEffect || e is HuertoUrbanoEffect)
            {
                player.HorizontalCardHolderTemporal.AddCard((e is CentrodeReciclajeEffect) ? EcologicCradType.CentroReciclaje : (e is GranjaSolarEffect) ? EcologicCradType.GranjaSolar : EcologicCradType.HuertoUrbano, canHold: false);
                
                return true;
            }
        }

        return false;
    }

    public bool IsActive()
    {
        return true;
    }

    public void SetCard(Card card)
    {
        return;
    }

    public int GetCost()
    {
        return 0;
    }
}
