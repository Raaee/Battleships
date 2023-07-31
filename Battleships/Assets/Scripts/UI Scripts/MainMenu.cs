using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour   {

    [SerializeField] private SceneControl sceneControl;
    private OnPanel currentPanel = OnPanel.MAIN_MENU;

    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text pressPlayText;
    [SerializeField] CanvasGroup mainMenuGroup;
    [SerializeField] CanvasGroup difficultySelectGroup;
    [SerializeField] GameObject buttonsPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject teamSelectPanel;

    private bool fading = false;
    private bool gameStart = false;

    private void Start() {
        StartMainMenu();
    }
    public void StartMainMenu() {
        currentPanel = OnPanel.MAIN_MENU;
        title.enabled = true;
        pressPlayText.enabled = true;
        buttonsPanel.SetActive(false);
        settingsPanel.SetActive(false);
        teamSelectPanel.SetActive(false);
        difficultySelectGroup.gameObject.SetActive(false);
    }
    void Update()   {
        if (currentPanel == OnPanel.MAIN_MENU) {
            if (Input.anyKeyDown) {
                Debug.Log("Key Pressed");
                currentPanel = OnPanel.BUTTONS;
                DisablePressPlay();
                OpenButtonsMenu();
            }
        }
        if (fading)
            FadeMainMenu();

        if (gameStart)
            FadeDifficultySelect();

    }
    public void FadeMainMenu() {
        if (mainMenuGroup.alpha > 0) {
            mainMenuGroup.alpha -= Time.deltaTime;
            if (mainMenuGroup.alpha == 0) {
                fading = false;
                mainMenuGroup.gameObject.SetActive(false);
                difficultySelectGroup.gameObject.SetActive(true);
            }
        }
        
    }
    public void FadeDifficultySelect() {
        if (difficultySelectGroup.alpha > 0) {
            difficultySelectGroup.alpha -= Time.deltaTime;
            if (difficultySelectGroup.alpha == 0) {
                fading = false;
                sceneControl.DelayedSceneChange(Scene.GAME, 1.25f);
            }
        }
        
    }
    public void DisablePressPlay() {
        pressPlayText.enabled = false;
    }

    public void CloseButtonsMenu() {
        buttonsPanel.SetActive(false);
    }
    public void OpenButtonsMenu() {
        buttonsPanel.SetActive(true);
    }
    public void StartGame() {
        fading = true;
    }
    public void QuitGame() {
        Application.Quit();
        Debug.Log("You will be back.");
    }
    public void OpenSettings() {
        CloseButtonsMenu();
        settingsPanel.SetActive(true);
    }
    public void CloseSettings() {
        title.enabled = true;
        settingsPanel.SetActive(false);
        OpenButtonsMenu();
    }
    public void SelectDifficulty(int difficulty) { // 0 = easy, 1 = med, 2 = hard//
        switch (difficulty) {
            case 0:
                Debug.Log("Easy Diffculty");
                break;
            case 1:
                Debug.Log("Medium Diffculty");
                break;
            case 2:
                Debug.Log("Hard Diffculty");
                break;
        }
    }

}
public enum OnPanel {
    MAIN_MENU,
    BUTTONS
}
