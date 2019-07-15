using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public AudioClip winningMusic;
    public AudioSource musicSource;
    private Rigidbody2D rb2d;
    private int score;
    private int life;

    public float speed;
    public float jumpForce;

    public Text scoreText;
    public Text winText;
    public Text lifeText;
    public Text endText;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator> ();
        score = 0;
        winText.text = "";
        endText.text = "";
        life = 3;
        SetAllText ();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKey("escape"))
        {
            Application.Quit(); 
        }
       
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                anim.SetInteger("State", 2);
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                anim.SetInteger("State", 0);
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                anim.SetInteger("State", 2);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                anim.SetInteger("State", 0);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                anim.SetInteger("State", 3);
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                anim.SetInteger("State", 0);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                anim.SetInteger("State", 5);
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                anim.SetInteger("State", 0);
            }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement * speed);
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow)) {
              
              rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
         
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag ("Pickup"))
        {
            other.gameObject.SetActive (false);
            score = score + 1;
            SetAllText ();
        }
        if (other.gameObject.CompareTag ("Enemy"))
        {
            other.gameObject.SetActive (false);
            life = life - 1;
            SetAllText ();
            DestroyGameObject();
        }
        if (score == 4)
         {
             transform.position = new Vector2(0f, -22.5f);
             life = 3;
            }
        
    }

    void SetAllText ()
    {
        scoreText.text = "Score: " + score.ToString();
        lifeText.text = "Lives: " + life.ToString();
        if (score >= 8)
        {
            winText.text = "You win!";
            musicSource.clip = winningMusic;
            musicSource.Play();
            Destroy(rb2d);
        }
        
    }
    void DestroyGameObject()
    { 
    if (life == 0)
        {
        Destroy(gameObject); 
        endText.text = "Game Over!";
    }
    }
    
}
