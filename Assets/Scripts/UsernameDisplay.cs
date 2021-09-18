using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;

public class UsernameDisplay : MonoBehaviour
{
    Camera cam;
    public Transform lookat;
    public Vector3 offset;
    private void Start()
    {
        FindCam();
    }

    private void FindCam()
    {
        if (cam == null)
        {
            cam = FindObjectOfType<Camera>();
        }
        else return;
    }

    void Update()
    {
        if (cam != null)
        {
            Vector3 pos = cam.WorldToScreenPoint(lookat.position + offset);

            if (transform.position != pos)
            {
                transform.position = pos;
            }
        }
    }
}
