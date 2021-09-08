using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpScript : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        }
    }
}
