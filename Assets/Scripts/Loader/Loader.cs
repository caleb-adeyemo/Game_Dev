using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader{

    public enum Scene{
        Menu,
        LoadingScene,
        GamePlay,
        Level_2,
        Level_3
    }
    public static Scene targetScene;

    public static void Load(Scene _targetScene){
        targetScene = _targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallBack(){
        SceneManager.LoadScene(targetScene.ToString());
    }
}
