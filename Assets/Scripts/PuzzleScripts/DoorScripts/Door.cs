using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool automatic = true;
    public Transform doorCube;
    public GameObject triggerSpace;

    public bool opening = false;

    float closedYPos = 0;
    float openYPos = -5;

    public float speed;

    void Start()
    {
        doorCube = transform.GetChild(0);
        triggerSpace = transform.GetChild(1).gameObject;
        if (!automatic) triggerSpace.SetActive(false);
    }

    void Update()
    {
        if (opening) Open();
        else Close();
    }

    void Open()
    {
        if (doorCube.position.y < openYPos)
            doorCube.position = new Vector3(doorCube.position.x, openYPos, doorCube.position.z);
        if (doorCube.position.y == openYPos) return;
        doorCube.position -= new Vector3(0, speed * Time.deltaTime);
    }
    void Close()
    {
        if (doorCube.position.y > closedYPos)
            doorCube.position = new Vector3(doorCube.position.x, closedYPos, doorCube.position.z);
        if (doorCube.position.y == closedYPos) return;
        doorCube.position += new Vector3(0, speed * Time.deltaTime);
    }
}