using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Cinemachine;

public class PlayerConfig : MonoBehaviourPunCallbacks
{
    PhotonView PV;
    public GameObject playerMatBody;
    public GameObject playerMatSkirts;
    public GameObject playerMatGun;
    Color playerColor;
    public Camera playerCam;
    [SerializeField] CinemachineVirtualCamera vCam;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {

        if (PV.IsMine)
        {
            //set-up camera
            vCam = FindObjectOfType<CinemachineVirtualCamera>();
            vCam.Follow = gameObject.transform;

            //give random color to player
            playerColor = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            playerMatBody.GetComponent<Renderer>().material.color = playerColor;
            playerMatSkirts.GetComponent<Renderer>().material.color = playerColor;
            playerMatSkirts.GetComponent<Renderer>().material.color = playerColor;
        }
        else
        {
            playerCam.enabled = false;

            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
            for (int i = 0; i < scripts.Length; i++)
            {
                if (scripts[i] is PlayerConfig) continue;
                else if (scripts[i] is PhotonView) continue;
                else if (scripts[i] is PhotonTransformView) continue;

                scripts[i].enabled = false;
            }
        }

    }
}
