using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBomb : MonoBehaviour
{//script that manages the firebomb animation after a certain amount of time
    [SerializeField]
    float timeBeforeExplosion;
    [SerializeField]
    Sprite spriteChange;
    SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(ExplodeAfter(timeBeforeExplosion));
    }

    IEnumerator ExplodeAfter(float time)
    {
        yield return new WaitForSeconds(time);
        sp.sprite = spriteChange;
    }
}
