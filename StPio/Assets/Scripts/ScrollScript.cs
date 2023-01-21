using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour
{

    public CircleCollider2D scroll;
    public Image[] scrollTexts;
    private int scrollCount;
    private void OnTriggerEnter2D (Collider2D collision)
    {
        BoxCollider2D playerBoxCollider = collision.gameObject.GetComponent<BoxCollider2D>();
        if (scroll.IsTouching(playerBoxCollider)){
            ScrollCounterScript.scrollAmount += 1;
            scrollCount = ScrollCounterScript.scrollAmount;
            scrollTexts[scrollCount-1].enabled = true;
            Destroy(gameObject);
        }
    }
}
