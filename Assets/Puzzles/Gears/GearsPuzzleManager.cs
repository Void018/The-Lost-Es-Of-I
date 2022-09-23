using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearsPuzzleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject puzzleInterface;
    public bool hasGear = false;
    public GameObject missingGear;

    public puzzleState state;

    public void UpdatePuzzleState(puzzleState newState)
    {
        state = newState;

        switch (newState)
        {
            case puzzleState.started:
                if (hasGear)
                    missingGear.SetActive(true);
                puzzleInterface.SetActive(true);
                break;
            case puzzleState.deactive:
            case puzzleState.solved:
                puzzleInterface.SetActive(false);
                break;
            default:
                break;
        }
    }

}
public enum puzzleState { deactive, started, solved };
