using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArcherySystem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private float shootPower;
    [SerializeField] float maxShootPower;
    [SerializeField] float powerIncreaseSpeed;
    [SerializeField] Transform arrowPosition;
    [SerializeField] Image shootPowerBar;
    [SerializeField] Animator bowAnimator;
    [SerializeField] GameObject arrow;
    // [SerializeField] Button shootButton;
    [SerializeField] bool isAiming;
    public int score;
    public int shootedArrows;
    public bool won;
    [SerializeField] TMPro.TMP_Text scoreUI;
    public Transform target;
    [SerializeField] float distance, minDistance;
    [SerializeField] TMPro.TMP_Text distanceUI;
    [SerializeField] LayoutGroup arrows;
    [SerializeField] GameObject e;

    // Start is called before the first frame update
    void Start()
    {
        scoreUI.text = "Aim for the bullseye!";

    }
    private void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("target1").transform;
        }
        distance = Vector3.Distance(target.position, Camera.main.transform.position);
        distanceUI.text = (distance < minDistance) ? "You can't shoot from here! \n" + distance : Mathf.FloorToInt(distance) + "m";
        distanceUI.color = (distance < minDistance) ? Color.red : Color.green;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (won) return;

        if (shootedArrows >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                arrows.transform.GetChild(i).gameObject.SetActive(true);
            }
            scoreUI.text = "Aim for the bullseye!";
            shootedArrows = 0;
            score = 0;
            foreach (var child in transform.GetComponentsInChildren<Transform>())
            {

                if (child.gameObject != this.gameObject) Destroy(child.gameObject);
            }
            return;
        }

        isAiming = true;
        StartCoroutine(Shoot());
    }

    public void OnPointerUp(PointerEventData data)
    {
        isAiming = false;

    }


    IEnumerator Shoot()
    {
        bowAnimator.Play("aim");
        while (isAiming)
        {
            shootPower = (shootPower % maxShootPower) + powerIncreaseSpeed * Time.deltaTime;
            shootPowerBar.fillAmount = shootPower / maxShootPower;
            yield return new WaitForEndOfFrame();
        }
        bowAnimator.Play("shoot");

        arrows.transform.GetChild(shootedArrows).gameObject.SetActive(false);
        shootedArrows++;
        GameObject _arrow = Instantiate(arrow, arrowPosition.position, arrowPosition.rotation, parent: transform);
        _arrow.GetComponent<Rigidbody>().AddForce(_arrow.transform.right * shootPower * 100);
        arrowPosition.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.15f);
        arrowPosition.gameObject.SetActive(true);
        shootPower = 0;
        shootPowerBar.fillAmount = 0;

    }
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreUI.text = "Current Score" + score;
        if (score >= 100)
        {
            won = true;
            Win();
        }
    }

    void Win()
    {
        e.SetActive(true);
    }

}
