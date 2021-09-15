using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    PhotonView PV;
    [HideInInspector] public Rigidbody rb;

    [Header("Player Controller")]
    [SerializeField] float rotationSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float boostSpeed;
    [SerializeField] float haltSpeedCooldown;
    [SerializeField] float speedBoostCooldown;
    private float newSpeedBoostTime;
    private float newHaltSpeedTime;
    Vector3 currentPosition;
    bool reverse = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        //Debug.Log("Yo check this out: " + PV.InstantiationData[0] + " is the view ID of Player Manager of this brat.");
    }

    private void Start()
    {
        newSpeedBoostTime = 0f;
        newHaltSpeedTime = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        if (PV.IsMine)
        {
            currentPosition = transform.position;
            PlayerRotation();
            PlayerMovement();
        }

    }

    
    private void Update()
    {
        if (PV.IsMine)
        {
            FireUsingGun();
            ResetRotation();
            speedBoost();
            haltSpeed();
        }
        if (Input.GetKeyDown(KeyCode.N)) { GetComponent<PlayerStatus>().SelfHarm(); }
    }
    

    public void PlayerRotation()
    {
        if (!reverse)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.RotateAround(currentPosition, -Vector3.up, rotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
                transform.RotateAround(currentPosition, Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else if (reverse)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.RotateAround(currentPosition, Vector3.up, rotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
                transform.RotateAround(currentPosition, -Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    public void PlayerMovement()
    {
        if (rb.velocity == Vector3.zero)
        {
            reverse = false;
        }

        if(Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * moveSpeed * Time.deltaTime);
            reverse = false;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * moveSpeed * Time.deltaTime);
            reverse = true;
        }
    }

    public void speedBoost()
    {
        if(Time.time > newSpeedBoostTime)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                rb.AddForce(transform.forward * boostSpeed);
                newSpeedBoostTime = Time.time + speedBoostCooldown;
            }
        }
    }

    private void haltSpeed()
    {
        if (Time.time > newHaltSpeedTime)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                rb.velocity = Vector3.zero;
                newHaltSpeedTime = Time.time + haltSpeedCooldown;
            }

        }
    }

    private void FireUsingGun()
    {
        if (Input.GetKey(KeyCode.L))
        {
            GetComponentInChildren<Weapon>().Fire(PV);
        }
    }

    private void ResetRotation()
    {
        if (Input.GetKey(KeyCode.R))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 2f, gameObject.transform.position.z);
            gameObject.transform.rotation = Quaternion.identity;
        }
    }
}
