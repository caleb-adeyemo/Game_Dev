using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : BaseCounter
{
    public override void Interact(Player player){
        player.GetKitchenObject().DestroySelf();
    }
}
