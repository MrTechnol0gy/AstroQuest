using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject thirdCamera;
    public GameObject thirdCamBrain;
    public GameObject firstCamera;
    public GameObject firstCamBrain;
    public int camMode;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Camera")) //make it so that player doesn't render(but does have shadow? in 1st person
        {
            camMode = (camMode == 1) ? 0 : 1;

            StartCoroutine(CamChange());
        }
    }

    IEnumerator CamChange()
    {
        yield return new WaitForSeconds(0.01f);

        if (camMode == 0)
        {
            thirdCamera.SetActive(true);
            firstCamera.SetActive(false);
            thirdCamBrain.SetActive(true);
            firstCamBrain.SetActive(false);
        }
        else if (camMode == 1)
        {
            thirdCamera.SetActive(false);
            firstCamera.SetActive(true);
            thirdCamBrain.SetActive(false);
            firstCamBrain.SetActive(true);
        }
    }
}
