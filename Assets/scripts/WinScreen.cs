using UnityEngine;
using System.Collections;

public class WinScreen : MonoBehaviour {

    void Start()
    {
        StartCoroutine(CameraStuff());
    }

    IEnumerator CameraStuff()
    {
        yield return new WaitForSeconds(4.0f);

        Application.LoadLevel("menu");
    }
}
