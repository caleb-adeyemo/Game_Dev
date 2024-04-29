using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent{
    Transform GetKitchenObjectFollowTransform();
    void SetKitchenObject(KitchenObject newKitchenObject);
    KitchenObject GetKitchenObject();
    void ClearKitchenObject();
    bool HasKitchenObject();
}
