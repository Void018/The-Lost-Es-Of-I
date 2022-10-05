using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    public List<Sound> sounds;

    private AudioSource source;
    private Dictionary<Sounds, AudioClip> soundsDict = new Dictionary<Sounds, AudioClip>();

    private void Awake() {
        instance = this;
    }

    private void Start() {
        source = GetComponent<AudioSource>();
        sounds.ForEach(s => soundsDict.Add(s.sound, s.clip));
    }


    public void Play(Sounds sound) {
        // source.clip = soundsDict[sound];
        // source.Play();
        source.PlayOneShot(soundsDict[sound]);
    }
}

[System.Serializable]
public class Sound {
    public Sounds sound;
    public AudioClip clip;
}

public enum Sounds {
    Sans,
    Wrong,
    DoorClosed,
    Spear,
}
