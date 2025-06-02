using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnergiaLimpiaEffect : IEffect
{
    public bool AplyEfect(PlayerStats player)
    {
        for(int i = player.Effects.Count - 1; i >= 0; i--)
        {
            if(player.Effects[i] is IRemobableEffect)
            {

                if(((IRemobableEffect)player.Effects[i]).IsChallemge())
                    return ((IRemobableEffect)player.Effects[i]).DesaplyEfect(player);

                // player.Effects.Remove(player.Effects[i]);
                // player.HorizontalCardHolderChallenge.RemoveCard(player.HorizontalCardHolderChallenge.cards[player.HorizontalCardHolderChallenge.cards.Count - 1]);

            }
        }

        return false;
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
