using UnityEngine;

public class L1Boss_Atk4_Drone_Laser : DealDamageOnce_E
{
    [Header("Config")]
    [SerializeField] private float _droneLaserMoveSpd;
    [SerializeField] private float _droneLaserRotateSpd;
    
    [Header("Debug")]
    [SerializeField] private GameObject _nina;
    [SerializeField] private Vector3 _ninaTempPos;
    [SerializeField] private Vector3 _droneLaserDir;
    [SerializeField] private Rigidbody2D _droneLaserRb2D;
    [SerializeField] private bool _isStartShooting;

    private void Start()
    {
        _nina = GameObject.FindGameObjectWithTag("Player");
        _droneLaserRb2D = GetComponent<Rigidbody2D>();
        _ninaTempPos = NinaPreviousPos();
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlaySF("Xuanwu's Drone laser");
        }
    }
    private void FixedUpdate()
    {
        if (!_isStartShooting)
        {
            //Vector3 droneLaserDir = HomingProjectileDir(_nina.transform.position, transform.position);
            _droneLaserDir = HomingProjectileDir(_nina.transform.position, transform.position);
            _droneLaserRb2D.velocity = new Vector2(_droneLaserDir.x * _droneLaserMoveSpd, _droneLaserDir.y * _droneLaserMoveSpd);
            _isStartShooting = true;
        }

        _droneLaserRb2D.angularVelocity = AimTarget(transform.up, _droneLaserRotateSpd, _droneLaserDir);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")
            || collision.gameObject.CompareTag("LeftWall") 
            || collision.gameObject.CompareTag("RightWall")
            || collision.gameObject.CompareTag("Platform"))
        {
            gameObject.SetActive(false);
        }
    }

    public Vector3 NinaPreviousPos()
    {
        Vector3 tempNinaPos = _nina.transform.position;
        return tempNinaPos;
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
}
