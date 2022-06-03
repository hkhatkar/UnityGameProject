using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{//This class is responsible for holding player inventory items in slots

    private Inventory inventory;
    public int slotNumber;
    //Declare variables

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        //uses inventory script in order to update isFull array to empty, when a button in the inventory is pressed and therefore destroyed
    }

    private void Update()
    {
        if(transform.childCount <= 0)
        {//if the item held in inventory is destroyed, it is no longer a child object of a slot, therefore checks there are no child objects
            inventory.isFull[slotNumber] = false;
            //therefore if not, the slot is empty
        }
    }

    public void DropItem()
    {//method used for destroying child objects in each slot

        foreach(Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
