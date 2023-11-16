using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipUpgradeManager : MonoBehaviour
{

    public enum ToUpgrade
    {
        Engine, Shield, Gun, Radar
    }

    //public int[] inventory = new int[3];

    public int EngineRank;
    public int ShieldRank;
    public int GunRank;
    public int RadarRank;

    public GameObject upgradeScreen;
    public GameObject shipScreen;

    public ShipControl shipControl;

    public List<UpgradeModule> modules;
    public UpgradeModule[] currentlyAvailable;

    public UpgradeUI[] UIs;

    public bool InUpgradeScreen;

    private void Start()
    {
        InUpgradeScreen = false;
    }

    private void Update()
    {
        if (InUpgradeScreen)
        {
            //open upgrade screen
            if (Input.GetKeyDown(KeyCode.U))
            {
                Time.timeScale = 1.0f;
                InUpgradeScreen = false;
                shipScreen.SetActive(true);
                upgradeScreen.SetActive(false);
            }
        }
        else
        {
            Refresh();
            //close upgrade screen
            if (Input.GetKeyDown(KeyCode.U))
            {
                Time.timeScale = 0;
                InUpgradeScreen = true;
                shipScreen.SetActive(false);
                upgradeScreen.SetActive(true);
            }
        }

        Debug.Log(ResourceManager.resourceInventory[0] + "|" + ResourceManager.resourceInventory[1] + "|" + ResourceManager.resourceInventory[2]);

        if (Input.GetKeyDown(KeyCode.K))
        {
            for (int i = 0; i < ResourceManager.resourceInventory.Length; i++)
            {
                ResourceManager.resourceInventory[i]++;
                Refresh();
            }
        }

    }

    public void AssignUI(UpgradeUI ui, UpgradeModule module)
    {
        if (ui == null) return;
        ui.title.text = module.title;
        ui.description.text = module.description;
        ui.AssignModule(module);
        if (module.CanAfford() && module.unlocked == false)
        {
            ui.button.interactable = true;
        }
        else
        {
            ui.button.interactable = false;
            if (module.last == true && module.unlocked == true)
            {
                ui.title.text += " (Fully Unlocked)";
            }
        }
    }

    //Purchase an upgrade module
    public void Upgrade(UpgradeModule module)
    {
        switch(module.toUpgrade)
        {
            case ToUpgrade.Engine:
                EngineRank++;
                shipControl.Upgrade(ShipControl.UpgradeableComponent.engine);
                break;
            case ToUpgrade.Shield:
                ShieldRank++;
                shipControl.Upgrade(ShipControl.UpgradeableComponent.shield);
                break;
            case ToUpgrade.Gun:
                GunRank++;
                shipControl.Upgrade(ShipControl.UpgradeableComponent.gun);
                break;
            case ToUpgrade.Radar:
                RadarRank++;
                shipControl.Upgrade(ShipControl.UpgradeableComponent.radar);
                break;
            default:
                break;
        }

        for (int i = 0; i < module.ingredient.Count; i++)
        {
            ResourceManager.LoseResource(module.ingredient[i], module.amountNeeded[i]);
            /*switch (module.ingredient[i])
            {
                case ResourceManager.ResourceType.unrefinedMetals:
                    ResourceManager.LoseResource(ResourceManager.ResourceType.unrefinedMetals, module.amountNeeded[i]);
                    break;
                case ResourceManager.ResourceType.unprocessedOrganics:
                    inventory[1] -= module.amountNeeded[i];
                    break;
                case ResourceManager.ResourceType.componentChemicals:
                    inventory[2] -= module.amountNeeded[i];
                    break;
            }*/
        }

        module.unlocked = true;
        Refresh();
    }

    //Check to see if values changed due to player buying resources
    void Refresh()
    {
        for (int i = 0; i < currentlyAvailable.Length; i++)
        {
            if (modules[i] != null)
            {
                UpgradeModule currentModule = modules[i];
                while(currentModule.unlocked && currentModule.ranked && currentModule.last == false)
                {
                    currentModule = currentModule.nextRank[0];
                }
                currentlyAvailable[i] = currentModule;
            }
            else
            {
                currentlyAvailable[i] = null;
            }
        }

        for (int i = 0; i < UIs.Length; i++)
        {
            AssignUI(UIs[i], currentlyAvailable[i]);
        }

    }

    [Serializable] //Create upgrades in inspector
    public class UpgradeModule
    {
        [Header("Recipe")]
        public List<ResourceManager.ResourceType> ingredient;
        public List<int> amountNeeded;

        [Header("Info")]
        public ToUpgrade toUpgrade;
        public bool unlocked;
        public bool ranked;
        public bool last;
        public List<UpgradeModule> nextRank;

        [Header("UI")]
        public string title;
        public string description;

        public bool CanAfford()
        {
            bool passing = true;
            for (int i = 0; i < ingredient.Count; i++)
            {
                /*switch(ingredient[i])
                {
                    case ResourceManager.ResourceType.unrefinedMetals:
                        if (A < amountNeeded[i]) passing = false;
                        break;
                    case ResourceManager.ResourceType.unprocessedOrganics:
                        if (B < amountNeeded[i]) passing = false;
                        break;
                    case ResourceManager.ResourceType.componentChemicals:
                        if (C < amountNeeded[i]) passing = false;
                        break;
                }*/
                if (ResourceManager.resourceInventory[(int)ingredient[i]] < amountNeeded[i]) passing = false;
            }
            return passing;
        }

    }

    [Serializable] //A collection of UI Assets to link with upgraded module data
    public class UpgradeUI
    {
        public Button button;
        public TextMeshProUGUI title;
        public TextMeshProUGUI description;
        public ShipUpgradeManager shipUpgradeManager;

        public void AssignModule(UpgradeModule module)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => shipUpgradeManager.Upgrade(module));
        }

    }

}
