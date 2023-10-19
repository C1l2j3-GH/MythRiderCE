using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1Boss_Atk3_FireWall : DealDamageCont_E
{
    [Header("Preset")]
    [SerializeField] private Animator _fireWallAnimtr;
    //[SerializeField] private GameObject[] _fireWallGOs;
    [SerializeField] private GameObject _fireWallPrefab;

    [Header("Config")]
    [SerializeField] private float _duration;
    [SerializeField] private float _xPosOffSet;
    [SerializeField] private float _yPosOffSet;
    [SerializeField] private Quaternion _rotationOffset;
    [Space(30f)]
    [SerializeField] private float _yScaleOffSet;
    [Space(30f)]


    [Header("Debug")]
    [SerializeField] public bool _isOnWall;
    [SerializeField] public bool _isOnGround;
    [SerializeField] private GameObject _fireWallGO;
    [SerializeField] private float _tempX;
    [SerializeField] private float _tempY;

    //[SerializeField] private int _fireWallCount;


    // Start is called before the first frame update
    void Start()
    {
        _fireWallAnimtr = GetComponent<Animator>();
        Destroy(gameObject, _duration);
        _tempX = _xPosOffSet;
        _tempY = _yPosOffSet;

        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlaySF("Xuanwu's firewave");
        }
    }

    private void Update()
    {
        if (_isOnGround)
        {
            _xPosOffSet = _tempX;
            _yPosOffSet = 0f;
        }

        if (_isOnWall)
        {
            _yPosOffSet = _tempY;
            _xPosOffSet = 0f;
        }
    }
    public void SpawnNewFireWall()
    {
        Vector3 tempPos = new(transform.position.x - _xPosOffSet, transform.position.y + _yPosOffSet, transform.position.z);
        //_fireWallGO = Instantiate(_fireWallPrefab, tempPos, _rotationOffset);
        _fireWallGO = Instantiate(_fireWallPrefab, tempPos, transform.rotation);
        _fireWallGO.SetActive(false);
        Vector3 tempScale = new(transform.localScale.x, transform.localScale.y + _yScaleOffSet, transform.localScale.z);
        _fireWallGO.transform.localScale = tempScale;
        _fireWallGO.SetActive(true);
        _fireWallAnimtr.SetBool("loopFire", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LeftWall") || collision.gameObject.CompareTag("RightWall"))
        {
            if (!_isOnWall)
            {
                _isOnWall = true;
            }

            if (_isOnGround)
            {
                _fireWallAnimtr.SetBool("loopFire", true);
            }
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            if (!_isOnGround)
            {
                _isOnGround = true;
            }

            if (_isOnWall)
            {
                _fireWallAnimtr.SetBool("loopFire", true);
            }
        }
    }
    //public void ActivateNextFireWall()
    //{
    //    if(_fireWallCount < 5)
    //    {
    //        _fireWallGOs[_fireWallCount].SetActive(true);
    //        _fireWallCount++;
    //    }
    //    else
    //    {
    //        _fireWallAnimtr.SetBool("loopFire", true);
    //    }
    //}

}
