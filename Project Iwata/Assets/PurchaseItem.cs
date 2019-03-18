using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseItem : MonoBehaviour
{

    public GameObject ItemPurchased;
    GameObject CloneItem;//Clone of ItemPurchased
    public GameObject ItemSpawnPosition;//Position the clone when spawned
    float ItemSpawnPositionVectorX, ItemSpawnPositionVectorY;
    public int ItemPrice;
    
    void Start()
    {
        ItemSpawnPositionVectorX = ItemSpawnPosition.transform.position.x;
        ItemSpawnPositionVectorY = ItemSpawnPosition.transform.position.y;
    }
    // Start is called before the first frame update
    public void CreateItemPurchased ()
    {
        if (ScoreTextScript.coinAmount >= ItemPrice)
        {
            ScoreTextScript.coinAmount = ScoreTextScript.coinAmount - ItemPrice;
            CloneItem = Instantiate(ItemPurchased, new Vector2(ItemSpawnPositionVectorX, ItemSpawnPositionVectorY), transform.rotation) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
