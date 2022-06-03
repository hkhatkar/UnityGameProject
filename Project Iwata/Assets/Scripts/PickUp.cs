using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
//Script that handles when the player enters a trigger area of a pick up item to be stored in the inventory
    private Inventory inventory;
    public GameObject itemButton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        //uses the inventory script isFull variable in order to assign it a place in the inventory and label it as taken
    }

    void OnTriggerEnter (Collider other)
    {//when the triggered area is entered it will check each slot of inventory for any free space, if found it will create a button UI
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if(inventory.isFull[i] == false)
                {//false when the space in that inventory space is empty
                    //item can be added to inventory
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
