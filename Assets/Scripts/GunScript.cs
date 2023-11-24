using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() //needs: SFX,cROSSHAIR, Ammo/Reload? Raycast hitscan?
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            //shoot
            Debug.Log("shotma gun");
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
