using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button menuButton;


    private void Awake(){
        resumeButton.onClick.AddListener( ()=>{
            GameManager.Instance.togglePauseGame();
        });

        menuButton.onClick.AddListener(()=>{
            Loader.Load(Loader.Scene.Menu);
        });
       

    }

    private void Start(){
        GameManager.Instance.OnGamePaused += GameManager_Instance_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_Instance_OnGameUnPaused;

        // Hide the pause screen by deafult
        hide();
    }

    private void GameManager_Instance_OnGamePaused(object sender, EventArgs e){
        show();
    }
    private void GameManager_Instance_OnGameUnPaused(object sender, EventArgs e){
        hide();
    }
    private void show(){
        gameObject.SetActive(true);
    }

    private void hide(){
        gameObject.SetActive(false);
    }

    private void testFunc(){
        Debug.Log("Thsi is a test for the Resume Button");
    }

    private void testFunc2(){
        Debug.Log("Thsi is a test for the Menu Button");
    }
}
