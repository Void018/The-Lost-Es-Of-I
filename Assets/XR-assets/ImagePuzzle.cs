using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class ImagePuzzle : MonoBehaviour
{

    Vector2 result;
    List<Transform> childrenSource = new List<Transform>();
    List<Transform> children = new List<Transform>();
    Vector3 emptyPosition = new Vector3(0, 0, 0);
    float animationSpeed = 0.5f;
    bool isMoving;
    [SerializeField] GameObject logo;


    // public Text debug;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            childrenSource.Add(transform.GetChild(i));
        }

        var rnd = new System.Random();
        children = childrenSource.OrderBy(item => rnd.Next()).ToList<Transform>();
        int index = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                children[index].localPosition = new Vector3(i, j, 0);

                if (children[index].CompareTag("empty")) emptyPosition = children[index].localPosition;
                index++;
            }

        }

    }

    void Update()
    {
        if (isMoving) return;


        if (Input.touchCount == 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "image")
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        StartCoroutine(touchCalculation(Input.GetTouch(0).position));
                    }
                }
            }
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            Debug.Log("input");

            SwipeLeft();
        }
    }

    IEnumerator touchCalculation(Vector2 startPosition)
    {
        Vector2 endPosition = new Vector2();
        while (Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            endPosition = Input.GetTouch(0).position;
            yield return new WaitForEndOfFrame();
        }

        result = endPosition - startPosition;
        if (Mathf.Abs(result.x) > Mathf.Abs(result.y))
        {
            if (result.x > 0) SwipeRight();
            else SwipeLeft();
        }
        else
        {
            if (result.y > 0) SwipeUp();
            else SwipeDown();
        }

    }

    void SwipeLeft()
    {
        Debug.Log("swip");

        foreach (Transform child in children)
        {
            Vector3 newPosition = emptyPosition - Vector3.left;
            if (child.localPosition == newPosition)
            {
                StartCoroutine(moveCube(child, newPosition));

            }
            if (child.CompareTag("empty"))
            {
                child.localPosition = newPosition;
            }
        }
    }

    void SwipeUp()
    {
        foreach (Transform child in children)
        {
            Vector3 newPosition = emptyPosition + Vector3.up;
            if (child.localPosition == newPosition)
            {
                StartCoroutine(moveCube(child, newPosition));

            }
            if (child.CompareTag("empty"))
            {
                child.localPosition = newPosition;
            }
        }
    }
    void SwipeRight()
    {
        foreach (Transform child in children)
        {
            Vector3 newPosition = emptyPosition - Vector3.right;
            if (child.localPosition == newPosition)
            {
                StartCoroutine(moveCube(child, newPosition));

            }
            if (child.CompareTag("empty"))
            {
                child.localPosition = newPosition;
            }
        }
    }
    void SwipeDown()
    {
        foreach (Transform child in children)
        {
            Vector3 newPosition = emptyPosition + Vector3.down;
            if (child.localPosition == newPosition)
            {
                StartCoroutine(moveCube(child, newPosition));

            }
            if (child.CompareTag("empty"))
            {
                child.localPosition = newPosition;
            }
        }
    }

    IEnumerator moveCube(Transform cube, Vector3 newPosition)
    {
        isMoving = true;
        Debug.Log("enum");
        while (cube.localPosition != emptyPosition)
        {
            cube.localPosition = Vector3.MoveTowards(cube.localPosition, emptyPosition, animationSpeed);
            yield return new WaitForEndOfFrame();
        }
        emptyPosition = newPosition;
        isMoving = false;
        if (transform.GetChild(0).localPosition == new Vector3(0, 0, 0) &&
            transform.GetChild(1).localPosition == new Vector3(-1, 0, 0) &&
            transform.GetChild(2).localPosition == new Vector3(1, 0, 0) &&
            transform.GetChild(3).localPosition == new Vector3(0, 1, 0) &&
            transform.GetChild(4).localPosition == new Vector3(-1, 1, 0) &&
            transform.GetChild(5).localPosition == new Vector3(1, 1, 0) &&
            transform.GetChild(6).localPosition == new Vector3(0, -1, 0) &&
            transform.GetChild(7).localPosition == new Vector3(-1, -1, 0) &&
            transform.GetChild(8).localPosition == new Vector3(1, -1, 0))
        {
            logo.gameObject.SetActive(true);
        }
    }
}
