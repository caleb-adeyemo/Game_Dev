using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveParticle : MonoBehaviour
{
    [SerializeField] private Stove stove;
    [SerializeField] private GameObject particlesGameObject;

    private void Start(){
        stove.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(object sender, Stove.OnstateChangedEventArgs e){
        bool showVisual = e.state == Stove.State.Frying || e.state == Stove.State.Fried;
        particlesGameObject.SetActive(showVisual);
    }
}
