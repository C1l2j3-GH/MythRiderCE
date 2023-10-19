using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1Boss_Atk4_Drone : DealDamageOnce_E, IFTakeDamage_E
{
    [Header("Preset")]
    [SerializeField] private GameObject _droneLaserPrefab;

    [Header("Config")]
    public float _maxDroneHP = 70f;
    public float _currentDroneHP;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _droneMoveSpd;
    [SerializeField] private float _droneRotateSpd;
    [SerializeField] private float _droneShootCD;

    [Header("Debug")]
    [SerializeField] private GameObject _nina;
    [SerializeField] private L1BossAI _l1BossAI;
    [SerializeField] private Rigidbody2D _droneRb2D;
    [SerializeField] private float _distance;
    [SerializeField] private int _startAtking;
    [SerializeField] private bool _isCrashingCD;
    [SerializeField] private bool _isCrashing;
    public Vector3 _targetPos;



    // Start is called before the first frame update
    void Start()
    {
        _currentDroneHP = _maxDroneHP;
        _l1BossAI = GameObject.FindGameObjectWithTag("L1Boss").GetComponent<L1BossAI>();
        _droneRb2D = GetComponent<Rigidbody2D>();
        _nina = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _distance = CalculateDistance(_targetPos, transform.position);
        
        if(_distance <= _minDistance)
        {
            _droneRb2D.velocity = Vector2.zero;
            
            if (_startAtking == 0)
            {
                _startAtking = 1;
            }
        }

        if (_startAtking == 1)
        {
            StartCoroutine(StartAtkCD(_droneShootCD));
        }

        if(_startAtking >= 3)
        {
            if (!_isCrashingCD)
            {
                StartCoroutine(CrashCD(_droneShootCD));
            }

            if(_isCrashing)
            {
                Vector3 droneDir = HomingProjectileDir(_nina.transform.position, transform.position);
                _droneRb2D.angularVelocity = AimTarget(transform.up, _droneRotateSpd, droneDir);
                _droneRb2D.velocity = new Vector2(droneDir.x * _droneMoveSpd, droneDir.y * _droneMoveSpd);
            }
        }

        if (_currentDroneHP <= 0)
        {
            _l1BossAI._droneGOs.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_startAtking >= 3)
        {
            if (collision.gameObject.CompareTag("LeftWall") 
                || collision.gameObject.CompareTag("RightWall")
                || collision.gameObject.CompareTag("Platform"))
            {
                //gameObject.SetActive(false);
                _l1BossAI._droneGOs.Remove(gameObject);
                Destroy(gameObject);
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                //gameObject.SetActive(false);
                _l1BossAI._droneGOs.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }

    private float CalculateDistance(Vector3 firstPos, Vector3 secondPos)
    {
        float Distance = (firstPos - secondPos).sqrMagnitude;
        return Distance;
    }

    public Vector3 HomingProjectileDir(Vector3 targetPos, Vector3 projectilePos)
    {
        Vector3 atkDirection = targetPos - projectilePos;
        atkDirection.Normalize();
        return atkDirection;
    }

    public float AimTarget(Vector3 rightAxis, float rotateSpeed, Vector3 _atkDirection)
    {
        float angularVel;
        //_atkDirection.Normalize();

        Vector3 rotateAmount = Vector3.Cross(_atkDirection, rightAxis);
        angularVel = rotateAmount.z * rotateSpeed;

        return angularVel;
    }
    private IEnumerator StartAtkCD(float cd)
    {
        for (int i = 0; i < 2; i++)
        {
            _startAtking++;
            Instantiate(_droneLaserPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(cd);
        }
    }

    private IEnumerator CrashCD(float cd)
    {
        _isCrashingCD = true;
        yield return new WaitForSeconds(cd);
        _isCrashing = true;
    }

    public void TakeDamage(float damage)
    {
        _currentDroneHP -= damage;
    }
}
