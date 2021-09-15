using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsernameDisplay : MonoBehaviour
{
    Camera cam;
    public Transform lookat;
    public Vector3 offset;
    private void Start()
    {
        if (cam == null)
        {
            cam = FindObjectOfType<Camera>();
        }
        if (cam == null)
        {
            Debug.Log("Camera not found!");
            return;
        }
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
        //transform.LookAt(cam.transform);
        //transform.Rotate(Vector3.up, 180f);
    }
}
