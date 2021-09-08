using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[CreateAssetMenu(menuName = "Gun")]
public class Gun : ScriptableObject
{
    public int bulletSpeed;
    public int fireCooldown;
    public GameObject bullet;
    [HideInInspector] public float newFireTime;
}