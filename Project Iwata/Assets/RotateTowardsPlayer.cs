using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    public float speed = 5f;
    public Transform target;
    private void Update()
    {
        Vector2 direction = target.position- transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

    }
}
