using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardObject : MonoBehaviour
{
    private GameObject theCamera;
    [SerializeField]
    bool fullyBillboard;
    [SerializeField]
    bool horizontalBillboardOnly;
    [SerializeField]
    bool verticalBillboardOnly;

    // Start is called before the first frame update
    void Start()
    {
        theCamera = GameObject.Find("CM FreeLook1");
    }

    // Update is called once per frame
    void LateUpdate()
    {
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
    }
    private void keepYStatic()
    {
      
        transform.LookAt(theCamera.transform);
        transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, transform.localRotation.eulerAngles.y, gameObject.transform.rotation.z);
    }
    private void keepXStatic()
    {
        transform.LookAt(theCamera.transform);
        transform.rotation = Quaternion.Euler(-transform.localRotation.eulerAngles.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z);
    }

}
