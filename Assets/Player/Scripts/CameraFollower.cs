using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollowPlayer : MonoBehaviour {
    public Transform Target;
    public float MaxDistance = 4f;
    public float Speed = 4;

    private new Camera camera;
    private Vector3 viewportPoint;
    private Vector3 desiredPosition;

    void Start() {
        camera = GetComponent<Camera>();
        viewportPoint = new Vector3(0.5f, 0.5f, Mathf.Abs(transform.position.z - Target.position.z));
        desiredPosition = transform.position;
    }

    void LateUpdate() {
        FollowTarget();
    }

    private void FollowTarget() {
        Vector3 centerPoint = camera.ViewportToWorldPoint(viewportPoint);
        Vector3 deltaPosition = Target.position - centerPoint;

        if (deltaPosition.sqrMagnitude > MaxDistance * MaxDistance)
            desiredPosition = transform.position + deltaPosition;

        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Speed * Time.deltaTime);
    }

}
