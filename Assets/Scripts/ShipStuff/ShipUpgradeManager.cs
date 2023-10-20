using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipUpgradeManager : MonoBehaviour
{

    public enum ResourcesPlaceholder
    {
        ResourceA, ResourceB, ResourceC
    }

    public List<UpgradeModule> modules;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    public class UpgradeModule
    {
        [Header("Recipe")]
        public List<ResourcesPlaceholder> ingredient;
        public List<int> amountNeeded;

        public bool ranked;
        public List<UpgradeModule> nextRank;

    }


}
