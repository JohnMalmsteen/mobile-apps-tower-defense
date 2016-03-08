using UnityEngine;
using System.Collections;

public class LoseScreen : MonoBehaviour {

    public GameObject lookPoint;

	void Start () {

        StartCoroutine(CameraStuff());
    }

    IEnumerator CameraStuff()
    {
        iTween.LookTo(Camera.main.gameObject, lookPoint.transform.position, 15.0f);

        yield return new WaitForSeconds(15.0f);

        iTween.MoveTo(Camera.main.gameObject, lookPoint.transform.position, 4.0f);

        yield return new WaitForSeconds(4.0f);

        Application.LoadLevel("menu");
    }
	
}
