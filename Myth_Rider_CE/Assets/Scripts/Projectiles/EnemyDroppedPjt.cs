using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDroppedPjt : DealDamageOnce_E
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
}
