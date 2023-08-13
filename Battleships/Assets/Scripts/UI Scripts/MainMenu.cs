using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

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

    private void Start() {
        Initialize();
    }
    public void Initialize() {
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
    }
    private IEnumerator FadeOutCanvasGroup(CanvasGroup cg, float fadeDuration) {
        float time = 0f;
        float startAlpha = cg.alpha;

        while (time < fadeDuration) {
            float newAlpha = Mathf.Lerp(startAlpha, 0f, time / fadeDuration);
            cg.alpha = newAlpha;
            time += Time.deltaTime;
            yield return null;
        }
        // Ensure the alpha is set to 0 at the end to avoid any floating-point inaccuracies.
        cg.alpha = 0f;
    }
    private IEnumerator FadeInCanvasGroup(CanvasGroup cg, float fadeDuration) {
        float time = 0f;
        float startAlpha = 0;
        cg.alpha = 0f;

        while (time < fadeDuration) {
            float newAlpha = Mathf.Lerp(startAlpha, 1.0f, time / fadeDuration);
            cg.alpha = newAlpha;
            time += Time.deltaTime;
            yield return null;
        }
        // Ensure the alpha is set to 0 at the end to avoid any floating-point inaccuracies.
        cg.alpha = 1.0f;
    }
    IEnumerator FadeOutMainMenu() {
        yield return StartCoroutine(FadeOutCanvasGroup(mainMenuGroup, 1.5f));
        mainMenuGroup.gameObject.SetActive(false);
        difficultySelectGroup.gameObject.SetActive(true);
        StartCoroutine(FadeInDifficultyMenu());
    }
    IEnumerator FadeInDifficultyMenu() {
        yield return StartCoroutine(FadeInCanvasGroup(difficultySelectGroup, 1f));
        // sceneControl.DelayedSceneChange(Scene.GAME, 1.25f);
        // sceneControl.ChangeScene(SceneEnum.GAME);
    }
    public void OnCameraShakeValueChanged(bool isOn)    {
        Debug.Log(MainMenuSettingsData.instance.MMSD_camShakeVal);
        MainMenuSettingsData.instance.MMSD_camShakeVal = isOn;
        Debug.Log(MainMenuSettingsData.instance.MMSD_camShakeVal);
    }

    public void ToggleFullscreen(bool toggleState)
    {
        Screen.fullScreen = !Screen.fullScreen;
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
        //   fading = true;
        StartCoroutine(FadeOutMainMenu());        
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
        float enemyAIPercentage = 0.01f;
        int amtRounds = 0;

        switch (difficulty) {
            case 0:
                enemyAIPercentage = 0.15f;
                amtRounds = 49;
                break;
            case 1:
                enemyAIPercentage = 0.30f;
                amtRounds = 39;
                break;
            case 2:
                enemyAIPercentage = 0.50f;
                amtRounds = 29;
                break;
            default:
                Debug.Log("buttons for enemy diff not properly set");
                break;
        }

        enemyAiStats.SetPercentageToHit(enemyAIPercentage);
        enemyAiStats.SetAmountOfRounds(amtRounds);
        StartGameOfficial();
    }

    private void StartGameOfficial()    {
        sceneControl.ChangeScene(SceneEnum.GAME);
    }
}
public enum OnPanel {
    MAIN_MENU,
    BUTTONS
}
