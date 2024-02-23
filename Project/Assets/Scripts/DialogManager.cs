using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int letterPerSecond;

    public event Action onShowDialog;
    public event Action onHideDialog;

    public static DialogManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    Dialog dialog;
    int currLine = 0;

    bool isTyping;

    public IEnumerator ShowDialog(Dialog dialog) {
        yield return new WaitForEndOfFrame();
        onShowDialog?.Invoke();

        this.dialog = dialog;
        dialogBox.SetActive(true);
        StartCoroutine(typeDialog(dialog.Lines[0]));
    }

    public void HandleUpdate() {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping) {
            ++currLine;
            if (currLine < dialog.Lines.Count) {
                StartCoroutine(typeDialog(dialog.Lines[currLine]));
            }
            else {
                dialogBox.SetActive(false);
                currLine = 0;
                onHideDialog?.Invoke();
            }
        }
    }

    public IEnumerator typeDialog(string line) {
        isTyping = true;
        dialogText.text = "";
        foreach (var c in line.ToCharArray()) {
            dialogText.text += c;
            yield return new WaitForSeconds(1f / letterPerSecond);
        }
        isTyping = false;
    }
}
