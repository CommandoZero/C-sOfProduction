using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour {

    
    public Animator anim;

    // Update is called once per frame
    void Update () {

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            anim.SetFloat("Speed", 1f);
        }
        else
        {
            anim.SetFloat("Speed", 0f);
        }
    }
}
