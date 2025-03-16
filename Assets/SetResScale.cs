using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetResScale : MonoBehaviour, IPointerEnterHandler
{
    public GameObject res;

    public void OnPointerEnter(PointerEventData eventData)
    {
        res.transform.localScale = new Vector2(1.6f,1.6f);
    }
}
