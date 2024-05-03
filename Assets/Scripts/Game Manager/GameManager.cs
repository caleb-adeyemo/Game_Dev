using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    // Instance 
    public static GameManager Instance { get; private set; }
    public enum GameState{
        waitingToStart,
        CountDownToStart,
        GamePlaying,
        Gameover
    }

    private GameState gameState;

    private bool IsGamePaused = false;
    private float waitngToStartTimer = 1f;
    private float countDownToStart = 3f;
    private float gamePlaying;
    [SerializeField] private float gamePlayingMax = 20f;

    // Event
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

    public void Awake(){
        Instance = this;
        gameState = GameState.waitingToStart;
    }

    public void Start(){
        GameInput.Instance.OnPauseAction += OnPauseAction;
    }

    private void OnPauseAction(object sender, System.EventArgs e){
        // Pause the game
        togglePauseGame();
    }
    public void Update(){
        switch(gameState){
            case GameState.waitingToStart:
                waitngToStartTimer -= Time.deltaTime;
                if (waitngToStartTimer < 0f){
                    gameState = GameState.CountDownToStart;
                    OnStateChanged?.Invoke(this, new EventArgs());
                }             
                break;
            case GameState.CountDownToStart:
                countDownToStart -= Time.deltaTime;
                if (countDownToStart < 0f){
                    gamePlaying = gamePlayingMax;
                    gameState = GameState.GamePlaying;
                    OnStateChanged?.Invoke(this, new EventArgs());
                }             
                break;
            case GameState.GamePlaying:
                gamePlaying -= Time.deltaTime;
                if (gamePlaying < 0f){
                    gameState = GameState.Gameover;
                    OnStateChanged?.Invoke(this, new EventArgs());
                }             
                break;
            case GameState.Gameover:
                // Do Nothing!!!    
                break;
        }
    }

    public bool IsGamePlaying(){
        return gameState == GameState.GamePlaying;
    }
    public bool IsCountDownToStartActive(){
        return gameState == GameState.CountDownToStart;
    }
    public bool IsGameOver(){
        return gameState == GameState.Gameover;
    }
    public float GetCountDownToStartTimer(){
        return countDownToStart;
    }

    public float GetGamePlayingTemerNormalized(){
        return gamePlaying/gamePlayingMax;
    }

    public void togglePauseGame(){
        // tuggle the pause
        IsGamePaused = !IsGamePaused;
        // If the game is paused stop all 'Time.deltaTime' multipler
        if (IsGamePaused){
            Time.timeScale = 0f;
            // Fire off the pause event
            OnGamePaused?.Invoke(this, new EventArgs());
        }else{ // Set Multipler to be 'normal'
            Time.timeScale = 1f;
            // Fire off the unpause event
            OnGameUnPaused?.Invoke(this, new EventArgs());
        }
        
    }
}
