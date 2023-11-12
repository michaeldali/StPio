using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    public void shinyEffect ()
    {
        anim.SetTrigger("shinyEffect");
    }

}
