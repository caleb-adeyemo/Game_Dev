using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour, IKitchenObjectParent
{
    public static Npc Instance {get; private set;}

    [SerializeField] private GameObject spawnPoint; // Waiter's hands
    private KitchenObject kitchenObject; // Kitchen object waiter is carrying

    private Table npcTable;
    public NpcController npcController;

    public Table GetTable(){
        return npcTable;
    }
    public void SetTable(Table table){
        npcTable = table;
    }



        public Transform GetKitchenObjectFollowTransform(){
            return spawnPoint.transform;
        }

        public void SetKitchenObject(KitchenObject newKitchenObject){
            kitchenObject = newKitchenObject;
        }

        public KitchenObject GetKitchenObject(){
            return kitchenObject;
        }

        public void ClearKitchenObject(){
            kitchenObject = null;
        }

        public bool HasKitchenObject(){
            return kitchenObject != null;
        }
}
