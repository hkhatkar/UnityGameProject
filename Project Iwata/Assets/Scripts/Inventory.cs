using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{//Inventory script is responsible for all slots in inventory/ checking if each slot contains an item
// if an item is found then this is updated in isFull, through the slot script
    public bool[] isFull;
    public GameObject[] slots = new GameObject[4];


    public bool FindItem(GameObject item)
    {//script used to check if an item is contained within any slots in inventory
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
