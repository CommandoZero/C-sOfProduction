using UnityEngine.UI;
using System;
using UnityEngine;

[Serializable]
public class LoadingBar {

    public Animator animator;
    public Scrollbar loadingBar;

    //This function expects a value between 1 to 100 to represent percentage
    public void UpdateLoadingBar(float value)
    {
        if(!loadingBar.IsActive())
        {
            loadingBar.gameObject.SetActive(true);
        }

        loadingBar.size = value / 100.0f;

        if(value >= 100)
        {
            animator.SetBool("Done",true);
        }
        else
        {
            animator.SetBool("Done", false);
        }
    }

}
