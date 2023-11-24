using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] bool createOnStart = true;

    [SerializeField] GameObject objectToSpawn;
    GameObject attachedObject;

    private void Start()
    {
        if (createOnStart)
        {
            CreateObject();
            ResetObject();
        }
    }

    public void ResetObject()
    {
        if (attachedObject == null)
            CreateObject();

        attachedObject.transform.position = transform.position;
        attachedObject.transform.rotation = transform.rotation;
    }

    public void CreateObject()
    {
        attachedObject = Instantiate(objectToSpawn, transform);
    }
}