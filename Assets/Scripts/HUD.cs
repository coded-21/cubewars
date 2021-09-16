using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class HUD : MonoBehaviour
{
    public TMP_Text killsText;
    public TMP_Text deathsText;
    PlayerManager pm;
    PhotonView PV;
    private void Awake()
    {
        PV = GetComponentInParent<PhotonView>();
        pm = GetComponentInParent<PlayerStatus>().playerManager;
    }
    private void Start()
    {
        if (!PV.IsMine)
        {
            gameObject.SetActive(false);
        }
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        killsText.text = pm.killCount.ToString() + " Kills";
        deathsText.text = pm.deathCount.ToString() + " Deaths";
    }
}
