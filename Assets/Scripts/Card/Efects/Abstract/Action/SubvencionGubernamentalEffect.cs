using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SubvencionGubernamentalEffect : IEffect
{
    public bool AplyEfect(PlayerStats player)
    {
        player.GetRawMaterials(3);

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
