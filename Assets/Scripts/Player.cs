using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPunCallbacks, IDamagable
{
    PhotonView PV;
    public PlayerManager playerManager;
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

    [Header("Player Status")]
    public int health = 100;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        //Debug.Log("Yo check this out: " + PV.InstantiationData[0] + " is the view ID of Player Manager of this brat.");
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    private void Start()
    {
        newSpeedBoostTime = 0f;
        newHaltSpeedTime = 0f;
        health = 100;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentPosition = transform.position;
        if (PV.IsMine)
        {
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
            if (Input.GetKeyDown(KeyCode.N)) { SelfHarm(); }
        }
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

    void SelfHarm()
    {
        PV.RPC("RPC_SelfHarm", RpcTarget.All);
    }
    public void TakeDamage(int _damage,  PhotonView _shooter)
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All, _damage, _shooter);
    }

    [PunRPC]
    void RPC_SelfHarm()
    {
        if (!PV.IsMine) return;

        health -= 20;
        Debug.Log(health);

        if(health <= 0)
        {
            playerManager.SelfDestruct();
        }
    }

    [PunRPC]
    void RPC_TakeDamage(int _damage, PhotonView _shooter)
    {
        if (!PV.IsMine) return;
        health -= _damage;

        if(health <= 0)
        {
            playerManager.PlayerDeath(_shooter);
        }
    }
}
