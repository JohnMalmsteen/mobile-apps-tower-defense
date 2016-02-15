using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlaceUnits : MonoBehaviour
{
    ScriptManager scriptManager;
    guiController guiController;
    MouseTile mouseTile;
    TurnController turnController;
    InfoPanel infopanel;

    public void Start()
    {
        scriptManager = GameObject.Find("ScriptManager").GetComponent<ScriptManager>();
        guiController = scriptManager.guiController;
        turnController = scriptManager.turnController;
        mouseTile = scriptManager.mouseTile;
        infopanel = scriptManager.infopanel;
    }

    public void fillUnitBar()
    {
        guiController.btnCross.SetActive(true);

        GlobalVars.PLACE_MODE = true;

        foreach (GameObject go in TurnController.playerUnits)
        {
            GameObject ImageButton = new GameObject();
            ImageButton.AddComponent<CanvasRenderer>();
            ImageButton.AddComponent<Image>().sprite = go.gameObject.GetComponent<attachableUnitDetails>()._class.spriteImage;
            ImageButton.AddComponent<Button>();
            ImageButton.AddComponent<Outline>().effectDistance = new Vector2(-3,3);
            ImageButton.transform.SetParent(guiController.unitPlacePanel.transform);

            go.gameObject.GetComponent<attachableUnitDetails>()._class.unitButton = ImageButton;
        }
    }

    public void resetPlacing()
    {
        int count = 0;
        GlobalVars.PlacedCount = 0;
        guiController.unitPlacePanel.SetActive(true);

        GlobalVars.PLACE_MODE = true;
        mouseTile.SelectedTile.gameObject.GetComponent<MeshRenderer>().enabled = false;

        foreach (GameObject go in TurnController.playerUnits)
        {
            TurnController.playerUnits[count].gameObject.GetComponent<attachableUnitDetails>()._class.unitButton.SetActive(true);
            Destroy(TurnController.playerUnits[count].gameObject.GetComponent<attachableUnitDetails>()._class.unitBoardModel);

            ++count;
        }
    }

    public void donePlacing()
    {
        guiController.setStatusText("Battle");
        guiController.unitPlacePanel.SetActive(false);
        turnController.FinishedPlacing();

        infopanel.HideAreas();
    }
	
}
