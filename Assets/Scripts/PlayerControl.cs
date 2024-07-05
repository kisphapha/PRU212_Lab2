using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float turningSpeed = 90f;
    private ParticleSystem dustParticles;
    private Rigidbody2D rb;
    public float torque = 4;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dustParticles = transform.Find("Dust Particles").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Rotate object to the left
            rb.AddTorque(torque);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Rotate object to the right
            rb.AddTorque(-torque);

        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            dustParticles.Play();
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            dustParticles.Stop();
        }
    }

}
