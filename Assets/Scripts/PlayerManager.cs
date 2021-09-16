using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;


public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    GameObject player;

    [Header("Spawn Dimentions")]
    public float minX;
    public float minZ;
    public float maxX;
    public float maxZ;

    [Header("In-Game Stats")]
    public int score;
    public int killCount;
    public int deathCount;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        Debug.Log("Loaded PlayerManager");
    }
    public void Start()
    {
        if (PV.IsMine)
        {
            killCount = 0;
            deathCount = 0;
            score = 0;
            SpawnPlayer();
        }
    }

    void SpawnPlayer()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(minX, maxX), 3, Random.Range(minZ, maxZ));
        player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"),
            spawnPoint,
            Quaternion.Euler(0, 0, 0), 0,
            new object[] { PV.ViewID });
    }

    public void Die()
    {
        PhotonNetwork.Destroy(player);
        SpawnPlayer();
        PV.RPC("RPC_Die", RpcTarget.All);
    }

    public void Kill()
    {
        PV.RPC("RPC_Kill", RpcTarget.All);
    }

    [PunRPC]
    void RPC_Kill()
    {
        killCount++;
        UpdateScore();
        if (PV.IsMine)
        {
            player.GetComponentInChildren<HUD>().UpdateHUD();
        }
    }

    [PunRPC]
    void RPC_Die()
    {
        deathCount++;
        UpdateScore();
        if (PV.IsMine)
        {
            player.GetComponentInChildren<HUD>().UpdateHUD();
        }
    }

    public void SelfDestruct()
    {
        if (PV.IsMine)
        {
            PhotonNetwork.Destroy(player);
            SpawnPlayer();
            PV.RPC("RPC_Die", RpcTarget.All);
        }
        
    }

    void UpdateScore()
    {
        score = killCount - deathCount;
    }
}