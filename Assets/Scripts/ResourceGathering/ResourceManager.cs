using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public enum ResourceContainer
    {
        random,
        spinyPlantA,
        spinyPlantB,
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
        resourceInventory[0] += collected.UnrefinedMetals;
        resourceInventory[1] += collected.UnprocessedOrganics;
        resourceInventory[2] += collected.ComponentChemicals;
    }

    public static void LoseResource(ResourceType resourceType, int count)
    {
        if (resourceInventory[(int)resourceType] >= count)
        {
            resourceInventory[(int)resourceType] -= count;
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
