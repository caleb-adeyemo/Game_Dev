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
    private float waitngToStartTimer = 1f;
    private float countDownToStart = 3f;
    private float gamePlaying;
    private float gamePlayingMax = 20f;

    // Event
    public event EventHandler OnStateChanged;

    public void Awake(){
        Instance = this;
        gameState = GameState.waitingToStart;
    }

    public void Update(){
        Debug.Log("State: " + gameState);
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
}
