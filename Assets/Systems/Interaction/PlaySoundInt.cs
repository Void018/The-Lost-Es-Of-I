using UnityEngine;

public class PlaySoundInt : MonoBehaviour, Interactable {

    void Start() {

    }


    void Update() {

    }
    public void Interact() {
        SoundManager.instance.Play(Sounds.DoorClosed);
    }
}
