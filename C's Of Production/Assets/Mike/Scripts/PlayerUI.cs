using UnityEngine;

public class PlayerUI : MonoBehaviour {

    private static PlayerUI instance;
    public LoadingBar loadingBar;
    public StatusText statusText;

    public static PlayerUI GetInstance()
    {
        return instance;
    }

	// Use this for initialization
	void Start () {
		if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void DisableStatusText()
    {
        if(statusText.StatusTextObj.IsActive())
            statusText.StatusTextObj.gameObject.SetActive(false);
    }

    public void DisableLoadingBar()
    {
        if (loadingBar.loadingBar.IsActive())
            loadingBar.loadingBar.gameObject.SetActive(false);
    }

    public void updateStatusText(string message)
    {
        statusText.UpdateText(message);
    }

    public void UpdateLoadingBar(int val)
    {
        loadingBar.UpdateLoadingBar(val);
    }
}
