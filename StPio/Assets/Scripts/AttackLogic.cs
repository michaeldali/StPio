using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackLogic : MonoBehaviour
{

    [SerializeField] private int attackDamage = 40;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;


    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack(){

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }


}
