using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public static Npc Instance {get; private set;}

    private Table npcTable;
    public NpcController npcController;

    public Table GetTable(){
        return npcTable;
    }
    public void SetTable(Table table){
        npcTable = table;
    }
}
