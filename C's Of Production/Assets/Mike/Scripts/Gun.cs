using UnityEngine;

public class Gun : MonoBehaviour
{

    public enum Mode { Pistol = 1, MachineGun = 2, Shotgun = 3 };
    public Mode FireMode = Mode.Pistol;
    public float damage = 25f;
    public bool randomizeDamage;
    public float MaxDamage = 75f;
    public int ammo = 50;
    RaycastHit hit;
    bool mouselocked;
    bool isAPistol;
    bool isAMachineGun;
    bool isAShotgun;
    public float shotGunSpread = 10;
    public float bulletsPerShot = 9;
    private Vector3 direction;
    public AudioSource audio;
    public Animator anim;

    public void AddAmmo(int amount)
    {
        ammo += amount;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mouselocked = true;

        switch (FireMode)
        {
            case Mode.Pistol:
                isAPistol = true;
                break;

            case Mode.MachineGun:
                isAMachineGun = true;
                break;

            case Mode.Shotgun:
                isAShotgun = true;
                break;
        }
    }

    void Update()
    {
        if (((isAPistol || isAShotgun) && Input.GetButtonDown("Fire1")) || isAMachineGun && Input.GetButton("Fire1"))
        {
            audio.Play();
            anim.SetBool("Fired", true);

            if (!isAShotgun && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1000))
            {

                if (hit.transform.tag.Equals("Enemy", System.StringComparison.Ordinal))
                {
                    hit.transform.SendMessage("ApplyDamage", (randomizeDamage) ? Random.Range(damage, MaxDamage) : damage, SendMessageOptions.DontRequireReceiver);
                }

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red, 5f);
                ammo--;
            }
            else if (isAShotgun)
            {
                for (int i = 0; i < bulletsPerShot; i++)
                {
                    direction = Random.insideUnitCircle / shotGunSpread;
                    direction.z = 90;

                    direction = transform.TransformDirection(direction.normalized);

                    //Creat a ray to be shot from the camera directly forward (adjusted because of the way the mouse is centered)

                    if (Physics.Raycast(new Ray(transform.position, direction), 1000))
                    {

                        if (hit.transform.tag.Equals("Enemy", System.StringComparison.Ordinal))
                        {
                            hit.transform.SendMessage("ApplyDamage", (randomizeDamage) ? Random.Range(damage, MaxDamage) : damage, SendMessageOptions.DontRequireReceiver);
                        }

                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red, 5f);
                        ammo--;

                    }
                }
            }
        }
        else
        {

            anim.SetBool("Fired", false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mouselocked)
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