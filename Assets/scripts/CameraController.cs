using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Camera Camera1;
    public Camera Camera2;

    public GameObject battleCamera;

    ScriptManager scriptmanager;
    TurnController turnController;
    Vector3 cameraDefaultPosition;
    Quaternion cameraDefaultRotation;
    
    bool original = true;

    public void Start()
    {
        scriptmanager = GameObject.Find("ScriptManager").GetComponent<ScriptManager>();

        cameraDefaultPosition = Camera.main.gameObject.transform.position;
        cameraDefaultRotation = Camera.main.gameObject.transform.rotation;

        turnController = scriptmanager.turnController;
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

    public void switchCamera()
    {
        if (original)
        {
            Camera1.gameObject.SetActive(false);
            Camera2.gameObject.SetActive(true);

            Camera1.enabled = false;
            Camera2.enabled = true;

            original = false;
        }
        else
        {
            Camera1.gameObject.SetActive(true);
            Camera2.gameObject.SetActive(false);

            Camera1.enabled = true;
            Camera2.enabled = false;

            original = true;
        }

        turnController.DrawHealth();
        //Camera1.camera.
    }

}
