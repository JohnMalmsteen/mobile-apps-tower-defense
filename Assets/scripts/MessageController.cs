using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageController : MonoBehaviour
{
    public Text statusMessage;
    public GameObject panel;

    public void UpdateStatusMessage(string message)
    {
        panel.SetActive(true);
        statusMessage.text = message;
    }
	
}
