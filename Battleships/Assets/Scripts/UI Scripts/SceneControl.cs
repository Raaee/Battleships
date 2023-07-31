using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneControl : MonoBehaviour   {

    public static SceneControl instance = null;
    [HideInInspector] public UnityEvent onGameStart;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }

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
    public IEnumerator DelayedSceneChange(Scene scene, float timeInSec) {
        yield return PeteHelper.GetWait(timeInSec);
        ChangeScene(scene);
    }
}
public enum Scene {
    MAIN_MENU,
    GAME
}
