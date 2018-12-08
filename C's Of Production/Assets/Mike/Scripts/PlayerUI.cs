using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    private static PlayerUI instance;
    public LoadingBar loadingBar;
    public StatusText statusText;
    public Scrollbar healthBar;

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

    public void UpdateHealth(float health)
    {
        if (!(health <= 0))
        {
            healthBar.size = health / 100;
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

    public void UpdateLoadingBar(float val)
    {
        loadingBar.UpdateLoadingBar(val);
    }
}
