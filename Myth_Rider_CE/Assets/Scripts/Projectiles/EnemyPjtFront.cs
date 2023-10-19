using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPjtFront : DealDamageOnce_E
{
    public float pjtSpeed;
    //public float dropPjtCD = 1f;
    public float pjtDropInterval = 1f;
    private Rigidbody2D rb2D;
    public Rigidbody2D droppedEnemyPjt;
    public bool isDropPjt;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        isDropPjt = true;
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.velocity = transform.right * -pjtSpeed;
        
        if (isDropPjt == true)
        {
            StartCoroutine(DropProjectile());
        }

        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")
            || collision.gameObject.CompareTag("EnemyPlatform")
            || collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DropProjectile()
    {
            for (int i = 0; i < 3; i++)
            {
                isDropPjt = false;
                Instantiate(droppedEnemyPjt, transform.position, transform.rotation);
                yield return new WaitForSeconds(pjtDropInterval);
                isDropPjt = true;
            }

    }
}
