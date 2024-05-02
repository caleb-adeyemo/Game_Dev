using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private Image clockImage;

    private void Update(){
       clockImage.fillAmount = GameManager.Instance.GetGamePlayingTemerNormalized();

       if(GameManager.Instance.GetGamePlayingTemerNormalized() < 0.25){
        clockImage.color = Color.red;
       }
       else if(GameManager.Instance.GetGamePlayingTemerNormalized() < 0.5){
        clockImage.color = Color.yellow;
       }
       else{
        clockImage.color = Color.blue;
       }
    }
}
