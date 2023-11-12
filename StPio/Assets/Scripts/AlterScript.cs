using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterScript : MonoBehaviour
{
    public GameObject playerGO;
    private bool inRange = false;
    public bool used = false;
  



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && inRange && !used && playerGO.GetComponent<Controller>().horizontalMove == 0) 
        {
            playerGO.GetComponent<Animator>().SetTrigger("isPraying");
            used = true;
            playerGO.GetComponent<Controller>().respawnPoint = transform.position;
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


}
