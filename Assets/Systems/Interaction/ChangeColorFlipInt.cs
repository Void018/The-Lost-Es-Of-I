using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorFlipInt : MonoBehaviour, Interactable {
    public float speed;

    SpriteRenderer sp;
    bool interact = false;

    void Start() {
        sp = GetComponent<SpriteRenderer>();
    }


    void Update() {
        if (!interact) return;
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        sp.color = Random.ColorHSV();
    }

    public void Interact() {
        interact = !interact;
    }
}
