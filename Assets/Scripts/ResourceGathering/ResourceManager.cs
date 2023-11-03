using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public enum ResourceContainer
    {
        undefined,
        spinyPlant,
        spikyCactus,
        rockA,
        rockB,
    }

    public enum ResourceType
    {
        unrefinedMetals,
        unprocessedOrganics,
        componentChemicals
    }

    public GameObject spinyPlant;
    public GameObject spikyCactus;
    public GameObject rockA;
    public GameObject rockB;

    public static int[] resourceInventory;

    public static 
    // Start is called before the first frame update
    void Start()
    {
        int numberOfResourceTypes = Enum.GetNames(typeof(ResourceType)).Length;
        resourceInventory = new int[numberOfResourceTypes];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void CollectResource(Resource collected)
    {
        switch(collected.container)
        {
            case ResourceManager.ResourceContainer.spinyPlant:
                int amount = UnityEngine.Random.Range(1, 6);
                resourceInventory[1] += amount;
                amount = UnityEngine.Random.Range(1, 6);
                resourceInventory[2] += amount;
                break;

            case ResourceManager.ResourceContainer.spikyCactus:
                amount = UnityEngine.Random.Range(1, 4);
                resourceInventory[1] += amount;
                amount = UnityEngine.Random.Range(1, 4);
                resourceInventory[2] += amount;
                break;

            case ResourceManager.ResourceContainer.rockA:
            case ResourceManager.ResourceContainer.rockB:
                amount = UnityEngine.Random.Range(1, 3);
                resourceInventory[0] += amount;
                break;
        }
    }

    public int[] GetResourceInventory()
    {
        return resourceInventory;
    }

    public int GetSpecificQuantity(ResourceType resourceType)
    {
        return resourceInventory[(int)resourceType];
    }
}
