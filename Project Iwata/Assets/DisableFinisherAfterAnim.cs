using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFinisherAfterAnim : MonoBehaviour
{
   void DisableFinisher()
    {

        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
