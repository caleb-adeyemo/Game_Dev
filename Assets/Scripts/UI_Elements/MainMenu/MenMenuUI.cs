using UnityEngine;
using UnityEngine.UI;

public class MenMenuUI : MonoBehaviour
{
    [SerializeField] private Button level_1_Button;
    [SerializeField] private Button level_2_Button;

    [SerializeField] private Button level_3_Button;

    [SerializeField] private Button quitButton;

    private void Awake(){
        level_1_Button.onClick.AddListener(()=> {
            Loader.Load(Loader.Scene.GamePlay);
        });

        level_2_Button.onClick.AddListener(()=> {
            Loader.Load(Loader.Scene.Level_2);
        });

        level_3_Button.onClick.AddListener(()=> {
            Loader.Load(Loader.Scene.Level_3);
        });

        quitButton.onClick.AddListener(()=> {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }
}
