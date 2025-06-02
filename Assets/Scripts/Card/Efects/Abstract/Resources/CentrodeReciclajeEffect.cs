using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CentrodeReciclajeEffect : IEffect, IRemobableEffect
{
    public Card card;
    private int _turns = 3;
    private bool _isActive = true;
    public bool AplyEfect(PlayerStats player)
    {
        // Debug.Log($"Centro reciclaje ApplyEffect {card}");
        _turns --;

        

        player.GetSustentabilityPoints(2);
        player.DescartarResiduo(1);
        player.GetRawMaterials(1);

        if(_turns <= 0)
        {
            
            DesaplyEfect(player);


        }

        return true;
    }

    public bool DesaplyEfect(PlayerStats player)
    {
        // Debug.Log($"Centro de reciclaje DesaplyEffect {card.Type}, {card.PlayerId}, {card.gameObject.name}");

        player.HorizontalCardHolderTemporal.RemoveCard(card);
        // player.Effects[player.Effects.IndexOf(this)] = null;

        _isActive = false;

        
        return true;
    }

    public int GetCost()
    {
        return 0;
    }

    public bool IsActive()
    {
        return _isActive;
    }

    public bool IsChallemge()
    {
        return false;
    }

    public void SetCard(Card card)
    {
        this.card = card;
    }
}
