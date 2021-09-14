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
    }

    private void Update()
    {
        if(transform.childCount <= 0)
        {
            inventory.isFull[slotNumber] = false;
        }
    }
    // Start is called before the first frame update
    public void DropItem()
    {

        foreach(Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
