using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1Boss_Atk1_Laser : DealDamageCont_E
{
    [Header("Preset")]
    [SerializeField] private Animator _l1BossAnimtr;
    [SerializeField] private Animator _laserAnimtr;


    [Header("Config")]
    [SerializeField] private float _rayLength;
    [SerializeField] private Vector2 _posOffset;

    [Header("Debug")]
    [SerializeField] private Vector2 _rayDirection = Vector2.right;
    [SerializeField] private Vector2 _collisionPosition;
    [SerializeField] private GameObject _laserLeftVFXGO;
    [SerializeField] private GameObject _laserRightVFXGO;
    [SerializeField] private SpriteRenderer _laserLeftVFXSR;
    [SerializeField] private SpriteRenderer _laserRightVFXSR;

    // Start is called before the first frame update
    void Start()
    {
        //_l1BossAnimtr = GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>();
        _l1BossAnimtr = GameObject.FindGameObjectWithTag("L1BossAnimator").GetComponent<Animator>();
        _laserLeftVFXGO = GameObject.FindGameObjectWithTag("OnWallVFX_L");
        _laserLeftVFXSR = _laserLeftVFXGO.GetComponent<SpriteRenderer>();
        _laserRightVFXGO = GameObject.FindGameObjectWithTag("OnWallVFX_R");
        _laserRightVFXSR = _laserRightVFXGO.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayXuanWuIdle()
    {
        if (_l1BossAnimtr != null)
        {
            _l1BossAnimtr.SetBool("isShooting", false);
            _l1BossAnimtr.SetBool("isIdle", true);

            //Debug.Log("PlayXuanWuIdle");
        }

        transform.parent.gameObject.SetActive(false);
        //Debug.Log("Laser Disabled");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LeftWall") || collision.gameObject.CompareTag("RightWall"))
        {
            if (!L1BossAI._isFlippedL1Boss)
            {
                _laserLeftVFXSR.color = new(1, 1, 1, 1);
                //Debug.Log("PlayLeftLaserVFX");
            }
            else
            {
                _laserRightVFXSR.color = new(1, 1, 1, 1);
                //Debug.Log("PlayRightLaserVFX");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LeftWall") || collision.gameObject.CompareTag("RightWall"))
        {
            if (!L1BossAI._isFlippedL1Boss)
            {
                _laserLeftVFXSR.color = new(1, 1, 1, 0);
                //Debug.Log("StopLeftLaserVFX");
            }
            else
            {
                _laserRightVFXSR.color = new(1, 1, 1, 0);
                //Debug.Log("StopRightLaserVFX");
            }
        }
    }
}
