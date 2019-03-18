using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectPixel2D : MonoBehaviour
{
    public float pixelsPerUnit = 32f;
    public float zoom = 240f;
    public bool usePixelScale = false;
    public float pixelScale = 4f;

    Vector3 cameraPos = Vector3.zero;
   
    public void Move(Vector3 dir)
    {
        ApplyZoom();
        cameraPos += dir;
        AdjustCamera();
    }
    public void MoveTo(Vector3 pos)
    {
        ApplyZoom();
        cameraPos = pos;
        AdjustCamera();
    }

    public void AdjustCamera()
    {
        Camera.main.transform.position = new Vector3(RoundToNearestPixel(cameraPos.x), RoundToNearestPixel(cameraPos.y), -10f);
        //set position of camera based on camera position variable and then round x and y position to nearest pixel
        //kept separate variable so camera acts smoother
    }

    public float RoundToNearestPixel(float pos)
    {
        float screenPixelsPerUnit = Screen.height / (Camera.main.orthographicSize * 2f);
        //gets pixels per unit for the screen (not pixels per unit we are using in game but instead the actual screen pixels)
        float pixelValue = Mathf.Round(pos * screenPixelsPerUnit);
        //convert position in float value and rounds that value to the nearest pixel

        return pixelValue / screenPixelsPerUnit;
        //convert pixel back to unity

    }

    public void ApplyZoom()
    {
        if (!usePixelScale)
        {
            float smallestDimension = Screen.height < Screen.width ? Screen.height : Screen.width;
            pixelScale = Mathf.Round(smallestDimension / zoom);

        }

        Camera.main.orthographicSize = (Screen.height / (pixelsPerUnit * pixelScale)) * 0.5f;
        
    }

    public Vector3 GetCameraPos()
    
    {
        return cameraPos;
    }
}
