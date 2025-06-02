using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class TransporteElectricoEffect : IEffect, IInfraestructureEffect
{

    public bool generate;
    public int Cost = 18;
    public bool AplyEfect(PlayerStats player)
    {
        player.GetSustentabilityPoints(3);

        player.GetResiduos(1);

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
