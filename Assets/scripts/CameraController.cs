using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Camera[] AltCam;

    int cameraCount = 0;
    int max = 0;

    public void Start()
    {
        max = AltCam.Length;

        print("max cam: " + max);
    }

    public void ChangeView()
    {
        for(int i = 0; i < max; i++)
        {
            AltCam[i].enabled = false;
        }

        if (cameraCount < max)
            AltCam[cameraCount].enabled = true;
        else
            cameraCount = -1;

        cameraCount++;
    }

}
