using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearControl : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public bool CCW = true;
    public float radius = 1f;

    GearsPuzzleManager manager;

    private void Start()
    {
        manager = FindObjectOfType<GearsPuzzleManager>();
    }

    void Update()
    {
        if (CCW)
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        else
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        if(gameObject.tag == "Final Gear" && rotationSpeed != 0)
        {
            manager.UpdatePuzzleState(puzzleState.solved);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GearControl gear = collision.GetComponent<GearControl>();
        if (collision.gameObject.tag == "Leader Gear")
        {
            RotateGear(gear);
        }
        else if (collision.gameObject.tag == "Gear" && gear.rotationSpeed > rotationSpeed)
        {
            RotateGear(gear);
        }
    }

    private void RotateGear(GearControl gear)
    {
        CCW = !gear.CCW;
        rotationSpeed = gear.rotationSpeed / (radius/gear.radius);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GearControl gear = collision.GetComponent<GearControl>();
        if (gameObject.tag != "Leader Gear" && gear.rotationSpeed == 0f && rotationSpeed == 0f)
        {
            rotationSpeed = 0f;
        }
        else if (collision.gameObject.tag == "Gear" && gear.rotationSpeed > rotationSpeed)
        {
            RotateGear(gear);
        }
        else if (collision.gameObject.tag == "Leader Gear")
        {
            RotateGear(gear);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.tag != "Leader Gear")
            rotationSpeed = 0f;
    }
}
