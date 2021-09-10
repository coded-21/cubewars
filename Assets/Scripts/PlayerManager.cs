using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager: MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX;
    public float minZ;
    public float maxX;
    public float maxZ;

    public void Start()
    {
        SpawnPlayer();
    }

    public void PlayerDeath(PhotonView _dedPlayer)
    {
        Debug.Log("Stupid player with View ID: " + _dedPlayer.ViewID + " got wrecked!");
    }

    private void SpawnPlayer()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint, Quaternion.Euler(0, 0, 0));
    }
}