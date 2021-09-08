using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    Vector3 respawnPoint = new Vector3(0, 1, 0);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = respawnPoint;
        }
        else Destroy(other.gameObject);
    }
}
