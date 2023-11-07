using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class enemybehaviour : MonoBehaviour
{
    public float speed;
    public int coinCount;
    public GameObject coin;
    public int enemyHealth;
    public int bombAmount;
    private float distance;
    SpriteRenderer sr;
    HelperScript helper;
    PlayerMovement player;



    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.AddComponent<SpriteRenderer>();
        helper = gameObject.AddComponent<HelperScript>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {



        helper.FlipObject(true);
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

        float dist = player.transform.position.x - transform.position.x;
        if( dist > 0 )
         {
            helper.FlipObject(false);
        }


    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bomb")
        {
            enemyHealth--;
            if (enemyHealth < 1)
            {
                Destroy(this.gameObject);
                for (int i = 0; i < coinCount; i++)
                {
                    Instantiate(coin, transform.position, transform.rotation);
                }
            }
        }
    }
}