using UnityEngine;

public class UIAnimationCallBack : MonoBehaviour {

    public void StatusTextIsDone()
    {
        PlayerUI.GetInstance().DisableStatusText();
    }

    public void LoadingBarIsDone()
    {
        PlayerUI.GetInstance().DisableLoadingBar();
    }
}
