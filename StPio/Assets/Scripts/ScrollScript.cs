using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour
{

    // Scroll item
    public CircleCollider2D scroll;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    // Scroll UI
    public Dialogue dialogue;
    public Controller player;
    public GameObject scrollBackground;

    void Start(){
        // Set offset
        posOffset = transform.position;
    }

    void Update(){
        // Float scroll up and down
        tempPos = posOffset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        BoxCollider2D playerBoxCollider = collision.gameObject.GetComponent<BoxCollider2D>();
        if (scroll.IsTouching(playerBoxCollider) && playerBoxCollider.CompareTag("Player")){
            ScrollCounterScript.scrollAmount += 1;
            TriggerDialogue();
            scrollBackground.SetActive(true);
            //scrollCount = ScrollCounterScript.scrollAmount;
            //scrollTexts[scrollCount-1].enabled = true;
            Destroy(gameObject);
        }
    }


    public void TriggerDialogue()
    {
        player.isReading = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    
}
