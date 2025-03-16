using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyMechanics : MonoBehaviour
{
    public ActiveMechanics activeScript;
    public GameObject activeObject;

    public Collider2D collider2d;

    private void Awake()
    {
        activeObject = GameObject.Find("ActiveScript");
        activeScript = activeObject.GetComponent<ActiveMechanics>();

        collider2d = gameObject.GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        collider2d.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collides with bullet or slime
        if (collision.gameObject.layer == 12 || collision.gameObject.layer == 7)
        {
            activeScript.DecoyDeath(true);
            collider2d.enabled = false;
        }
    }
}
