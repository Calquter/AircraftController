using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;


    void Update()
    {

        transform.position += transform.forward * _bulletSpeed * Time.deltaTime;

    }
}
