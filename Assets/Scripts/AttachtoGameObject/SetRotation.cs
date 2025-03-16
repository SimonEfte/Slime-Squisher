using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotation : MonoBehaviour
{
    public GameObject centerObject;
    public float speed;
    public float radius;

    private float initialAngle; // Stores the initial angle

    void OnEnable()
    {
        // Calculate initial angle based on the starting position
        Vector2 direction = transform.position - centerObject.transform.position;
        initialAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    void Update()
    {
        // Maintain the initial angle while adding rotation over time
        float angle = initialAngle + (Time.timeSinceLevelLoad * speed * 360f);

        float x = centerObject.transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float y = centerObject.transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

        transform.position = new Vector2(x, y);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        // Keep rotating around the center object
        transform.RotateAround(centerObject.transform.position, Vector3.forward, speed * Time.deltaTime);
    }
}
