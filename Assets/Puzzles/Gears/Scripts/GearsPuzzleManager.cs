using UnityEngine;
using TMPro;

public class GearsPuzzleManager : MonoBehaviour {
    [SerializeField]
    private GameObject puzzleInterface;
    public bool hasGear = false;
    public GameObject missingGear;

    public TextMeshProUGUI progressText;

    public puzzleState state;

    public void UpdatePuzzleState(puzzleState newState) {
        state = newState;

        switch (newState) {
            case puzzleState.started:
                if (hasGear)
                    missingGear.SetActive(true);
                puzzleInterface.SetActive(true);
                break;
            case puzzleState.solved:
                puzzleInterface.SetActive(false);
                progressText.text += "E";
                break;
            case puzzleState.deactive:
                puzzleInterface.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void DisableUI()
    {
        UpdatePuzzleState(puzzleState.deactive);
    }
}
public enum puzzleState { deactive, started, solved };
