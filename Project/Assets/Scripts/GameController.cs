using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState {FreeRoam, Dialog, Battle}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    GameState currState;

    private void Start() {
        DialogManager.Instance.onShowDialog += () => {
            currState = GameState.Dialog;
        };
        DialogManager.Instance.onHideDialog += () => {
            if (currState == GameState.Dialog) {
                currState = GameState.FreeRoam;
            }
        };
    }

    private void Update() {
        if (currState == GameState.FreeRoam) {
            playerController.HandleUpdate();
        } else if (currState == GameState.Dialog) {
            DialogManager.Instance.HandleUpdate();
        } else if (currState == GameState.Battle) {

        }
    }
}
