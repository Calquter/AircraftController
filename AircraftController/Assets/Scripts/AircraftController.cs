using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftController : MonoBehaviour
{

    [Header("Stats")]

    public int health;



    [Header("General")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _moveSpeedControlMultiplier;

    [SerializeField] private Transform _target;

    [SerializeField] private List<GameObject> _weapons;
    [SerializeField] private GameObject _bullet;

    [SerializeField] private List<GameObject> _rocketLaunchers;
    [SerializeField] private GameObject _rocket;

    private bool _isRight;

    [SerializeField] private GameObject _enemyTarget;
    [SerializeField] private bool _isLocked;

    [Header("Inputs")]
    private float _horizontalAxis;
    private float _rollAxis;
    private float _verticalAxis;


    

    [Header("General Settings")]
    [SerializeField] private float _rotateSpeed;


    [Header("Timers")]

    [SerializeField] private float _defaultRotTimerMax;

    [SerializeField] private float _defaultShootTimer;

    private float _shootCDtimer;
    private float _rocketCDTimer;

    private List<int> deneme;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _isRight = true;
    }


    void Update()
    {
        RotateAircraft();
        AircraftDefault();
        MoveForward();
        Shoot();
        ControlMovementSpeed();
    }


    private void RotateAircraft()
    {

        _rollAxis = Input.GetAxis("Horizontal");

        _verticalAxis = Input.GetAxis("Vertical");

       

        if (Input.GetKey(KeyCode.Q))
        {
            _horizontalAxis = -1;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            _horizontalAxis = 1;
        }
        else
        {
            _horizontalAxis = 0;
        }

        transform.Rotate(_verticalAxis * (_rotateSpeed / 2) * Time.deltaTime, _horizontalAxis * (_rotateSpeed / 6) * Time.deltaTime, -_rollAxis * (_rotateSpeed / 0.75f) * Time.deltaTime);


       

    }


    private void MoveForward()
    {
        transform.position += transform.forward * Time.deltaTime * _moveSpeed;
    }

    private void AircraftDefault()
    {

        if (_rollAxis == 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0f), 0.0015f);
        }
    }

    private void Shoot()
    {
        if (_target != null)
        {
            Vector3 planeToEnemy = _target.transform.position - transform.position;

            float dotProductValue = Vector3.Dot(transform.forward.normalized, planeToEnemy.normalized);

            if (dotProductValue >= 0.8f)
            {
                _isLocked = true;
            }
            else
            {
                _isLocked = false;
            }
        }
        else
        {
            _isLocked = false;
        }
        

        

        if (Input.GetMouseButton(0) && _shootCDtimer <= 0)
        {

            foreach (var item in _weapons)
            {
                Instantiate(_bullet, item.transform.position, item.transform.rotation);
            }

            _shootCDtimer = _defaultShootTimer;
        }

        if (_shootCDtimer >= 0)
        {
            _shootCDtimer -= Time.deltaTime;
        }

        if (Input.GetMouseButton(1) && _rocketCDTimer <= 0)
        {
            if (_isRight)
            {
                GameObject rocketRight = Instantiate(_rocket, _rocketLaunchers[0].transform.position, _rocketLaunchers[0].transform.rotation);

                if (_isLocked)
                {
                    rocketRight.GetComponent<Bullet>().followTarget = _enemyTarget.transform;
                    rocketRight.GetComponent<Bullet>().isFollowing = true;
                }

                _isRight = false;
            }
            else
            {
                GameObject rocketLeft = Instantiate(_rocket, _rocketLaunchers[1].transform.position, _rocketLaunchers[1].transform.rotation);

                if (_isLocked)
                {
                    rocketLeft.GetComponent<Bullet>().followTarget = _enemyTarget.transform;
                    rocketLeft.GetComponent<Bullet>().isFollowing = true;
                }

                _isRight = true;
            }

            _rocketCDTimer = _defaultShootTimer * 4;
        }

        if (_rocketCDTimer >= 0)
        {
            _rocketCDTimer -= Time.deltaTime;
        }
    }


    private void ControlMovementSpeed()
    {

        if (Input.GetKey(KeyCode.LeftControl) && _moveSpeed > 25)
        {
            _moveSpeed -= _moveSpeedControlMultiplier * Time.deltaTime;

            if (_moveSpeed < 25)
            {
                _moveSpeed = 25;
            }

        }
        else if (Input.GetKey(KeyCode.LeftShift) && _moveSpeed < 100)
        {
            _moveSpeed += _moveSpeedControlMultiplier * Time.deltaTime;

            if (_moveSpeed > 100)
            {
                _moveSpeed = 100;
            }
        }

  
    }

    private void HealthCheck()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

 

}
