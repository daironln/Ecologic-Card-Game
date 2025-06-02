using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CumbreClimaticaEffect : IEffect
{
    public bool AplyEfect(PlayerStats player)
    {

        if(player.RawMaterials > 0)
        {
            player.DescartarResiduo(2);
            player.GetCard();

        }

        if(player.contrario.RawMaterials > 0)
        {
            player.contrario.DescartarResiduo(2);
            player.contrario.GetCard();

        }

        return true;
    }
    public int GetCost()
    {
        return 0;
    }

    public bool IsActive()
    {
        return true;
    }

    public void SetCard(Card card)
    {
        return;
    }
}
