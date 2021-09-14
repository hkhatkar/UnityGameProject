using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseItem : MonoBehaviour
{//This class is responsible for all the shop purchase options dealing with currency and items
 

    public GameObject ItemPurchased;
    GameObject CloneItem;//Clone of ItemPurchased
    public GameObject ItemSpawnPosition;//Position the clone when spawned
    float ItemSpawnPositionVectorX, ItemSpawnPositionVectorY;
    public int ItemPrice;
    //Declares variables
    
    void Start()
    {// Start is called before the first frame update
        ItemSpawnPositionVectorX = ItemSpawnPosition.transform.position.x;
        ItemSpawnPositionVectorY = ItemSpawnPosition.transform.position.y;
        //Sets the spawn points of the newly bought item above the NPC's head/ anywhere assigned in unity
    }
    
    public void CreateItemPurchased ()
    {//This procedure is called when an item is purchased
        if (ScoreTextScript.coinAmount >= ItemPrice)
        {//This checks if the player has enough coins to buy the item clicked on
            ScoreTextScript.coinAmount = ScoreTextScript.coinAmount - ItemPrice;
            CloneItem = Instantiate(ItemPurchased, new Vector2(ItemSpawnPositionVectorX, ItemSpawnPositionVectorY), transform.rotation) as GameObject;
            //If so the item is cloned above the NPC's head and currency is deducted
        }
    }//end procedure
}//end class
