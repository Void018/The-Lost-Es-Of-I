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
    [SerializeField] GameObject arrow;
    // [SerializeField] Button shootButton;
    [SerializeField] bool isAiming;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnPointerDown(PointerEventData data)
    {
        isAiming = true;
        StartCoroutine(Shoot());
    }

    public void OnPointerUp(PointerEventData data)
    {
        isAiming = false;
    }


    IEnumerator Shoot()
    {

        while (isAiming)
        {
            shootPower = (shootPower % maxShootPower) + powerIncreaseSpeed * Time.deltaTime;
            shootPowerBar.fillAmount = shootPower / maxShootPower;
            yield return new WaitForEndOfFrame();
        }

        GameObject _arrow = Instantiate(arrow, arrowPosition.position, arrowPosition.rotation);
        _arrow.GetComponent<Rigidbody>().AddForce(_arrow.transform.right * shootPower * 100);

    }


}
