using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour   {

    [SerializeField] private SceneControl sceneControl;
    [SerializeField] private EnemyAiStats enemyAiStats;
    private OnPanel currentPanel = OnPanel.MAIN_MENU;

    [SerializeField] TMP_Text title;
    [SerializeField] CanvasGroup pressPlayText;
    [SerializeField] CanvasGroup mainMenuGroup;
    [SerializeField] CanvasGroup difficultySelectGroup;
    [SerializeField] GameObject buttonsPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject teamSelectPanel;

    private bool fading = false;
    private bool gameStart = false;

   

    private bool camShakeVal = false;
    void Awake()
    {
        
    }


    private void Start() {
        StartMainMenu();
    }
    public void StartMainMenu() {
        currentPanel = OnPanel.MAIN_MENU;
        DisplayTitle(true);
        pressPlayText.gameObject.SetActive(true);
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
            sceneControl.ChangeScene(Scene.GAME);

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
            if (difficultySelectGroup.alpha == 0.5f) {
                gameStart = false;
                // sceneControl.DelayedSceneChange(Scene.GAME, 1.25f);
                sceneControl.ChangeScene(Scene.GAME);
            }
        }
        
    }

    public void OnCameraShakeValueChanged(bool isOn)
    {
        camShakeVal = isOn;
    }
    public void DisablePressPlay() {
        pressPlayText.gameObject.SetActive(false);
    }
    public void DisplayTitle(bool b) {
        title.gameObject.SetActive(b);
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
      //  DisplayTitle(false);
        settingsPanel.SetActive(true);
    }
    public void CloseSettings() {
        settingsPanel.SetActive(false);
      //  DisplayTitle(true);
        OpenButtonsMenu();
    }
    public void SelectDifficulty(int difficulty) { // 0 = easy, 1 = med, 2 = hard//
        switch (difficulty) {
            case 0:
                Debug.Log("Easy Diffculty");
                enemyAiStats.SetPercentageToHit(0.15f);
                gameStart = true;
                break;
            case 1:
                Debug.Log("Medium Diffculty");
                enemyAiStats.SetPercentageToHit(0.33f);
                gameStart = true;
                break;
            case 2:
                Debug.Log("Hard Diffculty");
                enemyAiStats.SetPercentageToHit(0.55f);
                gameStart = true;
                break;
        }
    }

    public bool GetCamShakeValMaiMenu()
    { return camShakeVal;
    }

}
public enum OnPanel {
    MAIN_MENU,
    BUTTONS
}
