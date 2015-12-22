using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class guiController : MonoBehaviour
{
    public Text statusText;

    public GameObject btnCross;
    public GameObject unitPlacePanel;
    public GameObject initiativePanel;

    public void setStatusText(string msg)
    {
        statusText.text = msg;
    }

    

}
