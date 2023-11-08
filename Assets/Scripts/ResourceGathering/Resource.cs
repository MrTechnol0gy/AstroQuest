using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : Interactable
{
    // Start is called before the first frame update

    [Space(5)]
    [Header("Choose model for resource interactable:")]
    [Space(5)]
    public ResourceManager.ResourceContainer resourceModel;
    public string ResourceName { get; set; }

    [Space(5)]
    [Header("Choose amount of resources that will be collected:")]
    [Space(5)]
    [SerializeField] private int _unrefinedMetals = 0;
    public int UnrefinedMetals { get { return _unrefinedMetals; }}
    [SerializeField] private int _unprocessedOrganics = 0;
    public int UnprocessedOrganics { get { return _unprocessedOrganics; } }
    [SerializeField] private int _componentChemicals = 0;
    public int ComponentChemicals { get { return _componentChemicals; } }

    public int ResourceQuantity { get; set; }
    public static GameObject spinyPlantA;
    public static GameObject spinyPlantB;
    public static GameObject spikyCactus;
    public static GameObject rockA;
    public static GameObject rockB;

    protected List<GameObject> containers;
    void Awake()
    {
        spinyPlantA = transform.Find("models/spinyPlantA").gameObject;
        spinyPlantB = transform.Find("models/spinyPlantB").gameObject;
        spikyCactus = transform.Find("models/spikyCactus").gameObject;
        rockA = transform.Find("models/rockA").gameObject;
        rockB = transform.Find("models/rockB").gameObject;

        containers = new() { spinyPlantA, spinyPlantB, spikyCactus, rockA, rockB };

        foreach (GameObject gameObject in containers) gameObject.SetActive(false);

        if (resourceModel == ResourceManager.ResourceContainer.random)
        {
            int containerCount = Enum.GetNames(typeof(ResourceManager.ResourceContainer)).Length;
            resourceModel = (ResourceManager.ResourceContainer)UnityEngine.Random.Range(0, containerCount - 1);
        }
        switch (resourceModel)
        {
            case ResourceManager.ResourceContainer.spinyPlantA:
                spinyPlantA.SetActive(true);
                break;

            case ResourceManager.ResourceContainer.spinyPlantB:
                spinyPlantB.SetActive(true);
                break;

            case ResourceManager.ResourceContainer.spikyCactus:
                spikyCactus.SetActive(true);
                break;

            case ResourceManager.ResourceContainer.rockA:
                rockA.SetActive(true);
                break;

            case ResourceManager.ResourceContainer.rockB:
                rockB.SetActive(true);
                break;
        }

        if(_unrefinedMetals == 0 && _unprocessedOrganics == 0 && _componentChemicals == 0)
        {
            switch (resourceModel)
            {
                case ResourceManager.ResourceContainer.spinyPlantA:
                case ResourceManager.ResourceContainer.spinyPlantB:
                    _unprocessedOrganics = UnityEngine.Random.Range(1, 3);
                    _componentChemicals = UnityEngine.Random.Range(1, 3);
                    break;

                case ResourceManager.ResourceContainer.spikyCactus:
                    _unprocessedOrganics = UnityEngine.Random.Range(1, 5);
                    _componentChemicals = UnityEngine.Random.Range(1, 5);
                    break;

                case ResourceManager.ResourceContainer.rockA:
                case ResourceManager.ResourceContainer.rockB:
                    _unrefinedMetals = UnityEngine.Random.Range(1, 3);
                    break;
            }
        }

        // var outline = gameObject.AddComponent<Outline>();
        // 
        // outline.OutlineMode = Outline.Mode.OutlineAll;
        // outline.OutlineColor = Color.yellow;
        // outline.OutlineWidth = 5f;
        // 
        // outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Activate()
    {
        ResourceManager.CollectResource(this);
        Destroy(gameObject);
    }
}
