using System.Collections;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour {
    public static DialogManager instance;

    public GameObject dialogPanel;
    private TMP_Text textComponent;
    public float textSpeed = 0.05f;
    [HideInInspector] public bool onDialog;

    private int index;
    private string[] lines;

    private void Awake() {
        instance = this;
    }

    void Start() {
        textComponent = dialogPanel.GetComponentInChildren<TMP_Text>();
    }

    void Update() {
        if (!dialogPanel.activeSelf) return;

        if (Input.GetMouseButtonDown(0)) {
            if (textComponent.text == lines[index]) {
                NextLine();
            } else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialog(string[] lines) {
        // if there a dialog right now, cancel dialog request
        if (dialogPanel.activeSelf) {
            return;
        } else {
            dialogPanel.SetActive(true);
        }
        this.lines = lines;
        index = 0;
        onDialog = true;
        StartCoroutine(TypeLine());
    }

    public void EndDialog() {
        onDialog = false;
        lines = null;
        dialogPanel.SetActive(false);
    }

    IEnumerator TypeLine() {
        textComponent.text = "";
        foreach (char c in lines[index].ToCharArray()) {
            textComponent.text += c;
            SoundManager.instance.Play(Sounds.Sans);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine() {
        if (index < lines.Length - 1) {
            index++;
            StartCoroutine(TypeLine());
        } else {
            EndDialog();
        }
    }
}
