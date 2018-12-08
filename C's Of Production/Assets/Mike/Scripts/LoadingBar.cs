using UnityEngine.UI;
using UnityEngine;

public class LoadingBar : MonoBehaviour {

    public static LoadingBar instance;
    public Scrollbar loadingBar;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public LoadingBar GetInstance()
    {
        return instance;
    }

    //This function expects a value between 1 to 100 to represent percentage
    public void UpdateLoadingBar(int value)
    {
        loadingBar.size = value / 100;
    }

}
