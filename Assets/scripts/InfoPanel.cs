using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanel : MonoBehaviour
{
    public Text txtTitle;
    public Text txtMessage;
    public GameObject infoWindow;
    public GameObject placeableArea;

    bool firstTimePlacing = false;

    public void Start()
    {
        Show_BuyUnitText();
    }

    public void Show_BuyUnitText()
    {
        string title = "Choose your units";
        string message = "Choose between the three available classes of units. Each unit has it's own strengths and weaknesses.\n\nOnce you are happy with your choice you will be brought to the battlefield and prompted to place your units.";

        infoWindow.gameObject.SetActive(true);

        txtTitle.text = title;
        txtMessage.text = message;
    }

    public void Show_PlaceUnitText()
    {

        string title = "Place your units";
        string message = "Place each of your units on the board. You can reset them by using the button on the top left of the screen.\n\nYou can only place your units within the area marked BLUE on your side of the board.\n\nOnce you have played your units the initiative of each unit is randomized and the game begins.";

        infoWindow.gameObject.SetActive(true);

        txtTitle.text = title;
        txtMessage.text = message;

        firstTimePlacing = true;
    }

    public void HideAreas()
    {
        placeableArea.SetActive(false);
    }

    public void CloseWindow()
    {
        if (firstTimePlacing)
            GlobalVars.MOUSE = true;

        infoWindow.gameObject.SetActive(false);

        //display_UnitStoreMenu();
        //pu.fillUnitBar();
    }
}
