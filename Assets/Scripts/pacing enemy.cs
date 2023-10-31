using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pacingenemy : MonoBehaviour
{
    SpriteRenderer sr;
    float speed = 3;
    Animator anim;
    public int enemyHealth;
    public int bombAmount;
    PlayerMovement player;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }


    void Update()
    {
        Color hitColor = Color.white;

        float laserlength = 0.5f;
        Vector3 rayOffset = new Vector3(1.3f, 0, 0);
        Vector3 rayOffset2 = new Vector3(-1.3f, 0, 0);

        RaycastHit2D hit = Physics2D.Raycast(transform.position + rayOffset, Vector2.right, laserlength);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + rayOffset2, Vector2.left, laserlength);
        if ((hit.collider != null) || (hit2.collider != null))
        {
            hitColor = Color.red;
        }
        Debug.DrawRay(transform.position + rayOffset, Vector2.right * laserlength, hitColor);
        Debug.DrawRay(transform.position + rayOffset2, Vector2.left * laserlength, hitColor);

        transform.position = new Vector2(transform.position.x + (-(speed) * Time.deltaTime), transform.position.y);

        if (hit.collider != null)
        {
            speed = (speed) = 3;
            sr.flipX = true;

        }
        if (hit2.collider != null)
        {
            speed = (speed) = -3;
            sr.flipX = false;

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
                player.bombCount = player.bombCount + bombAmount;
            }
        }
    }
}
    