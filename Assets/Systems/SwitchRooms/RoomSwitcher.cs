using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class RoomSwitcher : MonoBehaviour {
    public Transform targetRoom;

    private Transform player;
    private Transform targetPosition;
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineConfiner2D confiner;
    private PolygonCollider2D polygonCollider;

    void Start() {
        player = FindObjectOfType<PlayerController>().transform;
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
        polygonCollider = targetRoom.GetComponentInChildren<PolygonCollider2D>();
        targetPosition = transform.GetChild(0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;

        player.transform.position = targetPosition.position;
        confiner.m_BoundingShape2D = polygonCollider;
    }
}
