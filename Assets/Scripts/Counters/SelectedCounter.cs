using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField] private BaseCounter counter;
    [SerializeField] private GameObject visualObject;

    // Start
    private void Start()
    {
        Player.Instance.OnSelectingCounter  += Instance_OnselectingCounter;
    }

    private void Instance_OnselectingCounter(object sender, Player.OnSelectedCounterEventArgs e){
        if(e.selectedCounter == counter){
            show();
        }else{
            hide();
        }
    }

    private void show(){
        visualObject.SetActive(true);
    }

    private void hide(){
        visualObject.SetActive(false);
    }


}
