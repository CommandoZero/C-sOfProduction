using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadALevel : MonoBehaviour {

    public void LoadByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

}
