using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : BaseCounter{

    public static event EventHandler OnItemTrashed;
    public override void Interact(Player player){
        player.GetKitchenObject().DestroySelf();
        OnItemTrashed?.Invoke(this, EventArgs.Empty);
    }
}
