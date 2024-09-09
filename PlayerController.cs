using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    float speedX, speedY;
    Rigidbody2D rb;
    public GameObject radius;

    // sound file for take damage
    public AudioClip takeDamage;

    //player health
    public int health = 100;
    public Image healthbar;
    public float overcharge = 0;
    private float overchargeMinimum = 0;
    //private float overchargeMaximum = 100;
    public Image meter;

    public Animator animator;

    private void Start()
    {
        //rotation is frozen in the RigidBody inspector
        rb = GetComponent<Rigidbody2D>();
        //radius = gameObject.transform.GetChild(1);
        healthbar.fillAmount = health / 100f;
        InvokeRepeating("MeterDown", 0.25f, 0.25f);
    }


    private void Update()
    {
        // Get input from the WASD keys
        speedX = Input.GetAxis("Horizontal") * moveSpeed; // A and D keys
        speedY = Input.GetAxis("Vertical") * moveSpeed; // W and S keys
        rb.velocity = new Vector2(speedX, speedY);
        animator.SetFloat("SpeedX", speedX);
        animator.SetFloat("SpeedY", speedY);
    }

    // on collision with enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(10);
            Debug.Log("Player Health: " + health);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Enemy")
        {
            trigger.gameObject.GetComponent<EnemyAI>().inRadius = true;
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Enemy")
        {
            trigger.gameObject.GetComponent<EnemyAI>().inRadius = false;
        }
    }

    public void TakeDamage(int damage)
    {
        AudioSource.PlayClipAtPoint(takeDamage, transform.position);
        health -= damage;
        if (health <= 0)
        {
            SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
            //Die();
        }
        healthbar.fillAmount = health / 100f;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void MeterDown()
    {
        //making the meter gradually decrease
        if (overcharge > overchargeMinimum)
        {
            overcharge = overcharge - 1;
        }
        if (overcharge > 100)
        {
            overchargeMinimum = 100;
            radius.transform.localScale = new Vector3 (1,1,1);

        }
        if (overcharge > 200)
        {
            overchargeMinimum = 200;
            moveSpeed = 15f;
        }
        if (overcharge > 300)
        {
            overchargeMinimum = 300;
            health = 100;
        }
        meter.fillAmount = overcharge / 300f;
    }

}