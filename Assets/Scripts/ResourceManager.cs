using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public enum ResourceType
    {
        undefined,
        spinyPlant,
        glowyRock,
        techSalvage,
        celestialMetal,
        universalOre
    }

    public GameObject spinyPlant;
    public GameObject glowyRock;
    public GameObject techSalvage;
    public GameObject celestialMetal;
    public GameObject universalOre;

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
        resourceInventory[(int)collected.type] += collected.ResourceQuantity;
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
