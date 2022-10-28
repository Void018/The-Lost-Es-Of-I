using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirefixInt : MonoBehaviour, Interactable
{
    WirefixPuzzleManager manager;

    private void Start()
    {
        manager = FindObjectOfType<WirefixPuzzleManager>();
    }
    public void Interact()
    {
        if (manager.state != puzzleState.solved)
        {
            manager.UpdatePuzzleState(puzzleState.started);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(1) && manager.state != puzzleState.solved)
        {
            manager.UpdatePuzzleState(puzzleState.deactive);
        }
    }
}
