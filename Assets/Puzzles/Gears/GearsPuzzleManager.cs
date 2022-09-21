using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearsPuzzleManager : MonoBehaviour
{
    public static GearsPuzzleManager instance;
    [SerializeField]
    private GameObject puzzleInterface;
    public bool hasGear = false;
    public GameObject missingGear;

    public puzzleState state;
    void Awake()
    {
        instance = this;
        UpdatePuzzleState(puzzleState.started);
    }

    public void UpdatePuzzleState(puzzleState newState)
    {
        state = newState;

        switch (newState)
        {
            case puzzleState.started:
                puzzleInterface.SetActive(true);
                if (hasGear)
                    missingGear.SetActive(true);
                UpdatePuzzleState(puzzleState.unsolved);
                break;
            case puzzleState.unsolved:
                break;
            case puzzleState.solved:
                break;
            default:
                break;
        }
    }
}
public enum puzzleState { unsolved, started, solved };
