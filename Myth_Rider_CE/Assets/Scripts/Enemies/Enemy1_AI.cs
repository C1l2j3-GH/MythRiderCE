using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy1_AI : DealDamageCont_E, IFTakeDamage_E
{
    [Header("Preset")]
    [SerializeField] private GameObject _skillPointGO;

    public float maxEnemy1HP;
    public float currentEnemy1HP;
    public float enemy1HPRegenRate;
    public float moveSpd;
    private Transform target;
    private Rigidbody2D rb2D;
    //public GameObject[] enemyGO;
    //public int currentEnemy;
    //public float minDistance;
    //public bool startFollow;
    // Start is called before the first frame update
    void Start()
    {
        //enemyDamage = 30f;
        currentEnemy1HP = maxEnemy1HP;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Nina_Movement.startFollow)
        {
            //transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpd * Time.fixedDeltaTime);
            ChasePlayer();
        }

        if (currentEnemy1HP <= 0)
        {
            ////Destroy(gameObject);
            Instantiate(_skillPointGO, new Vector3(transform.position.x, 3, transform.position.z), _skillPointGO.transform.rotation);
            gameObject.SetActive(false);
        }
    }

    void ChasePlayer()
    {
        if (transform.position.x < target.transform.position.x)
        {
            Vector3 newScale = new(-2.5f, transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
            rb2D.velocity = new Vector2(moveSpd, 0); 
        }

        //if (transform.position.x > target.transform.position.x)
        else
        {
            Vector3 newScale = new(2.5f, transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
            rb2D.velocity = new Vector2(-moveSpd, 0);
        }

        //else if (transform.position.x == target.transform.position.x)
        //{
        //    rb2D.velocity = new Vector2(0, 0);
        //}
    }

    public void TakeDamage(float damage)
    {
        currentEnemy1HP -= damage;
    }
}
