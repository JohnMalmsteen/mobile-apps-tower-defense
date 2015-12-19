using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UnitStore : MonoBehaviour
{

    attachableUnitDetails deets;
    GameObject playerUnit;

    public GameObject unitStoreMenu;
    public GameObject[] unitListImages;
    public Sprite defaultImage;

    public ToggleGroup myToggleGroup;

    public List<GameObject> playerUnits;

    public Sprite dwarf_fighter;
    public Sprite dwarf_ranger;
    public Sprite dwarf_sorcerer;

    public Button btnBuyUnit;
    public Button placeUnits;

    public void Start()
    {
        playerUnits = new List<GameObject>(GlobalVars.MAX_UNITS);
    }

    public bool display_UnitStoreMenu()
    {
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
        string classType = "";
        
        playerUnit = new GameObject();
        deets = playerUnit.gameObject.AddComponent<attachableUnitDetails>();

        foreach (Toggle toggle in myToggleGroup.ActiveToggles())
        {
            classType = toggle.name;
        }

        deets.unit = new Unit();
        deets.unit.BAB = 15;
        deets.unit.maxHealth = 30;
        deets.unit.health = 30;
        deets.unit.armorClass = 15;
        
        if (classType == "toggle_warrior")
        {
            deets._class = new Fighter();
            deets._class.spriteImage = dwarf_fighter;
            deets._class.level = 3;
        }

        if (classType == "toggle_ranger")
        {
            deets._class = new Ranger();
            deets._class.spriteImage = dwarf_ranger;
            deets._class.level = 3;
        }

        if (classType == "toggle_mage")
        {
            deets._class = new Sorcerer();
            deets._class.spriteImage = dwarf_sorcerer;
            deets._class.level = 3;
        }
                
        playerUnit.gameObject.name = deets._class.ToString();

        playerUnits.Add(playerUnit.gameObject);

        if (playerUnits.Count == GlobalVars.MAX_UNITS)
        {
            btnBuyUnit.interactable = false;
            placeUnits.interactable = true;
        }

        updateList();

    }// add_Unit

    public void updateList()
    {
        int count = 0;

        foreach(GameObject deets in playerUnits)
        {
            print(deets.gameObject.name + " : " + deets.gameObject.GetComponent<attachableUnitDetails>()._class);

            //print(deets.gameObject.GetComponent<attachableUnitDetails>()._class.spriteImage + " : " + count + " : ");

            unitListImages[count].gameObject.GetComponent<Image>().sprite = deets.gameObject.GetComponent<attachableUnitDetails>()._class.spriteImage;
        
            count++;
        }

    }

    public void clear_Unit()
    {
        playerUnits = new List<GameObject>();

        foreach (GameObject images in unitListImages)
        {
            images.GetComponent<Image>().sprite = defaultImage;
        }

        btnBuyUnit.interactable = true;
        placeUnits.interactable = false;

    }// clear_Unit

}// UnitStore
