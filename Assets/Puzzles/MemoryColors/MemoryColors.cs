using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;
using UnityEngine.UI;

public class MemoryColors : MonoBehaviour {
    [HideInInspector]
    public UnityEvent<bool> SetClickMask = default;

    [Range(0, 1f)]
    public float cooldown;
    public GameObject indicatorsParent;

    private List<ColorTile> colorTiles;
    private List<Image> indicators;
    private List<int> sequence = new List<int>();
    private List<int> originalSequence = new List<int>();
    private bool onSequence;

    private int first { get { return sequence[0]; } }
    private int last { get { return sequence.Count > 0 ? sequence[sequence.Count - 1] : -1; } }

    void Start() {
        colorTiles = GetComponentsInChildren<ColorTile>().ToList();
        indicators = indicatorsParent.GetComponentsInChildren<Image>().ToList();

        for (int i = 0; i < colorTiles.Count; i++) {
            colorTiles[i].index = i;
            SetClickMask.AddListener(colorTiles[i].OnMaskChanged);
        }
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) GenerateSequence();
        if (Input.GetKeyDown(KeyCode.P)) {
            var s = "";
            sequence.ForEach(i => s += i.ToString() + ", ");
            print(s);
        }
    }

    public void GenerateSequence() {
        StartCoroutine(GenerateRandomPattern());
    }

    IEnumerator GenerateRandomPattern(int n = 5) {
        if (onSequence) yield break;

        SetClickMask?.Invoke(true);
        onSequence = true;
        sequence.Clear();
        originalSequence.Clear();
        indicators.ForEach(i => i.color = Color.white);

        for (int _ = 0; _ < n; _++) {
            // make sure the new tile isn't the last one
            var i = Random.Range(0, 4);
            while (i == last) {
                i = Random.Range(0, 4);
                yield return null;
            }

            sequence.Add(i);
            colorTiles[i].TileDown(generating: true);
            yield return new WaitForSeconds(cooldown);
        }
        originalSequence = sequence.ToList();
        SetClickMask?.Invoke(false);
        onSequence = false;
    }

    public void UpdateSequence(int index) {
        // ! TODO: what if sequence is empty
        if (index == first) {
            ContinueSequence();
        } else {
            ResetSequence();
        }
    }

    void ContinueSequence() {
        sequence.RemoveAt(0);

        for (int i = 0; i < 5 - sequence.Count; i++) {
            indicators[i].color = Color.green;
        }

        if (sequence.Count == 0) {
            // call this from UI:
            // GenerateSequence();
            Utils.Timeout(3000, GenerateSequence); // TODO: Remove, this is for development only
        }
    }

    void ResetSequence() {
        sequence = originalSequence.ToList();
        SoundManager.instance.Play(Sounds.Wrong);
        indicators.ForEach(i => i.color = Color.white);
    }
}

/*
 * TODO:
 - UI buttons for Starting the puzzle
 - Delete line 95
 - make prefabs
 - remove Update function
*/
