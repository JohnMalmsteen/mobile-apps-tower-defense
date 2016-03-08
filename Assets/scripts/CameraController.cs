using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject battleCamera;
    Vector3 cameraDefaultPosition;
    Quaternion cameraDefaultRotation;

    public void Start()
    {
        cameraDefaultPosition = Camera.main.gameObject.transform.position;
        cameraDefaultRotation = Camera.main.gameObject.transform.rotation;
    }

    public void ShowBattle(GameObject perp, GameObject victim)
    {
        //StartCoroutine(ShowBattleEvent(perp, victim));
    }

    IEnumerator ShowBattleEvent(GameObject perp, GameObject victim)
    {
        Vector3 midpoint = (perp.transform.position + victim.transform.position) / 2;

        print("Midpoint: " + midpoint);

        midpoint = new Vector3(midpoint.x, 0.5f, midpoint.z);

        battleCamera.transform.position = midpoint;

        print("battleCamera.transform.position: " + battleCamera.transform.position);

        //iTween.MoveTo(Camera.main.gameObject, battleCamera.gameObject.transform.position, 1.0f);

        yield return new WaitForSeconds(2.0f);

        resetCamera();
    }

    public void resetCamera()
    {
        print("Reset");

        Camera.main.gameObject.transform.position = cameraDefaultPosition;
        Camera.main.gameObject.transform.rotation = cameraDefaultRotation;
    }

}
