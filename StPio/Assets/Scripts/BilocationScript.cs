using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilocationScript : MonoBehaviour
{
    public GameObject playerGO;
    public GameObject bilocationLocation;
    private bool inRange = false;




    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && inRange && playerGO.GetComponent<Controller>().horizontalMove == 0)
        {
            playerGO.GetComponent<Animator>().SetTrigger("bilocate");
            playerGO.GetComponent<Controller>().bilocationLocation = bilocationLocation;
        }

    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType() == typeof(BoxCollider2D))
        {   
            inRange = true;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType() == typeof(BoxCollider2D))
        {
            inRange = false;
        }
    }

    /*private void bilocation()
    {
        originalLocation = transform.position;
        transform.position = bilocationLocation.transform.position;
        bilocating = true;
    }*/


}
