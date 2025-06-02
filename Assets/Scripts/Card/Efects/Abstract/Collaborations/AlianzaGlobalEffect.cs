using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AlianzaGlobalEffect : IEffect
{

    public bool AplyEfect(PlayerStats player)
    {

        if(player.SustainabilityPoints > 0)
        {
            player.GetRawMaterials(2);
            player.ReduceSustentabilityPoints(10);

        }

        if(player.contrario.SustainabilityPoints > 0)
        {
            player.contrario.GetRawMaterials(2);
            player.contrario.ReduceSustentabilityPoints(10);

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
