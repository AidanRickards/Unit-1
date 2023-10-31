using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator anim;
    public GameObject weapon;
    public int bombCount = 1;
    int direction = 1;
    public int playerHealth;
    SpriteRenderer sr;
    Rigidbody2D rb;
    void Start()
    {
        print("start");
        anim = GetComponent<Animator>(); // ***
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("jump", false);
        anim.SetBool("walk", false);
        anim.SetBool("attack", false);
        anim.SetBool("sprint", false);
        float speed = 7.5f;


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


        if ((Input.GetKey("w") == true) && (onground == true))
        {
            rb.velocity = new Vector2(rb.velocity.x, 3);

        }






        if (Input.GetKey("d") == true)
        {
            sr.flipX = false;
            anim.SetBool("walk", true);
            print("Player pressed right");
            transform.position = new Vector2(transform.position.x + (speed * Time.deltaTime), transform.position.y);

        }
        if (Input.GetKey("a") == true)
        {
            anim.SetBool("walk", true);
            sr.flipX = true;
            print("Player pressed left");
            transform.position = new Vector2(transform.position.x + (-(speed) * Time.deltaTime), transform.position.y);
        }

        if (Input.GetKey(KeyCode.LeftShift) == true && Input.GetKey("d") == true)
        {
            speed = (speed + 1);
            anim.SetBool("sprint", true);
            print("Player pressed right");
            transform.position = new Vector2(transform.position.x + (speed * Time.deltaTime), transform.position.y);

        }
        if (Input.GetKey(KeyCode.LeftShift) == true && Input.GetKey("a") == true)
        {
            speed = (speed + 1);
            print("Player pressed left");
            anim.SetBool("sprint", true);
            transform.position = new Vector2(transform.position.x + (-(speed) * Time.deltaTime), transform.position.y);

        }

        if (sr.flipX == false)
        {
            direction = 1;
        }

        if (sr.flipX == true)
        {
            direction = -1;
        }


        if (Input.GetKey("r") == true)
        {
            anim.SetBool("attack", true);

        }

        int moveDirection = 1;
        if (Input.GetKeyDown("r") && bombCount > 0)
        {
            bombCount--;
            
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
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            playerHealth--;
            if (playerHealth < 1)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
