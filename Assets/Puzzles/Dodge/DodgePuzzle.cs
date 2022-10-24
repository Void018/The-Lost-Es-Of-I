using System.Collections;
using UnityEditor.MPE;
using UnityEngine;

public class DodgePuzzle : MonoBehaviour {
    public GameObject sprite;
    public float minSpeed;
    public float maxSpeed;
    [Range(0f, 0.5f)]
    public float spawnCooldown;
    [Range(0f, 10f)]
    public float lifetime;

    private BoxCollider2D box;
    private Transform player;

    void Start() {
        box = GetComponentInChildren<BoxCollider2D>();
        player = FindObjectOfType<PlayerController>().transform;
        StartCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn() {
        while (true) {
            yield return new WaitForSeconds(spawnCooldown);
            InstantiateProjectile();
        }
    }

    private void InstantiateProjectile() {
        var projectile = Instantiate(sprite, GetSpawnPoint(), Quaternion.identity, transform).GetComponent<Rigidbody2D>();
        //
        SoundManager.instance.Play(Sounds.Spear);
        var sp = projectile.GetComponentInChildren<SpriteRenderer>();
        var origColor = sp.color;
        LeanTween.value(projectile.gameObject, 0.4f, 1, 0.2f).setOnUpdate((float a) => sp.color = origColor * a);
        projectile.transform.LeanRotateZ(projectile.rotation + Random.Range(0, 180), 0.2f)
            .setOnUpdate(r => projectile.rotation = r)
            .setOnComplete(
                () => {
                    projectile.GetComponent<BoxCollider2D>().enabled = true;
                    projectile.AddForce(projectile.transform.up * (minSpeed + (Random.value * (maxSpeed - minSpeed))));
                }
            );

        Destroy(projectile.gameObject, lifetime);
    }

    private Vector2 GetSpawnPoint() {
        Bounds bounds = box.bounds;

        float minX = bounds.size.x * -0.5f;
        float minY = bounds.size.y * -0.5f;

        var newPos = player.position;
        while (Vector2.Distance(player.position, newPos) < 5) {
            newPos = transform.TransformPoint(
                new Vector3(
                    Random.Range(minX, -minX),
                    Random.Range(minY, -minY)
                )
            );
        }
        return newPos;
    }
}
