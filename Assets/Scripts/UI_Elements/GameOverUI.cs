using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalNoOfOrdersComplete;
    private void Start(){
        GameManager.Instance.OnStateChanged += OnStateChanged;
        hide();
    }

    private void OnStateChanged(object sender, System.EventArgs e){
        if(GameManager.Instance.IsGameOver()){
            show();
            totalNoOfOrdersComplete.text = DeleveryManager.Instance.getSuccessfullOrdersNo().ToString();
        }else{
            hide();
        }

    }

    private void show(){
        gameObject.SetActive(true);
    }
    private void hide(){
        gameObject.SetActive(false);
    }
}
