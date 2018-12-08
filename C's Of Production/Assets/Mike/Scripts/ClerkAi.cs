using UnityEngine;

public class ClerkAi : MonoBehaviour {


    public GameObject captureArea;
    Rigidbody baseRigidbody;
    Quaternion rot;

	void Start ()
    {
        baseRigidbody = GetComponent<Rigidbody>();
        rot = transform.rotation;
	}

    public void ApplyDamage()
    {
        baseRigidbody.useGravity = true;
        baseRigidbody.isKinematic = false;
        captureArea.SetActive(true);
    }

    public void ResetClerk(bool captured = false)
    {
        captureArea.SetActive(false);
        baseRigidbody.useGravity = false;
        baseRigidbody.isKinematic = true;
        transform.rotation = rot;

        if(captured)
        {
            //Change color
        }
    }
}
