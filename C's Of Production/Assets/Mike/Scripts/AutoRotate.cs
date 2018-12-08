using UnityEngine;

public class AutoRotate : MonoBehaviour {

    public float X, Y, Z;
    public bool RandomlyInvert;

    private void Start()
    {
        if(RandomlyInvert && Random.Range(0,2) == 0)
        {
            X *= -1;
            Y *= -1;
            Z *= -1;
        }
    }

    void Update ()
    {
        transform.Rotate(X, Y, Z);
	}
}
