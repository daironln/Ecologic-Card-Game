using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
      public bool AplyEfect(PlayerStats player);
      public void SetCard(Card card);
      public bool IsActive();
      public int GetCost();
}
