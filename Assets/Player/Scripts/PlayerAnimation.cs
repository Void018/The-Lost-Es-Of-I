using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    private Animator animator;

    private string currentAnimation;

    void Start() {
        animator = GetComponent<Animator>();
    }

    public void Animate(Vector2 dir) {
        // we will use horizontal animation unless the movement is vetical only
        if (dir.x != 0) {
            if (dir.x > 0) Play("MoveRight");
            else Play("MoveLeft");
        } else  if (dir.y != 0) {
            if (dir.y > 0) Play("MoveUp");
            else Play("MoveDown");
        } else Play("Idle");
    }

    private void Play(string anim) {
        if (anim == currentAnimation) return;
        animator.Play(anim, 0, 0f);
        currentAnimation = anim;
    }
}
