using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSo : ScriptableObject{
    public List<KitchenObjectsSO> KitchenObjectsSOList;
    public string recipeName;
}
