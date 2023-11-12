using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    public BoxCollider2D characterCollider;
    public CapsuleCollider2D characterBlockerCollider;

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(characterCollider, characterBlockerCollider, true);
    }
}
