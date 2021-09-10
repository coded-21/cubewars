using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;


public class PlayerManager: MonoBehaviour
{
    PhotonView PV;

    [Header("Spawn Dimentions")]
    public float minX;
    public float minZ;
    public float maxX;
    public float maxZ;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        Debug.Log("Loaded PlayerManager");
    }
    public void Start()
    {
        if (PV.IsMine)
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), spawnPoint, Quaternion.Euler(0, 0, 0));
    }

    public void PlayerDeath(PhotonView _dedPlayer)
    {
        Debug.Log("Stupid player with View ID: " + _dedPlayer.ViewID + " got wrecked!");
    }

}