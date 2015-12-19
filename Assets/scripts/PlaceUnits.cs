using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlaceUnits : MonoBehaviour
{
    public GameObject unitPlacePanel;

    public void fillUnitBar()
    {
        foreach(GameObject go in TurnController.playerUnits)
        {
            GameObject ImageButton = new GameObject();
            ImageButton.AddComponent<CanvasRenderer>();
            ImageButton.AddComponent<Image>().sprite = go.gameObject.GetComponent<attachableUnitDetails>()._class.spriteImage;
            ImageButton.AddComponent<Button>();
            ImageButton.AddComponent<Outline>().effectDistance = new Vector2(-3,3);

            ImageButton.transform.parent = unitPlacePanel.transform;



        }
    }
	
}
