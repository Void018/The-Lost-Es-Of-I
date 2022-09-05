using UnityEngine;

public class DialogInt : MonoBehaviour, Interactable {
    [TextArea]
    public string[] lines;

    public void Interact() {
        DialogManager.instance.StartDialog(lines);
    }
}
