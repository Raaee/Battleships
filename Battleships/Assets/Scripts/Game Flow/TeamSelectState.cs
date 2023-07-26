using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSelectState : GameState    {

    [SerializeField] CanvasGroup teamSelectPanel;
    //private TeamSide teamSide;
    private bool fading = false;
    private bool teamSelected = false;

    public override void OnStateEnter() {
      //  teamSelectPanel.alpha = 0;
      //  FadeIn();
    }
    public override void OnStateUpdate() {
        if (fading) {
            if (teamSelectPanel.alpha < 1) {
                teamSelectPanel.alpha += Time.deltaTime;
                if (teamSelectPanel.alpha >= 1)
                    fading = false;
            }
        }
        if (teamSelected) { 
            ShowTeamDesc();
        }
    }
    public override void OnStateExit() {

    }
    public override void TurnComplete() {

    }
    public void FadeIn() {
        fading = true;
    }
    public void SelectedTeam(string team) {
        switch(team) {
            case "Dark":
            case "dark":
            case "Duskmare":
            case "duskmare":
            case "Duskmares":
            case "duskmares":
                teamSelected = true;
                teamSide = TeamSide.DUSKMARE;
                break;
            case "Light":
            case "light":
            case "Luminid":
            case "Luminids":
            case "luminid":
            case "luminids":
                teamSelected = true;
                teamSide = TeamSide.LUMINID;
                break;
        }
    }
    public void ShowTeamDesc() {
        if (teamSide == TeamSide.DUSKMARE) {
            Debug.Log("Dusk");
        } else if (teamSide == TeamSide.LUMINID) {
            Debug.Log("Lumin");
        } else {
            Debug.Log("No team selected.");
        }
    }

    public override IEnumerator WaitForSec(float time) {
        yield return null;
    }
}
