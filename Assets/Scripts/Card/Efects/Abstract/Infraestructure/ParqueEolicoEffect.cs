using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParqueEolicoEffect : IEffect, IInfraestructureEffect
{

    public bool generate;
    public int Cost = 12;
    public bool AplyEfect(PlayerStats player)
    {
        player.GetSustentabilityPoints(2);

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
