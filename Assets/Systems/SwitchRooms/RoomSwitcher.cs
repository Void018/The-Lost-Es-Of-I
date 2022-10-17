using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class RoomSwitcher : MonoBehaviour {
    public Transform targetRoom;

    private CinemachineVirtualCamera targetCamera;
    private CinemachineVirtualCamera cam;
    private Transform player;
    private Transform targetPosition;
    private List<CinemachineVirtualCamera> allCams;

    void Start() {
        cam = transform.parent.GetComponentInChildren<CinemachineVirtualCamera>();
        player = FindObjectOfType<PlayerController>().transform;
        targetPosition = transform.GetChild(0);
        allCams = FindObjectsOfType<CinemachineVirtualCamera>().ToList();
        targetCamera = targetRoom.GetComponentInChildren<CinemachineVirtualCamera>();
    }

    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;

        player.transform.position = targetPosition.position;
        if (cam == targetCamera) return;

        print($"current: {cam.transform.parent.name}, target: {targetCamera.transform.parent.name}");
        allCams.ForEach(c => c.enabled = false);
        targetCamera.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        cam.enabled = false;
    }
}
