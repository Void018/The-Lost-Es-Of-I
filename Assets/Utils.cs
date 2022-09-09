using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Utils : MonoBehaviour {
    public static async void Timeout(int milliseconds, System.Action action) {
        await Task.Delay(milliseconds);
        action();
    }
}
