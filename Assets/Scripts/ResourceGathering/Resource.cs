using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : Interactable
{
    // Start is called before the first frame update

    public ResourceManager.ResourceContainer container;
    public string ResourceName { get; set; }

    [SerializeField] private int _resourceQuantity = 1;
    public int ResourceQuantity { get; set; }
    public GameObject gameModel;

    void Start()
    {
        if (container == ResourceManager.ResourceContainer.undefined) Destroy(gameObject);
        else
        {
            switch(container)
            {
                case ResourceManager.ResourceContainer.spinyPlant:
                    // set spinyPlant model to Active
                    break;
            }
        }

        var outline = gameObject.AddComponent<Outline>();

        // outline.OutlineMode = Outline.Mode.OutlineAll;
        // outline.OutlineColor = Color.yellow;
        // outline.OutlineWidth = 5f;

        outline.enabled = false;
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
