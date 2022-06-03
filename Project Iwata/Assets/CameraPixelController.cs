using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPixelController : MonoBehaviour
{//NON ACTIVE SCRIPT
    // Start is called before the first frame update
    public PerfectPixel2D cameraHelper;
    public float moveSpeed = 0.1f;

    Vector3 randomPos = Vector3.zero;
    float randomTimer = 0f;

    void Update()
    {
        randomTimer -= Time.deltaTime;

        if (randomTimer <= 0f)
        {
            randomTimer = 2f;
            randomPos = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), transform.position.z);
        }

        Vector3 movedir = (randomPos - cameraHelper.GetCameraPos()).normalized;
        cameraHelper.Move(movedir * moveSpeed * Time.deltaTime);
    }


}
