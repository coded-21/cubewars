using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;


public class PlayerManager: MonoBehaviour
{
    PhotonView PV;
    GameObject player;

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

    void SpawnPlayer()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
        player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"),
            spawnPoint,
            Quaternion.Euler(0, 0, 0), 0,
            new object[] { PV.ViewID });
    }

    public void PlayerDeath()
    {
        Debug.Log(player.GetPhotonView().ViewID + " was demolished by a tango");
        PhotonNetwork.Destroy(player);
        SpawnPlayer();
    }

    public void SelfDestruct()
    {
        PhotonNetwork.Destroy(player);
        Debug.Log("You finally got a kill, but guess who you killed? :)");
        SpawnPlayer();
    }

}