using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletScrollBar : MonoBehaviour
{//script responsiblle for the UI scroll bar for the weapon wheel
    public GameObject[] BulletSlots;
    int bulletSlotPosition = 0;
    Image currentSlotImage;
    public Sprite selectedSlotImage;
    public Sprite normalSlotImage;
    float delayTime = 0f;
    public static int selectedbulletNumber;

    void Start()
    {
        currentSlotImage = BulletSlots[0].GetComponentInChildren<Image>();
        currentSlotImage.sprite = selectedSlotImage;
        //sets the initial bullet slot to be the first slot
    }

    void Update()
    {
        var scrollposition = Input.GetAxis("Mouse ScrollWheel");
        delayTime += 1* Time.deltaTime;
        //Allows for scroll wheel input to select a weapon

        if (scrollposition == 0 && delayTime > 1f)
        {//if the player stops with the scroll wheel input after 1 second, that weapon/bullet type will be selected
            for (int i = 0; i < BulletSlots.Length; i++)
            {
                Image im = BulletSlots[i].GetComponent<Image>();
                
                Image im2 = GameObject.Find("Bullet" + i).GetComponent<Image>();
                Color c = im.color;
                Color c2 = im2.color;
                c.a = 0;
                c2.a = 0;
                im.color = c;
                im2.color = c2;
                //UI is updated as accordingly to select item
            }
        }
        else
        {
            for (int i = 0; i < BulletSlots.Length; i++)
            {
                Image im = BulletSlots[i].GetComponentInChildren<Image>();
                Image im2 = GameObject.Find("Bullet" + i).GetComponent<Image>();
                Color c = im.color;
                Color c2 = im2.color;
                c.a = 1;
                im.color = c;
                im2.color = c;
            }
        }

        if (scrollposition > 0.05f)
        {//for when the player is using the scroll wheel, for moving clockwise around the UI (forward wheel)
            delayTime = 0;
            Debug.Log("SHOULD MOVE NOW");
            bulletSlotPosition++;
            if (bulletSlotPosition > 7)
            {
                bulletSlotPosition = 0;
            }
            currentSlotImage = BulletSlots[bulletSlotPosition].GetComponentInChildren<Image>();
            currentSlotImage.sprite = selectedSlotImage;
            //Updates selected UI to visualise which has been selected

            selectedbulletNumber = bulletSlotPosition;

            for (int i = 0; i < BulletSlots.Length; i++)
            {
                if(i != bulletSlotPosition)
                {
                    BulletSlots[i].GetComponentInChildren<Image>().sprite = normalSlotImage;
                }
            }
        }

        else if (scrollposition < -0.05f)
        {//for when the player is using the scroll wheel, for moving anticlockwise around the UI (back wheel)
            delayTime = 0;
            Debug.Log("SHOULD MOVE NOW");
            bulletSlotPosition--;
            if (bulletSlotPosition < 0)
            {
                bulletSlotPosition = 7;
            }
            currentSlotImage = BulletSlots[bulletSlotPosition].GetComponentInChildren<Image>();
            currentSlotImage.sprite = selectedSlotImage;
            //Updates selected UI to visualise which has been selected
            selectedbulletNumber = bulletSlotPosition;

            for (int i = 0; i < BulletSlots.Length; i++)
            {
                if (i != bulletSlotPosition)
                {
                    BulletSlots[i].GetComponentInChildren<Image>().sprite = normalSlotImage;
                }
            }
        }




    }
}
