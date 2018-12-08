using UnityEngine;

public class ClerkAi : MonoBehaviour {

    public GameObject captureArea;
    public MeshRenderer Meshrend;
    public Material CaputredMat;
    public Material defaultMat;
    bool pointCaptured;
    Rigidbody baseRigidbody;
    Quaternion rot;

	void Start ()
    {
        Meshrend = GetComponent<MeshRenderer>();
        baseRigidbody = GetComponent<Rigidbody>();
	}

    public void ApplyDamage()
    {
        if (!pointCaptured)
        {
            //deadClerk.transform.rotation = rot;
            //deadClerk.SetActive(true);
            Meshrend.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            captureArea.SetActive(true);
        }
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
            Meshrend.enabled = true;
            GetComponent<CapsuleCollider>().enabled = true;
            GetComponent<MeshRenderer>().material = CaputredMat;
            pointCaptured = true;
        }
        else
        {
            Meshrend.enabled = true;
            GetComponent<CapsuleCollider>().enabled = true;
            GetComponent<MeshRenderer>().material = defaultMat;
            pointCaptured = false;
        }
    }
}
