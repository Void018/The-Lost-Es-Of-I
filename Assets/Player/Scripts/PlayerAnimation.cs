using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private Animator animator;
    private string currentAnimation;
    private Transform sensor;
    private float sensorRadius;

    void Start() {
        animator = GetComponent<Animator>();
        sensor = transform.Find("InteractSensor");
        sensorRadius = sensor.transform.position.magnitude;
    }

    public void Animate(Vector2 dir) {
        // we will use horizontal animation unless the movement is vetical only
        if (dir.x != 0) {
            if (dir.x > 0) {
                Play("MoveRight");
            } else {
                Play("MoveLeft");
            }
            sensor.transform.localPosition = Vector2.right * Mathf.Sign(dir.x) * sensorRadius;
        } else if (dir.y != 0) {
            if (dir.y > 0) {
                Play("MoveUp");
            } else {
                Play("MoveDown");
            }
            sensor.transform.localPosition = Vector2.up * Mathf.Sign(dir.y) * sensorRadius;
        } else {
            // Play("Idle");
            // sensor.transform.localPosition = Vector2.down * sensorRadius;
        }
    }

    private void Play(string anim) {
        if (anim == currentAnimation) return;
        animator.Play(anim, 0, 0f);
        currentAnimation = anim;
    }
}
