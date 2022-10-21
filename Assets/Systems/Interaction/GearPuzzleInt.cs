using UnityEngine;

public class GearPuzzleInt : MonoBehaviour, Interactable {
    GearsPuzzleManager gearManager;

    private void Start() {
        gearManager = FindObjectOfType<GearsPuzzleManager>();
    }
    public void Interact() {
        if (gearManager.state != puzzleState.solved) {
            gearManager.UpdatePuzzleState(puzzleState.started);
        }
    }

    private void Update() {
        if (Input.GetMouseButton(1) && gearManager.state != puzzleState.solved) {
            gearManager.UpdatePuzzleState(puzzleState.deactive);
        }
    }
}
