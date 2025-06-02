using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EdificioVerdeEffect : IEffect, IInfraestructureEffect
{
    public int Cost = 8;
    public bool generate = true;

    public bool AplyEfect(PlayerStats player)
    {
        player.GetSustentabilityPoints(1);
        player.DescartarResiduo(1);

        return true;
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
        return Cost;
    }

    public bool Generate()
    {
        return generate;
    }
}
