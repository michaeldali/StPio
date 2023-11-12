using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterArea : MonoBehaviour
{
    public GameObject door;
    public float speed = 2f;

    Vector3 oPosition;


    void Start(){
        oPosition = door.transform.position;
    }


    void OnTriggerStay2D (Collider2D other){
        if (other.CompareTag("Player")){
            if (oPosition.y + 3 > door.transform.position.y){
                moveDoorUp();
            }
        }
    }

    void OnTriggerExit2D (Collider2D other){
        if (other.CompareTag("Player")){
            StartCoroutine(moveDoorDown());
            
            StopCoroutine(moveDoorDown());
        }
    }

    private IEnumerator moveDoorDown(){
        while (door.transform.position.y > oPosition.y){
            door.transform.position -= door.transform.up * speed * Time.deltaTime;
            yield return null;
        }
        yield break;
        
    }

    private void moveDoorUp(){
        door.transform.position += door.transform.up * speed * Time.deltaTime;
    }
}
