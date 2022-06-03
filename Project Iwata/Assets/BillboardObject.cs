using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardObject : MonoBehaviour
{//class responsible for billboarding object towards the camera, can be customized to axis (vertical and horizontal or both)
    private GameObject theCamera;
    [SerializeField]
    bool fullyBillboard;
    [SerializeField]
    bool horizontalBillboardOnly;
    [SerializeField]
    bool verticalBillboardOnly;

    void Start()
    {
        theCamera = GameObject.Find("CM FreeLook1");
        //finds the virtual camera responsible for the 3D aspect of the game
    }

    void LateUpdate()
    {//late update used for more reliable outcome for billboarding object in the same frame

        //conditions selected in Unity interface
        if (fullyBillboard == true)
        {
            fullBillboarding();
        }
        else if (horizontalBillboardOnly == true)
        {
            keepYStatic();
        }
        else if (verticalBillboardOnly == true)
        {
            keepXStatic();
        }
    }
    //Separate into different axis e.g. the tree billboarding doesnt look right, face it straight

    private void fullBillboarding()
    {
        transform.LookAt(theCamera.transform);
        //the object will be facing towards the cameras x and y position
    }
    private void keepYStatic()
    {
      
        transform.LookAt(theCamera.transform);
        transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, transform.localRotation.eulerAngles.y, gameObject.transform.rotation.z);
        //the object will be facing towards the cameras x position
    }
    private void keepXStatic()
    {
        transform.LookAt(theCamera.transform);
        transform.rotation = Quaternion.Euler(-transform.localRotation.eulerAngles.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z);
        //the object will be facing towards the cameras y position
    }

}
