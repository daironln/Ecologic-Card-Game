using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReciclajeUrbanoEffect : IEffect
{
    public bool AplyEfect(PlayerStats player)
    {
        // player.DescartarResiduo(2);
        player.GetRawMaterials(2);

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
