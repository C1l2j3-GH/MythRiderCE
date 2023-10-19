using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyProjectile : DealDamageOnce_E
{
    private GameObject tempTarget;
    private GameObject target;
    private Vector2 hitLocation;
    public float pjtSpeed;
    public float pjtRotateSpeed;
    private Rigidbody2D rb2D;
    public GameObject pjtEFX;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //tempTarget = GameObject.FindGameObjectWithTag("Player").gameObject;
        //target = tempTarget;
        //target = GameObject.FindGameObjectWithTag("Player").gameObject;
        target = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        hitLocation = new Vector2(target.transform.position.x - 2, target.transform.position.y - 3);
    }
    void FixedUpdate()
    {
        //Vector2 pjtDirection = (Vector2)target.transform.position - rb2D.position;
        Vector2 pjtDirection = hitLocation - rb2D.position;
        //Debug.Log(pjtDirection);
        pjtDirection.Normalize();
        float pjtRotateAmount = Vector3.Cross(pjtDirection, transform.up).z;
        rb2D.angularVelocity = -pjtRotateAmount * pjtRotateSpeed;
        rb2D.velocity = transform.up * pjtSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") 
            || collision.gameObject.CompareTag("EnemyPlatform") 
            || collision.gameObject.CompareTag("Player"))
        {
            Instantiate(pjtEFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
