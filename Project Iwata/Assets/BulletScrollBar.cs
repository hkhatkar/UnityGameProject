using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletScrollBar : MonoBehaviour
{
    public GameObject[] BulletSlots;
    int bulletSlotPosition = 0;
    Image currentSlotImage;
    public Sprite selectedSlotImage;
    public Sprite normalSlotImage;
    // Start is called before the first frame update
    float delayTime = 0f;

    public static int selectedbulletNumber;

    void Start()
    {
        currentSlotImage = BulletSlots[0].GetComponentInChildren<Image>();
        currentSlotImage.sprite = selectedSlotImage;
    }

    // Update is called once per frame
    void Update()
    {
        var scrollposition = Input.GetAxis("Mouse ScrollWheel");
        delayTime += 1* Time.deltaTime;

        if (scrollposition == 0 && delayTime > 1f)
        {


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
        {
            delayTime = 0;
            Debug.Log("SHOULD MOVE NOW !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            bulletSlotPosition++;
            if (bulletSlotPosition > 7)
            {
                bulletSlotPosition = 0;
            }
            currentSlotImage = BulletSlots[bulletSlotPosition].GetComponentInChildren<Image>();
            currentSlotImage.sprite = selectedSlotImage;

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
        {
            delayTime = 0;
            Debug.Log("SHOULD MOVE NOW !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            bulletSlotPosition--;
            if (bulletSlotPosition < 0)
            {
                bulletSlotPosition = 7;
            }
            currentSlotImage = BulletSlots[bulletSlotPosition].GetComponentInChildren<Image>();
            currentSlotImage.sprite = selectedSlotImage;

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
