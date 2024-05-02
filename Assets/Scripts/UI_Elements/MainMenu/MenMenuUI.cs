using UnityEngine;
using UnityEngine.UI;

public class MenMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake(){
        playButton.onClick.AddListener(()=> {
            Loader.Load(Loader.Scene.GamePlay);
        });

        quitButton.onClick.AddListener(()=> {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }
}
