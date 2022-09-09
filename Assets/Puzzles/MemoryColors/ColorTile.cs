using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTile : MonoBehaviour {

    public AudioClip clip;

    [HideInInspector]
    public int index;

    private SpriteRenderer sp;
    private AudioSource audioSource;
    private MemoryColors memoryColors;
    private float h, s, v; // for changing color
    private bool clickMask = true;

    void Start() {
        sp = GetComponent<SpriteRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        memoryColors = GetComponentInParent<MemoryColors>();

        audioSource.clip = clip;
        Color.RGBToHSV(sp.color, out h, out s, out v);
    }

    public void TileDown(bool generating = false) {
        StopAllCoroutines();
        TileUp();
        sp.color = Color.HSVToRGB(h, s, v * 2);
        audioSource.Play();
        StartCoroutine(SetTimeout(0.25f, () => TileUp()));
        if (!generating)
            memoryColors.UpdateSequence(index);
    }

    public void TileUp() {
        sp.color = Color.HSVToRGB(h, s, v);
    }

    private void OnMouseDown() {
        if (clickMask) return;
        TileDown();
    }

    private void OnMouseUp() {
        TileUp();
    }

    IEnumerator SetTimeout(float timeout, System.Action action) {
        yield return new WaitForSeconds(timeout);
        action();
    }

    public void OnMaskChanged(bool newValue) {
        clickMask = newValue;
    }
}
