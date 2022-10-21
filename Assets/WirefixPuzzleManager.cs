using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This manages each of the two versions indivisually and must have 2 different instances
public class WirefixPuzzleManager : MonoBehaviour
{

    // Number of double tiles in certain puzzle
    public int numberOfTiles = 3;

    // Put line drawer and all the elements of the puzzle in one (deactive by default) Object
    public GameObject puzzleInterface;

    public LinesDrawer linesDrawer;

    // The enum is defined on GearsPuzzleManager.cs
    public puzzleState state;

    public void UpdatePuzzleState(puzzleState newState)
    {
        state = newState;

        switch (newState)
        {
            case puzzleState.started:
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
