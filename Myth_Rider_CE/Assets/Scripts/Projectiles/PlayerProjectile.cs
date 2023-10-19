using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public bool isFacingRight;
    public float pjtDamage;
    ////private float _xScale;
    [SerializeField] private Animator _animComponent;
    [SerializeField] private GameObject _hitPE;

    public int _projectileID;
    // Start is called before the first frame update
    void Awake()
    {
        _animComponent = GetComponent<Animator>();
        Debug.Log(_animComponent);
        ////isFacingRight = GameObject.FindGameObjectWithTag("Player").GetComponent<Nina_Movement>().isFacingRight;
        ////_xScale = GameObject.FindGameObjectWithTag("Player").transform.localScale.x;

        FlipProjectile();

        switch (_projectileID)
        {
            default:
                _animComponent.Play("Base Normal Projectile");
                //Debug.Log("Projectile 1 is playing");

                break;
            case 2:
                _animComponent.Play("Base Charged Projectile");
                //Debug.Log("Projectile 2 is playing");

                break;
            case 3:

                break;
            case 4:

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ////if (isFacingRight == true)
        ////{
        ////    FlipProjectile();
        ////}

        Destroy(gameObject, 3f);
    }

    ////private void FlipProjectile()
    ////{
    ////    isFacingRight = !isFacingRight;
    ////    Vector3 theScale = transform.localScale;
    ////    theScale.x *= -1;
    ////    transform.localScale = theScale;
    ////}
    private void FlipProjectile()
    {
        if (Nina_Movement._isFacingRight && !Nina_Attack._isOnLeftWall && !Nina_Attack._isOnRightWall)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (Nina_Movement._isFacingRight && Nina_Attack._isOnLeftWall && !Nina_Attack._isOnRightWall)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (Nina_Movement._isFacingRight && !Nina_Attack._isOnLeftWall && Nina_Attack._isOnRightWall)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (!Nina_Movement._isFacingRight && !Nina_Attack._isOnLeftWall && !Nina_Attack._isOnRightWall)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (!Nina_Movement._isFacingRight && Nina_Attack._isOnLeftWall && !Nina_Attack._isOnRightWall)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (!Nina_Movement._isFacingRight && !Nina_Attack._isOnLeftWall && Nina_Attack._isOnRightWall)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        #region Old Triggers
        //// if the bullet hits enemy
        //if (collision.gameObject.CompareTag("Enemy1"))
        //{
        //    // get current enemy health and reduce it
        //    GameObject.FindGameObjectWithTag("Enemy1").GetComponent<Enemy1_AI>().currentEnemy1HP = 
        //    GameObject.FindGameObjectWithTag("Enemy1").GetComponent<Enemy1_AI>().currentEnemy1HP - pjtDamage;

        //    //Debug.Log("Enemy1Hit");
        //    Destroy(gameObject); // destroy the bullet
        //}

        //// if the bullet hits enemy
        //if (collision.gameObject.CompareTag("Enemy2"))
        //{
        //    // get current enemy health and reduce it
        //    GameObject.FindGameObjectWithTag("Enemy2").GetComponent<TBossAI>()._currentTBossHP = 
        //    GameObject.FindGameObjectWithTag("Enemy2").GetComponent<TBossAI>()._currentTBossHP - pjtDamage;

        //    //Debug.Log("Enemy2Hit");
        //    Destroy(gameObject); // destroy the bullet
        //}

        //if (collision.gameObject.CompareTag("L1Boss"))
        //{
        //    // get current enemy health and reduce it
        //    collision.GetComponent<L1BossAI>()._currentL1BossHP -= pjtDamage;

        //    //Debug.Log("Enemy2Hit");
        //    Destroy(gameObject); // destroy the bullet
        //}

        //if (collision.gameObject.CompareTag("L1BossDrone"))
        //{
        //    // get current enemy health and reduce it
        //    collision.GetComponent<L1Boss_Atk4_Drone>()._currentDroneHP -= pjtDamage;

        //    //Debug.Log("Enemy2Hit");
        //    Destroy(gameObject); // destroy the bullet
        //}

        //if (collision.gameObject.CompareTag("L2Boss"))
        //{
        //    // get current enemy health and reduce it
        //    collision.GetComponent<L2BossAI>()._currentL2BossHP -= pjtDamage;

        //    //Debug.Log("Enemy2Hit");
        //    Destroy(gameObject); // destroy the bullet
        //}
        #endregion

        if(collision.GetComponent<IFTakeDamage_E>() != null)
        {
            IFTakeDamage_E damagable = collision.GetComponent<IFTakeDamage_E>();
            damagable.TakeDamage(pjtDamage);
            Instantiate(_hitPE, transform.position, transform.rotation);

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("EnemyPlatform") || collision.gameObject.CompareTag("Platform"))
        {
            //Debug.Log("PlatformHit");
            Instantiate(_hitPE, transform.position, transform.rotation);
            Destroy(gameObject); // destroy the bullet
        }
    }
}
