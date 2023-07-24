using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour   {
    public void ChangeScene(int sceneIndex) {
        SceneManager.LoadScene(sceneBuildIndex: sceneIndex);
    }
    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName: sceneName);
    }
    public void ChangeScene(Scene scene) {
        switch(scene) {
            case Scene.MAIN_MENU:
                SceneManager.LoadScene(sceneName: "MainMenu");
                break;
            case Scene.GAME:
                SceneManager.LoadScene(sceneName: "GameScene");
                break;
        }
    }
}
public enum Scene {
    MAIN_MENU,
    GAME
}
