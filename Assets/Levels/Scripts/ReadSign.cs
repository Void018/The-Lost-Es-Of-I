using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadSign : MonoBehaviour {

    public string[] lines;

    private DialogManager dialogManager;

    void Start() {
        dialogManager = FindObjectOfType<DialogManager>();
    }


    void Update() {

    }

    void OnTriggerEnter2D(Collider2D other) {
        dialogManager.StartDialog(lines);
    }
}
