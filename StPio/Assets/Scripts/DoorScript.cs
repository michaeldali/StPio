using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public Enemy[] enemies = new Enemy[0];
    public HellHound[] hellHounds = new HellHound[0];
    public bool allDead = false;
    public float speed = 2f;
    private Vector2 oPosition;
    public AlterScript alterScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        oPosition = this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (allDead){
            if (oPosition.y + 3 > this.transform.position.y){
                moveDoorUp();
            }
        }
        
        if (alterScript.used)
        {
            StartCoroutine(moveDoorDown());

            StopCoroutine(moveDoorDown());
        }
        if (enemies.Length != 0){
            foreach (Enemy enemy in enemies){
                if (enemy.currentHealth > 0){
                    allDead = false;
                    break;
                }
                else {
                    allDead = true;
                }
            }
        }
        if (hellHounds.Length != 0){
            foreach (HellHound hellhound in hellHounds){
                if (hellhound.currentHealth > 0){
                    allDead = false;
                    break;
                }
                else {
                    allDead = true;
                }
            }
        }
        
        

    }

    private void moveDoorUp(){
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private IEnumerator moveDoorDown(){
        while (this.transform.position.y > oPosition.y){
            this.transform.position -= this.transform.up * speed * Time.deltaTime;
            yield return null;
        }
        yield break;
        
    }
}
