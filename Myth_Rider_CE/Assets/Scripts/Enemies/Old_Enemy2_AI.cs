using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Old_Enemy2_AI : MonoBehaviour
{
    [Header("Enemy2 Stats")]
    [Tooltip("Enemy2 Statistics")]
    public float maxEnemy2HP;
    public float currentEnemy2HP;
    public float enemy2HPRegenRate;
    public float enemy2Damage;


    [Header("Enemy2 Atk")]
    [Tooltip("Enemy2 Attack Status")]
    public GameObject topFireport;
    public GameObject frontFireport;
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public Rigidbody2D enemyPjtTop;
    public Rigidbody2D enemyPjtFront;
    public Rigidbody2D spawnedCreeps;
    private Rigidbody2D rb2D;
    private Rigidbody2D spawnedCreepsRB2D1;
    private Rigidbody2D spawnedCreepsRB2D2;
    public Transform rushTarget;
    public Transform originalPos;
    //private Vector3 originalPos;
    public float topFireCD = 30f;
    //public float topCDCount;
    public float frontFireCD = 30f;
    //public float frontCDCount;
    public float swapSpriteTime = 30f;
    //public float swapTimeCount;
    public float rushTime = 2f;
    public float rushTimeCount = 2f;
    public float rushSpd;
    public float returnSpd;
    public float creepSpd;
    public int pjtUpSpeed;
    public int pjtSideSpeed;
    //public bool isFireTop;
    //public bool isFireFront;
    public Sprite currentSprite;
    public Sprite[] enemySprite;
    //public bool isSwapSprite;
    public bool startAction;
    public int currentAction;
    public int lastUsedAction;
    private int pjtDirection; 
    


    // Start is called before the first frame update
    void Start()
    {
        currentEnemy2HP = maxEnemy2HP;
        //topCDCount = topFireCD;
        currentSprite = enemySprite[0];
        //isFireFront = false;
        //isFireTop = true;
        //isSwapSprite = false;
        startAction = true;
        rb2D = GetComponent<Rigidbody2D>();
        rushTimeCount = rushTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemy2HP <= 0)
        {
            Destroy(gameObject);
        }

        if (startAction == true)
        {
            StartCoroutine(AttackPattern());
        }
    }

    private IEnumerator AttackPattern()
    {
        startAction = false;

        if (currentAction == 0) //FireTop Attack Pattern
        {
            for (int b = 0; b < 5 ; b++)
            {
                Instantiate(enemyPjtTop, topFireport.transform.position, transform.rotation);
                //GameObject.FindGameObjectWithTag("EnemyPjt").GetComponent<EnemyProjectile>().target =
                //GameObject.FindGameObjectWithTag("EnemyPjt").GetComponent<EnemyProjectile>().finalTarget;
                //GameObject.FindGameObjectWithTag("EnemyPjt").GetComponent<EnemyProjectile>().target =
                //GameObject.FindGameObjectWithTag("Player").gameObject;
                yield return new WaitForSeconds(topFireCD);
            }

            currentAction = RandomAttack();

        }

        if (currentAction == 1) //Swap Sprite
        {
            if (currentSprite == enemySprite[0])
            {
                for (int a = 1; a <= 2; a++)
                {
                    currentSprite = enemySprite[a];
                    gameObject.GetComponent<SpriteRenderer>().sprite = currentSprite;
                    yield return new WaitForSeconds(swapSpriteTime);

                }


                currentAction = 2;
            }

            else if (currentSprite == enemySprite[2])
            {
                for (int c = 1; c >= 0; c--)
                {
                    currentSprite = enemySprite[c];
                    gameObject.GetComponent<SpriteRenderer>().sprite = currentSprite;
                    yield return new WaitForSeconds(swapSpriteTime);

                    
                }

                currentAction = 0;
            }

        }

        if (currentAction == 2) //FireFront Attack Pattern
        {
            for (int i = 0; i < 3; i++)
            {
               Instantiate(enemyPjtFront, frontFireport.transform.position, transform.rotation);
               yield return new WaitForSeconds(frontFireCD);
            }

            currentAction = RandomAttack();
        }

        if (currentAction == 3) //Spawn Creeps Attack Pattern
        {
            spawnedCreepsRB2D1 = Instantiate(spawnedCreeps, spawnPoint1.transform.position, transform.rotation);
            spawnedCreepsRB2D2 = Instantiate(spawnedCreeps, spawnPoint2.transform.position, transform.rotation);
            spawnedCreepsRB2D1.AddForce(new Vector2(-creepSpd, 0), ForceMode2D.Impulse);
            spawnedCreepsRB2D2.AddForce(new Vector2(-creepSpd, 0), ForceMode2D.Impulse);

            lastUsedAction = 3;
            currentAction = RandomAttack();

            yield return new WaitForSeconds(3f);
        }

        if (currentAction == 4) //Rush Forward Attack Pattern
        {
            
            //originalPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, rushTarget.position, rushSpd * Time.fixedDeltaTime);
            //rb2D.velocity = transform.right * -rushSpd;
            rushTimeCount -= Time.deltaTime;


            if (rushTimeCount <= 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, originalPos.position, returnSpd * Time.fixedDeltaTime);
                //rb2D.velocity = transform.right * -rushSpd;
                //AddForce(new Vector2(-creepSpd, 0), ForceMode2D.Impulse);
            }
            rushTimeCount = rushTime;
            //Debug.Log("Attack 4");
            lastUsedAction = 4;
            currentAction = RandomAttack();

            yield return new WaitForSeconds(3f);
        }

        startAction = true;
    }

    int RandomAttack()
    {
        var randomCount = Random.Range(1, 4);
        int randomAtkNo = 0;

            if (randomCount == 1)
            {
                randomAtkNo = 1;
            }
            else if (randomCount == 2 && lastUsedAction != 3)
            {
                randomAtkNo = 3;
            }
        //else if (randomCount == 3 && lastUsedAction != 4)
        //{
        //    randomAtkNo = 4;
        //}

        return randomAtkNo;
    }

    //topCDCount -= Time.deltaTime;

    ////Rigidbody2D projectileRB2D;

    //if (topCDCount <= 0)
    //{
    //    isFireTop = true;
    //    topCDCount = topFireCD;
    //}

    //if (isFireTop) //FireTop Attack Pattern
    //{
    //    isFireTop = false;
    //    //projectileRB2D = Instantiate(enemyPjt, fireport.transform.position, transform.rotation);
    //    Instantiate(enemyPjt, topFireport.transform.position, transform.rotation);
    //    //projectileRB2D.AddForce(new Vector2(pjtDirection * pjtSideSpeed, pjtUpSpeed), ForceMode2D.Impulse);
    //    topCDCount = topFireCD;
    //}

    //if (!isSwapSprite && !isFireFront) //FireTop Attack Pattern
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        //topCDCount -= Time.deltaTime;

    //        //if (topCDCount <= 0)
    //        //{
    //        Instantiate(enemyPjtTop, topFireport.transform.position, transform.rotation);
    //        yield return new WaitForSeconds(topFireCD);
    //        //topCDCount = topFireCD;
    //        //}
    //    }

    //    isFireFront = true;
    //    isFireTop = false;
    //    isSwapSprite = true;
    //}

    //if (!isFireTop && !isFireFront)
    //{
    //    if (currentSprite == enemySprite[0])
    //    {
    //        for (int a = 1; a <= 2; a++)
    //        {
    //            currentSprite = enemySprite[a];
    //            gameObject.GetComponent<SpriteRenderer>().sprite = currentSprite;
    //            yield return new WaitForSeconds(swapSpriteTime);
    //        }

    //        isFireFront = true;
    //        isFireTop = false;
    //        isSwapSprite = false;
    //    }

    //    else if (currentSprite == enemySprite[2])
    //    {
    //        for (int a = 1; a >= 0; a--)
    //        {
    //            currentSprite = enemySprite[a];
    //            gameObject.GetComponent<SpriteRenderer>().sprite = currentSprite;
    //            yield return new WaitForSeconds(swapSpriteTime);
    //        }

    //        isFireFront = false;
    //        isFireTop = true;
    //        isSwapSprite = false;
    //    }

    //}

    //if (!isFireTop && !isSwapSprite) //FireFront Attack Pattern
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        //frontCDCount -= Time.deltaTime;

    //        //if (frontCDCount <= 0)
    //        //{ 
    //        Instantiate(enemyPjtFront, frontFireport.transform.position, transform.rotation);
    //        yield return new WaitForSeconds(frontFireCD);
    //        //    frontCDCount = frontFireCD;
    //        //}
    //    }

    //    isFireFront = false;
    //    isFireTop = true;
    //    isSwapSprite = true;
    //}
}
