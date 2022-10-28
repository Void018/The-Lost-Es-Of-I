using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostE : MonoBehaviour
{
    [SerializeField] GameObject E1, E2, E3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "E1")
                {
                    E1.SetActive(true);
                    hit.collider.GetComponent<Animator>().Play("collect");
                    hit.collider.transform.parent.parent.gameObject.SetActive(false);
                }
                if (hit.collider.tag == "E2")
                {
                    E2.SetActive(true);
                    hit.collider.GetComponent<Animator>().Play("collect");
                }
                if (hit.collider.tag == "E3")
                {
                    E3.SetActive(true);
                    hit.collider.GetComponent<Animator>().Play("collect");
                }
            }
        }
    }
}
