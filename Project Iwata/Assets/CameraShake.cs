using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{//This class is responsible for creating a camera shake which is played every time the player takes damage


    public static bool bulletDestroyed;
    public Vector3 originalPos;
    void Start()
    {
        bulletDestroyed = false;
        originalPos = transform.localPosition;
    }

    public IEnumerator Shake(float duration, float magnitude)   
    {//IEnumerator allows for a function to be ran for a specific amount of time (duration)
       
        
        //The original camera position is assigned to the cameras position originally

        float elapsed = 0.0f;
        while(elapsed < duration)//while loop iterates until duration is up
        {
            float x = Random.Range(-1f, 1f) * 0.02f; //* magnitude;
            float y = Random.Range(-1f, 1f) * 0.02f; //* magnitude;
            Debug.Log(x + " and " + y);
            //creates the camera shake from placing camera in random positions

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
            //function returns null
           
        }
       

        transform.localPosition = originalPos;
        //turns the camera position back to original
    }//end function
    public IEnumerator ShakeCustom(float duration, float magnitude)
    {//IEnumerator allows for a function to be ran for a specific amount of time (duration)

        
        //The original camera position is assigned to the cameras position originally

        float elapsed = 0.0f;
        while (elapsed < duration)//while loop iterates until duration is up
        {
            float x = Random.Range(-1f, 1f) * 0.1f * magnitude;
            float y = Random.Range(-1f, 1f) * 0.1f * magnitude;
            Debug.Log(x + " and " + y);
            //creates the camera shake from placing camera in random positions

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            
            yield return null;
            //function returns null
            

        }



        transform.localPosition = originalPos;
        //turns the camera position back to original
    }//end function

    
    void Update()
    {
        if(bulletDestroyed == true)
        {
            Debug.Log("In !!!!!!!!!!!!!");
            StartCoroutine(ShakeCustom(0.3f, 1f));
            bulletDestroyed = false;
        }
    }

}//end class
