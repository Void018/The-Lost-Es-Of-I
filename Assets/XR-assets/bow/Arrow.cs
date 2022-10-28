using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    bool inAir = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!inAir) return;
        transform.right = GetComponent<Rigidbody>().velocity;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile")) return;
        Debug.Log("other.name");
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().drag = 1000;
        GetComponent<Rigidbody>().useGravity = false;
        inAir = false;
        if (other.CompareTag("target1"))
        {

            GetComponentInParent<ArcherySystem>().AddScore(10);

        }
        else
        {


            GetComponentInParent<ArcherySystem>().AddScore(0);
        }
    }
}
