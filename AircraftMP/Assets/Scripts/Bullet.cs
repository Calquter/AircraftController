using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;

    public Transform followTarget;

    public bool isRocket;
    public bool isFollowing;

    void Update()
    {

        transform.position += transform.forward * _bulletSpeed * Time.deltaTime;

        FollowTarget();
    }

    private void FollowTarget()
    {
        if (isFollowing && isRocket)
        {
            Vector3 dir = followTarget.transform.position - transform.position;

            Quaternion newRotation = Quaternion.LookRotation(dir);

            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 0.01f);
        }
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            isFollowing = false;

            if (isRocket)
            {
                Destroy(other.gameObject);
            }
            else
            {
                if (other.gameObject.GetComponent<AircraftController>())
                {
                    other.gameObject.GetComponent<AircraftController>().health -= 20;
                }
            }
            
            Destroy(this.gameObject);

        }
    }
}
