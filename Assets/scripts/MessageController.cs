using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageController : MonoBehaviour
{
    public Text statusMessage;
    public GameObject panel;

    public void PlaceMessage()
    {
        panel.SetActive(true);
        statusMessage.text = "Place Units within the BLUE area of the board";
    }

    public void UpdateStatusMessage(string message)
    {
        panel.SetActive(true);
        statusMessage.text = message;
    }
	
}
