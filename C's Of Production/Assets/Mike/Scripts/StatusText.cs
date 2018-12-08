using UnityEngine.UI;
using System;

[Serializable]
public class StatusText {

    public Text StatusTextObj;

    public void UpdateText(string text)
    {

        StatusTextObj.gameObject.SetActive(true);
        StatusTextObj.text = text;
    }

}
