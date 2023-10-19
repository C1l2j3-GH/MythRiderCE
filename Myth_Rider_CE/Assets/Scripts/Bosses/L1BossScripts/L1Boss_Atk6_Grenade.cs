using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1Boss_Atk6_Grenade : DealDamageOnce_E
{
    [Header("Config")]
    [Tooltip("Config")]
    [SerializeField] private float _isMoveGrenadeDuration = 2f;
    [SerializeField] private float _grenadeMoveSpd = 2f;
    [SerializeField] private float _grenadeRotateSpd = 10f;

    [Header("Debug")]
    [Tooltip("Debug")]
    [SerializeField] public int _id;
    [SerializeField] private L1BossAI _l1BossAI;
    [SerializeField] private float _isMoveGrenadeTimeCount = 0f;
    [SerializeField] private GameObject _nina;
    [SerializeField] private Rigidbody2D _grenadeRb2D;
    [SerializeField] private Vector3 _ninaPreviousPos;
    [SerializeField] private bool _getPreviousPos;

    // Start is called before the first frame update
    void Start()
    {
        _l1BossAI = GameObject.FindGameObjectWithTag("L1Boss").GetComponent<L1BossAI>();
        _nina = GameObject.FindGameObjectWithTag("Player");
        _grenadeRb2D = GetComponent<Rigidbody2D>();
        _getPreviousPos = true;

        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlaySF("Xuanwu's fire grenade");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(L1BossAI._atk6GrenadeCanChase)
        {
            if (_isMoveGrenadeTimeCount < _isMoveGrenadeDuration)
            {
                _isMoveGrenadeTimeCount += Time.deltaTime;
                Vector3 playerDir01 = HomingProjectileDir(_nina.transform.position, transform.position);
                _grenadeRb2D.velocity = new Vector2(playerDir01.x * _grenadeMoveSpd, playerDir01.y * _grenadeMoveSpd);
            }
        }
    }
    public Vector3 HomingProjectileDir(Vector3 targetPos, Vector3 projectilePos)
    {
        Vector3 atkDirection = targetPos - projectilePos;
        atkDirection.Normalize();
        return atkDirection;
    }

    //private IEnumerator MoveGrenade(float delay)
    //{
    //    _isMoveGrenade = false;
    //    Vector3 playerDir01 = HomingProjectileDir(_nina.transform.position, transform.position);
    //    _grenadeRb2D.velocity = new Vector2(playerDir01.x * _grenadeMoveSpd, playerDir01.y * _grenadeMoveSpd);
    //    yield return new WaitForSeconds(delay);

    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")
            || collision.gameObject.CompareTag("LeftWall") 
            || collision.gameObject.CompareTag("RightWall")
            || collision.gameObject.CompareTag("Platform"))
        {
            //gameObject.SetActive(false);
            //_l1BossAI._atk6GrenadeGOs.RemoveAt(_id);
            //_l1BossAI._atk6GrenadeGOs[_id] = null;
            _l1BossAI._atk6GrenadeGOs.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
