using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftController : MonoBehaviour
{

    [Header("General")]
    [SerializeField] private Rigidbody _rigidBody;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private Transform _target;

    [Header("Inputs")]
    private float _horizontalAxis;
    private float _verticalAxis;


    [Header("General Settings")]
    [SerializeField] private float _rotateSpeed;


    [Header("Timers")]

    [SerializeField] private float _defaultRotTimerMax;

    private float _verticalAxisTimer;
    private float _HorizontalAxisTimer;



    void Start()
    {
        _verticalAxisTimer = _defaultRotTimerMax;
        _HorizontalAxisTimer = _defaultRotTimerMax;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _rigidBody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        RotateAircraft();
        AircraftDefault();
        MoveForward();


        if (Input.GetKeyDown(KeyCode.Space))
        {

            Vector3 planeToEnemy = _target.transform.position - transform.position;

            print(DotProduct(_target.transform.forward.normalized, planeToEnemy.normalized));

            if (DotProduct(_target.transform.forward.normalized, planeToEnemy.normalized) > 0.5f)
            {
                print("Önünde");
            }
            else
            {
                print("Arkanda");
            }

        }
    }


    private void RotateAircraft()
    {

        _horizontalAxis = Input.GetAxis("Horizontal");

        _verticalAxis = Input.GetAxis("Vertical");

        transform.Rotate(_verticalAxis * (_rotateSpeed / 2) * Time.deltaTime, 0, -_horizontalAxis * (_rotateSpeed / 0.75f) * Time.deltaTime);

    }


    private void MoveForward()
    {
        transform.position += transform.forward * Time.deltaTime * _moveSpeed;
    }

    private void AircraftDefault()
    {

        if (_horizontalAxis == 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0f), 0.001f);
        }
    }



    private float DotProduct(Vector3 a, Vector3 b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }

}
