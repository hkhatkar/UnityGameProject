using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots = new GameObject[10];


    public bool FindItem(GameObject item)
    {
       for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i] == item)
            {
                //found the item 
                return true;
            }

        }
        //item not found
        return false;
    }
}
