using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{//This is used within enemy type 2's object in order to rotate towards the way the player is facing
    private float speed = 5f;
    public Transform target;
    //Declares variables

    private void Update()
    {
        Vector2 direction = target.position- transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
        //The angle between player and enemy is calculated and varied as the player moves over time

    }
}//end class
