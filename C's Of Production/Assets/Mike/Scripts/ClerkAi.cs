using UnityEngine;

public class ClerkAi : MonoBehaviour {

    public GameObject captureArea;
    public GameObject deadClerk;
    public MeshRenderer NormalClerk;
    public Material CaputredMat;
    private Material defaultMat;
    bool pointCaptured;
    Rigidbody baseRigidbody;
    Quaternion rot;

	void Start ()
    {
        defaultMat = GetComponent<MeshRenderer>().material;
        baseRigidbody = GetComponent<Rigidbody>();
        rot = transform.rotation;
	}

    public void ApplyDamage()
    {
        if (!pointCaptured)
        {
            deadClerk.transform.rotation = rot;
            deadClerk.SetActive(true);
            NormalClerk.enabled = false;
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
            deadClerk.SetActive(false);
            NormalClerk.enabled = true;
            GetComponent<CapsuleCollider>().enabled = true;
            GetComponent<MeshRenderer>().material = CaputredMat;
            pointCaptured = true;
        }
        else
        {
            deadClerk.SetActive(false);
            NormalClerk.enabled = true;
            GetComponent<CapsuleCollider>().enabled = true;
            GetComponent<MeshRenderer>().material = defaultMat;
            pointCaptured = false;
        }
    }
}
