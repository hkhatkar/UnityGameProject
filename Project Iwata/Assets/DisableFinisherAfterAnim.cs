using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFinisherAfterAnim : MonoBehaviour
{//short script to disable the finishing move animation once it has been completed
   void DisableFinisher()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
