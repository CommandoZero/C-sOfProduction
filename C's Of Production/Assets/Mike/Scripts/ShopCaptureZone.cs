using UnityEngine;

public class ShopCaptureZone : MonoBehaviour {

    public float DistanceTilCantInteract = 2.5f;
    public float captureTime = 1;
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
        if (playerObject != null)
        {
            if ((playerObject.transform.position - transform.position).magnitude > DistanceTilCantInteract)
            {
                playerObject = null;
            }

            captureTime += Time.deltaTime * 10;

            PlayerUI.GetInstance().UpdateLoadingBar(captureTime);

            if (captureTime >= 100)
            {
                PlayerUI.GetInstance().updateStatusText("Point Captured!");
                clerk.ResetClerk(true);
                captureTime = 1;
            }
        }
        else
        {
            return;
        }
    }

    private void OnDisable()
    {
        playerObject = null;
    }
}
