using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoundownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private void Start(){
        GameManager.Instance.OnStateChanged += OnStateChanged;
        hide();
    }

    private void OnStateChanged(object sender, System.EventArgs e){
        if(GameManager.Instance.IsCountDownToStartActive()){
            show();
        }else{
            hide();
        }

    }
    private void Update(){
        countDownText.text = Mathf.Ceil(GameManager.Instance.GetCountDownToStartTimer()).ToString(); 
    }

    private void show(){
        gameObject.SetActive(true);
    }
    private void hide(){
        gameObject.SetActive(false);
    }
}
