using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    public Collider2D sensor;
    ContactFilter2D filter = new ContactFilter2D().NoFilter();

    void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            Interact();
        }
    }

    public void Interact() {
        var colliders = new List<Collider2D>();
        sensor.OverlapCollider(filter, colliders); // output to colliders list

        colliders
            .Where(c => (!c.CompareTag("Player")) && (c.GetComponent<Interactable>() != null))
            .ToList()
            .ForEach(c => c.GetComponents<Interactable>().ToList().ForEach(co => co.Interact()));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<Interactable>(out Interactable interactable)) {
            // interactable.Interact();
        }
        switch (other.tag) {
            case "Projectile":
                Destroy(other.gameObject);
                transform.position = Vector2.right * -8; // TODO: change respawn point to global Transform
                break;
        }
    }
}
