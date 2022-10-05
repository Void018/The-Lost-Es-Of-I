using Cinemachine;
using UnityEngine;

public class RoomSwitcher : MonoBehaviour {
    public Transform targetPosition;

    private CinemachineVirtualCamera cam;
    private Transform player;
    private CinemachineVirtualCamera targetCamera;

    void Start() {
        cam = transform.parent.GetComponentInChildren<CinemachineVirtualCamera>();
        player = FindObjectOfType<PlayerController>().transform;
        targetCamera = targetPosition.parent.GetComponentInChildren<CinemachineVirtualCamera>();
    }

    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;

        player.transform.position = targetPosition.position;
        Debug.Log(targetPosition.name);
        cam.enabled = false;
        targetCamera.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        cam.enabled = false;
    }
}
