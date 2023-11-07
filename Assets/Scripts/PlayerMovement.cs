using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Animator anim;
    public Text bombs;
    public GameObject weapon;
    public int bombCount = 3;
    int direction = 1;
    public int playerHealth;
    SpriteRenderer sr;
    Rigidbody2D rb;
    HealthManager health;
    void Start()
    {
        print("start");
        anim = GetComponent<Animator>(); // ***
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        health = GameObject.FindWithTag("Health").GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("jump", false);
        anim.SetBool("walk", false);
        anim.SetBool("attack", false);
        float speed = 7.5f;
        rb.velocity = new Vector2(0, rb.velocity.y);


        Color hitColor = Color.white;
        bool onground = false;
        float laserlength = 0.1f;
        Vector3 rayOffset = new Vector3(0, -1.7f, 0);
        Vector3 rayOffset2 = new Vector3(-0.5f, -1.6f, 0);
        Vector3 rayOffset3 = new Vector3(0.5f, -1.6f, 0);

        RaycastHit2D hit = Physics2D.Raycast(transform.position + rayOffset, Vector2.down, laserlength);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + rayOffset2, Vector2.down, laserlength);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position + rayOffset3, Vector2.down, laserlength);

        if ((hit.collider != null) || (hit2.collider != null) || (hit3.collider != null))
        {
            hitColor = Color.red;
        }
        Debug.DrawRay(transform.position + rayOffset, Vector2.down * laserlength, hitColor);
        Debug.DrawRay(transform.position + rayOffset2, Vector2.down * laserlength, hitColor);
        Debug.DrawRay(transform.position + rayOffset3, Vector2.down * laserlength, hitColor);

        if ((hit.collider != null) || (hit2.collider != null) || (hit3.collider != null))
        {
            onground = true;
            anim.SetBool("jump", false);
        }

        if (onground == false)
        {
            anim.SetBool("jump", true);
        }


        if ((Input.GetKey("w") == true) || (Input.GetKey(KeyCode.Space) == true))
        {
            if (onground == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, 6);
            }
        }


        bombs.text = "Bomb Count: " + bombCount.ToString();


        if (Input.GetKey("d") == true)
        {
            sr.flipX = false;
            anim.SetBool("walk", true);
            print("Player pressed right");
            rb.velocity = new Vector2((speed), rb.velocity.y);

        }
        if (Input.GetKey("a") == true)
        {
            anim.SetBool("walk", true);
            sr.flipX = true;
            print("Player pressed left");
            rb.velocity = new Vector2(-(speed), rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.LeftShift) == true && Input.GetKey("d") == true)
        {
            speed = (speed + 2);
            print("Player pressed right");
            rb.velocity = new Vector2((speed), rb.velocity.y);

        }
        if (Input.GetKey(KeyCode.LeftShift) == true && Input.GetKey("a") == true)
        {
            speed = (speed + 2);
            print("Player pressed left");
            rb.velocity = new Vector2(-(speed), rb.velocity.y);

        }

        if (sr.flipX == false)
        {
            direction = 1;
        }

        if (sr.flipX == true)
        {
            direction = -1;
        }

        if (Input.GetKeyDown("r") && bombCount > 0)
        {
            anim.SetBool("attack", true);
        }

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            health.TakeDamage(20);
        }
    }
    public void BombThrow()
    {
        bombCount--;
        int moveDirection = 1;

        // Instantiate the bullet at the position and rotation of the player
        GameObject clone;
        clone = Instantiate(weapon, transform.position, transform.rotation);


        // get the rigidbody component
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();

        int power = (direction) * 15;
        // set the velocity
        rb.velocity = new Vector3(power * moveDirection, 0, 0);


        // set the position close to the player
        rb.transform.position = new Vector3(transform.position.x, transform.position.y +
        2, transform.position.z + 1);
    }
}   
