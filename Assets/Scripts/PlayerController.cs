using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float liftingForce;

    public bool jumped;
    public bool doubleJumped;

    private Rigidbody2D rb;
    public float startingY;

    public Transform groundCheck;
    public bool grounded;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        startingY = transform.position.y + 0.03f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.inGame) return;

        if (jumped && transform.position.y <= startingY)
        {
            jumped = doubleJumped = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Skoczyłem");
            if (!jumped)
            {
                rb.velocity = new Vector2(0f, jumpForce);
                jumped = true;
            }
            else if (!doubleJumped)
            {
                rb.velocity = new Vector2(0f, jumpForce);
                doubleJumped = true;
            }
        }

        if (Input.GetMouseButton(0))
        {
            rb.AddForce(new Vector2(0f, liftingForce * Time.deltaTime));
        }
    }

    //Metoda zostanie automatycznie wywołana w momencie zderzenia z jakimkolwiek
    //colliderem na którym ustawione jest pole IsTrigger

    void OnTriggerEnter2D(Collider2D other)
    {
        //sprawdzamy czy zderzyliśmy się z przeszkodą
        if (other.CompareTag("Obstacle"))
        {
            //Wywołujemy metodę odpowiedzialną za śmierć gracza
            PlayerDeath();
        }
    }
    void PlayerDeath()
    {
        //Zamrażamy fizykę gracza (pozostanie on wtedy w miejscu w którym przegrał
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        //Wywołujemy Game Over na managerze gry:
        GameManager.instance.GameOver();
    }

}