using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EducacionAmbientalEffect : IEffect
{
    public bool AplyEfect(PlayerStats player)
    {
        player.GetCard();
        player.GetCard();

        player.DescartarResiduo(1);
        
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
