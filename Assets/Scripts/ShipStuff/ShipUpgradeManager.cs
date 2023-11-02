using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipUpgradeManager : MonoBehaviour
{

    public enum ResourcesPlaceholder
    {
        ResourceA, ResourceB, ResourceC
    }

    public enum ToUpgrade
    {
        Engine, Shield, Damage, FireRate
    }

    public int[] inventory = new int[3];

    public int EngineRank;
    public int ShieldRank;
    public int DamageRank;
    public int FireRateRank;

    public GameObject upgradeScreen;
    public GameObject shipScreen;

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
            if (Input.GetKeyDown(KeyCode.U))
            {
                Time.timeScale = 0;
                InUpgradeScreen = true;
                shipScreen.SetActive(false);
                upgradeScreen.SetActive(true);
            }
        }
    }

    public void AssignUI(UpgradeUI ui, UpgradeModule module)
    {
        if (ui == null) return;
        ui.title.text = module.title;
        ui.description.text = module.description;
        ui.AssignModule(module);
        if (module.CanAfford(inventory[0], inventory[1], inventory[2]) && module.unlocked == false)
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

    public void Upgrade(UpgradeModule module)
    {
        switch(module.toUpgrade)
        {
            case ToUpgrade.Engine:
                EngineRank++;
                break;
            case ToUpgrade.Shield:
                ShieldRank++;
                break;
            case ToUpgrade.Damage:
                DamageRank++;
                break;
            case ToUpgrade.FireRate:
                FireRateRank++;
                break;
            default:
                break;
        }

        for (int i = 0; i < module.ingredient.Count; i++)
        {
            switch (module.ingredient[i])
            {
                case ResourcesPlaceholder.ResourceA:
                    inventory[0] -= module.amountNeeded[i];
                    break;
                case ResourcesPlaceholder.ResourceB:
                    inventory[1] -= module.amountNeeded[i];
                    break;
                case ResourcesPlaceholder.ResourceC:
                    inventory[2] -= module.amountNeeded[i];
                    break;
            }
        }

        module.unlocked = true;
        Refresh();
    }

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

    [Serializable]
    public class UpgradeModule
    {
        [Header("Recipe")]
        public List<ResourcesPlaceholder> ingredient;
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

        public bool CanAfford(int A, int B, int C)
        {
            bool passing = true;
            for (int i = 0; i < ingredient.Count; i++)
            {
                switch(ingredient[i])
                {
                    case ResourcesPlaceholder.ResourceA:
                        if (A < amountNeeded[i]) passing = false;
                        break;
                    case ResourcesPlaceholder.ResourceB:
                        if (B < amountNeeded[i]) passing = false;
                        break;
                    case ResourcesPlaceholder.ResourceC:
                        if (C < amountNeeded[i]) passing = false;
                        break;
                }
            }
            return passing;
        }

    }

    [Serializable]
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
