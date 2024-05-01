using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order{


    private Table table;
    private RecipeSo order;

    public Order(Table _table, RecipeSo _order){
        table = _table;
        order = _order;
    }
    public Table getOrdertable(){
        return table;
    }

    public RecipeSo getOrderRecipeSO(){
        return order;
    }

    public void setTable(Table _table){
        table = _table;
    }
    public void setOrderRecipeSO(RecipeSo recipe){
        order= recipe;
    }

}
