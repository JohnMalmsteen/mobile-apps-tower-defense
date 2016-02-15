using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UnitStore : MonoBehaviour
{
    public PlaceUnits pu;
    public InfoPanel infoPanel;

    attachableUnitDetails deets;
    GameObject playerUnit;

    public Text GoldText;
    public Text WarCostText;
    public Text RangeCostText;
    public Text MageCostText;
    public GameObject playerPieces;
    public GameObject unitStoreMenu;
    public GameObject[] unitListImages;
    public Sprite defaultImage;

    public ToggleGroup myToggleGroup;

    public Button btnBuyUnit;
    public Button placeUnits;

    ScriptManager scriptManager;
    SpritesModels spriteModels;

    public void Start()
    {
        WarCostText.text = "" + Fighter.FighterCost;
        RangeCostText.text = "" + Ranger.RangerCost;
        MageCostText.text = "" + Sorcerer.SorcererCost;

        scriptManager = GameObject.Find("ScriptManager").GetComponent<ScriptManager>();
        spriteModels = scriptManager.spriteModels;
    }       

    public bool display_UnitStoreMenu()
    {
        GoldText.text = "" + GlobalVars.GoldCount;

        if (unitStoreMenu.activeSelf)
        {
            unitStoreMenu.SetActive(false);
            return false;
        }
        else
        {
            unitStoreMenu.SetActive(true);
            return true;
        }        
    }

    public void add_Unit()
    {
        bool canBuy = true;

        string classType = "";
        
        foreach (Toggle toggle in myToggleGroup.ActiveToggles())
        {
            classType = toggle.name;
        }

        if (classType == "toggle_warrior")
        {
            if ((GlobalVars.GoldCount -= Fighter.FighterCost) < 0)
            {
                canBuy = false;
                GlobalVars.GoldCount -= Fighter.FighterCost;
            }
        }

        if (classType == "toggle_ranger")
        {
            if ((GlobalVars.GoldCount -= Ranger.RangerCost) < 0)
            {
                canBuy = false;
                GlobalVars.GoldCount -= Ranger.RangerCost;
            }
        }

        if (classType == "toggle_mage")
        {
            if ((GlobalVars.GoldCount -= Sorcerer.SorcererCost) < 0)
            {
                canBuy = false;
                GlobalVars.GoldCount -= Sorcerer.SorcererCost;
            }
        }

        if (canBuy)
        {
            playerUnit = new GameObject();

            playerUnit.transform.parent = playerPieces.transform;

            deets = playerUnit.gameObject.AddComponent<attachableUnitDetails>();

            deets.unit = new Unit();
            deets.unit.BAB = 15;
            deets.unit.maxHealth = 30;
            deets.unit.health = 30;
            deets.unit.armorClass = 15;

            if (classType == "toggle_warrior")
            {
                deets._class = new Fighter();
                deets._class.spriteImage = spriteModels.dwarf_fighter;
                deets._class.model = spriteModels.model_dwarf_fighter;
                deets._class.level = 3;
            }

            if (classType == "toggle_ranger")
            {
                deets._class = new Ranger();
                deets._class.spriteImage = spriteModels.dwarf_ranger;
                deets._class.model = spriteModels.model_dwarf_ranger;
                deets._class.level = 3;
            }

            if (classType == "toggle_mage")
            {
                deets._class = new Sorcerer();
                deets._class.spriteImage = spriteModels.dwarf_sorcerer;
                deets._class.model = spriteModels.model_dwarf_sorcerer;
                deets._class.level = 3;
            }

            playerUnit.gameObject.name = deets._class.ToString();

            TurnController.playerUnits.Add(playerUnit.gameObject);

            if (TurnController.playerUnits.Count == GlobalVars.MAX_UNITS)
            {
                btnBuyUnit.interactable = false;
                placeUnits.interactable = true;
            }

            GoldText.text = "" + GlobalVars.GoldCount;

            updateList();

        }

    }// add_Unit

    public void updateList()
    {
        int count = 0;

        foreach(GameObject deets in TurnController.playerUnits)
        {
            unitListImages[count].gameObject.GetComponent<Image>().sprite = deets.gameObject.GetComponent<attachableUnitDetails>()._class.spriteImage;
        
            count++;
        }

    }

    public void clear_Unit()
    {
        TurnController.playerUnits = new List<GameObject>();

        foreach (GameObject images in unitListImages)
        {
            images.GetComponent<Image>().sprite = defaultImage;
        }

        GlobalVars.GoldCount = GlobalVars.StartGold;
        GoldText.text = "" + GlobalVars.GoldCount;

        btnBuyUnit.interactable = true;
        placeUnits.interactable = false;

    }// clear_Unit

    public void finishedBuying()
    {
        display_UnitStoreMenu();
        pu.fillUnitBar();

        infoPanel.Show_PlaceUnitText();

    }// finishedBuying()

}// UnitStore
