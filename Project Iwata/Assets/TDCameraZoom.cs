using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TDCameraZoom : MonoBehaviour
{//NON ACTIVE SCRIPT

    public Transform target;

    public Vector3 offset;
    public float zoomSpeed = 4f;
    public float minZoom = 1f;
    public float maxZoom = 40f;
    public float pitch = 2f;
    public float yawSpeed = 300f;
    private float currentZoom = 10f;
    private float currentYaw = 0f;

    private void Update()
    {

        
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        currentYaw -= Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;

    }

    void LateUpdate()
    {

        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, currentYaw);

    }
}
