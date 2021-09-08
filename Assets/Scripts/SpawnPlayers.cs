using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX;
    public float minZ;
    public float maxX;
    public float maxZ;

    public void Start()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint, Quaternion.Euler(0, 0, 0));
    }

}