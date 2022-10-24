using UnityEngine;

public class DialogInt : MonoBehaviour, Interactable {
    [TextArea]
    public string[] lines;
    public bool deleteOnRead = false;
    GearsPuzzleManager gearManager;

    private void Start() {
        gearManager = FindObjectOfType<GearsPuzzleManager>();
    }

    public void Interact() {
        DialogManager.instance.StartDialog(lines);
        if (deleteOnRead) {
            Destroy(gameObject);
        }
        if (gameObject.tag == "Gear") {
            gearManager.hasGear = true;
        }
    }
}
