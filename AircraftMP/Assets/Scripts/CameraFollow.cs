using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform _followTarget;



    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 moveCamTo = _followTarget.position - _followTarget.transform.forward * 25f + _followTarget.transform.up * 5f;

        float bias = .99f;

        transform.position = transform.position * bias + moveCamTo * (1 - bias);
        transform.LookAt(_followTarget.transform.position + _followTarget.transform.forward * 50f);
    }
}
