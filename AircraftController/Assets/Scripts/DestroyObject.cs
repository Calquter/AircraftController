using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    [SerializeField] private float _timer;

    void Start()
    {
        StartCoroutine(DestroyWithDelay(_timer));
    }

    IEnumerator DestroyWithDelay(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(this.gameObject);
    }

}
