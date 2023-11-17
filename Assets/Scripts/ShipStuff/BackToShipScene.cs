using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToShipScene : MonoBehaviour
{
    public void SwitchScene(int index)
    {
        FindObjectOfType<ShipUpgradeManager>().ShowUI();
        SceneManager.LoadScene(index);
    }
}
