using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGrass : MonoBehaviour
{
    private void Awake()
    {
        float randomScale = Random.Range(0.29f, 0.42f);
        gameObject.transform.localScale = new Vector2(randomScale, randomScale);

        gameObject.transform.localPosition = new Vector2(Random.Range(-955, 955), Random.Range(-530, 530));

        float randomRotation = Random.Range(20, -20);
        gameObject.transform.eulerAngles = new Vector3(0, 0, randomRotation);
    }
}
