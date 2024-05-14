using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalNoOfOrdersComplete;

    private void Start(){
        DeleveryCounter.OnSucesssfullDerlevery += OnSucesssfullDerlevery;
    }

    private void OnSucesssfullDerlevery(object sender, DeleveryCounter.E e){
        totalNoOfOrdersComplete.text = e.no_of_successfullDeleveries.ToString();
    }

}
