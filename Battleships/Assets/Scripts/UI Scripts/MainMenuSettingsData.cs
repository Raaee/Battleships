using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSettingsData : MonoBehaviour
{
    public static MainMenuSettingsData instance = null;




    public bool MMSD_camShakeVal { get; set; } = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }

}
