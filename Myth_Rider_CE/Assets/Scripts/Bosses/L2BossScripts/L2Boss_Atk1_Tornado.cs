using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Boss_Atk1_Tornado : MonoBehaviour
{
    [Header("Config")]
    [Tooltip("L2Boss Tornado Config")]
    [SerializeField] private float _force = 100f;
    [SerializeField] private float _ninaMSPenalty = 0.1f;
    [SerializeField] private float _ninaSlowedMS = 3f;

    [Header("Debug")]
    [Tooltip("L2Boss Tornado Debug")]
    [SerializeField] private Vector2 _ninaDir;
    [SerializeField] private float _ninaTempLMoveSpd;
    [SerializeField] private float _ninaTempRMoveSpd;
    [SerializeField] private GameObject _l2Boss;
    [SerializeField] private L2BossAI _l2BossAI;

    // Start is called before the first frame update
    void Start()
    {
        _l2Boss = GameObject.FindGameObjectWithTag("L2Boss");
        _l2BossAI = _l2Boss.GetComponent<L2BossAI>();
        
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlaySF("Qinglong's tornado projectile");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _ninaTempLMoveSpd = collision.GetComponent<Nina_Movement>()._playerLMoveSpeed;
            _ninaTempRMoveSpd = collision.GetComponent<Nina_Movement>()._playerRMoveSpeed;
            collision.GetComponent<Nina_Movement>()._playerLMoveSpeed = _ninaSlowedMS;
            collision.GetComponent<Nina_Movement>()._playerRMoveSpeed = _ninaSlowedMS;

            if (_ninaDir == Vector2.zero)
            {
                _ninaDir = L2BossFunctions.NormalizedDir(collision.transform.position, _l2Boss.transform.position);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (_ninaDir.x >= 0f)
            {
                _ninaDir.x = 0.5f;
            }

            if (_ninaDir.x < 0f)
            {
                _ninaDir.x = -0.5f;
            }

            Vector2 tempVelocity = collision.GetComponent<Rigidbody2D>().velocity;
            Vector2 tornadoDir = new(_ninaDir.x, 0);

            if (_ninaDir.x >= 0)
            {
                if (tempVelocity.x >= 1)
                {
                    collision.GetComponent<Nina_Movement>()._playerLMoveSpeed = _ninaSlowedMS;
                    collision.GetComponent<Nina_Movement>()._playerRMoveSpeed += _ninaMSPenalty;
                    ////collision.GetComponent<Rigidbody2D>().velocity += new Vector2(-0.01f, 0);
                    ////collision.GetComponent<Rigidbody2D>().velocity = new(0, collision.GetComponent<Rigidbody2D>().velocity.y);
                    //collision.GetComponent<Rigidbody2D>().AddForce(_force * Time.deltaTime * tornadoDir, ForceMode2D.Force);
                    collision.GetComponent<Rigidbody2D>().AddForce(_force * tornadoDir, ForceMode2D.Force);
                    //collision.GetComponent<Rigidbody2D>().velocity = new Vector2(-2 * Time.deltaTime, tempVelocity.y);
                    Debug.Log("Tornado pushing right!");
                }
            }

            else if (_ninaDir.x < 0)
            {
                if (tempVelocity.x < 1)
                {
                    collision.GetComponent<Nina_Movement>()._playerRMoveSpeed = _ninaSlowedMS;
                    collision.GetComponent<Nina_Movement>()._playerLMoveSpeed += _ninaMSPenalty;
                    ////collision.GetComponent<Rigidbody2D>().velocity += new Vector2(-0.01f, 0);
                    ////collision.GetComponent<Rigidbody2D>().velocity = new(0, collision.GetComponent<Rigidbody2D>().velocity.y);
                    //collision.GetComponent<Rigidbody2D>().AddForce(_force * Time.deltaTime * tornadoDir, ForceMode2D.Force);
                    collision.GetComponent<Rigidbody2D>().AddForce(_force * tornadoDir, ForceMode2D.Force);
                    //collision.GetComponent<Rigidbody2D>().velocity = new Vector2(-2 * Time.deltaTime, tempVelocity.y);
                    Debug.Log("Tornado pushing right!");
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Nina_Movement>()._playerLMoveSpeed = _ninaTempLMoveSpd;
            collision.GetComponent<Nina_Movement>()._playerRMoveSpeed = _ninaTempRMoveSpd;
        }
    }

    public void TornadoEnd()
    {
        if (_l2BossAI != null)
        {
            _l2BossAI.Attack1TornadoEnd();
        }
    }
}
