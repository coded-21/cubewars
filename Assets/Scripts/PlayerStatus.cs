using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, IDamagable
{
    PhotonView PV;
    const int maxHealth = 100;
    public int currentHealth;
    [HideInInspector] public PlayerManager playerManager;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
        currentHealth = maxHealth;
        
    }
    public void TakeDamage(int damage)
    {
        if (PV.IsMine)
        {
            PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
        }
    }

    [PunRPC]
    void RPC_TakeDamage(int _damage)
    {
        if (!PV.IsMine) return;
        currentHealth -= _damage;

        if (currentHealth <= 0)
        {
            playerManager.PlayerDeath();
        }
    }

    public void SelfHarm()
    {
        if (PV.IsMine)
        {
            PV.RPC("RPC_SelfHarm", RpcTarget.All);
        }
    }

    [PunRPC]
    void RPC_SelfHarm()
    {
        if (!PV.IsMine) return;

        currentHealth -= 20;
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            playerManager.SelfDestruct();
        }
    }
}
