﻿using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Health = 100f;
    public float XSensitivity = 30f;
    public float YSensitivity = 20f;
    public float XMovementSensitivity = 0.5f;
    public float YMovementSensitivity = 0.5f;
    public GameObject CameraObject;
    private Rigidbody playerRigid;
    public GameObject PlayerDeathReplacement;

    private void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * YSensitivity;
        CameraObject.transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), 0, 0) * Time.deltaTime * XSensitivity;
        playerRigid.MovePosition(transform.position+transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal") * XMovementSensitivity * Time.deltaTime, Input.GetAxis("Jump"), Input.GetAxis("Vertical") * YMovementSensitivity * Time.deltaTime)));
        
    }

    public void ApplyDamage(float amount)
    {
        Health -= amount;
        PlayerUI.GetInstance().UpdateHealth(Health);
        
        if(Health <= 0)
        {
            Instantiate(PlayerDeathReplacement, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
