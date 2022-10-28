using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPuzzle : MonoBehaviour
{

    // touch vars
    Vector2 result;
    float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "world")
                {
                    transform.Rotate(0, 0, Input.GetTouch(0).deltaPosition.x * -rotationSpeed);
                }
                if (hit.collider.tag == "region")
                {
                    GetComponent<Animator>().Play("win");
                }
            }
        }
    }
}

