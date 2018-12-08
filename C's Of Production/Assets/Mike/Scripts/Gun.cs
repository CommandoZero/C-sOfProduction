using UnityEngine;

public class Gun : MonoBehaviour
{

    public int ammo = 50;
    RaycastHit hit;
    bool mouselocked;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mouselocked = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1000))
            {

                if(hit.transform.tag.Equals("Enemy",System.StringComparison.Ordinal))
                {
                    hit.transform.SendMessage("ApplyDamage",SendMessageOptions.DontRequireReceiver);
                    Debug.Log("Enemy Hit!");
                }

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red, 5f);
                ammo--;
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(mouselocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
