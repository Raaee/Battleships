using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour   {

    [SerializeField] private SceneControl sceneControl;
    private OnPanel currentPanel = OnPanel.MAIN_MENU;

    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text pressPlayText;
    [SerializeField] CanvasGroup mainMenuGroup;
    [SerializeField] GameObject buttonsPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject teamSelectPanel;

    private bool fading = false;

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
        if (fading) {
            if (mainMenuGroup.alpha > 0) {
                mainMenuGroup.alpha -= Time.deltaTime;
                if (mainMenuGroup.alpha == 0) {
                    fading = false;
                    sceneControl.ChangeScene(Scene.GAME);
                }
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
        // this starts the game. going to the team selection first
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
}
public enum OnPanel {
    MAIN_MENU,
    BUTTONS
}
