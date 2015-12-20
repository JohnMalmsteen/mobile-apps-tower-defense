using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlaceUnits : MonoBehaviour
{
    public GameObject btnCross;
    public GameObject unitPlacePanel;
    public MouseTile mousetile;

    public void fillUnitBar()
    {
        btnCross.SetActive(true);
        GlobalVars.PLACE_MODE = true;

        foreach (GameObject go in TurnController.playerUnits)
        {
            GameObject ImageButton = new GameObject();
            ImageButton.AddComponent<CanvasRenderer>();
            ImageButton.AddComponent<Image>().sprite = go.gameObject.GetComponent<attachableUnitDetails>()._class.spriteImage;
            ImageButton.AddComponent<Button>();
            ImageButton.AddComponent<Outline>().effectDistance = new Vector2(-3,3);
            ImageButton.transform.SetParent(unitPlacePanel.transform);

            go.gameObject.GetComponent<attachableUnitDetails>()._class.unitButton = ImageButton;
        }
    }

    public void resetPlacing()
    {
        int count = 0;
        GlobalVars.PlacedCount = 0;
        unitPlacePanel.SetActive(true);

        GlobalVars.PLACE_MODE = true;
        mousetile.SelectedTile.gameObject.GetComponent<MeshRenderer>().enabled = false;

        foreach (GameObject go in TurnController.playerUnits)
        {
            TurnController.playerUnits[count].gameObject.GetComponent<attachableUnitDetails>()._class.unitButton.SetActive(true);
            Destroy(TurnController.playerUnits[count].gameObject.GetComponent<attachableUnitDetails>()._class.unitBoardModel);

            ++count;
        }
    }

    public void donePlacing()
    {
        unitPlacePanel.SetActive(false);
    }
	
}
