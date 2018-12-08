using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCaptureZone : MonoBehaviour {

    public float DistanceTilCantInteract = 2.5f;
    public GameObject playerObject;
    public ClerkAi clerk;

    private void OnTriggerEnter(Collider other)
    {
        if(playerObject==null && other.transform.tag.Equals("Player",System.StringComparison.Ordinal))
        {
            playerObject = other.gameObject;
        }
    }

    private void Update()
    {
        if(playerObject!= null && (playerObject.transform.position-transform.position).magnitude > DistanceTilCantInteract)
        {
            playerObject = null;
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
            {
                clerk.ResetClerk();
            }
        }
    }

    private void OnDisable()
    {
        playerObject = null;
    }
}
