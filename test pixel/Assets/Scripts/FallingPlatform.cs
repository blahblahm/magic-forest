using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector2 startPos;

    public bool respawn = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            Invoke("DropPlatform", 0.2f);
        }
    }

    void DropPlatform()
    {
        rb.isKinematic = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bounds" && respawn)
        {
            Invoke("Respawn", 1f);
        }
    }

    void Respawn()
    {
        rb.isKinematic = true;
        rb.velocity = new Vector3(0, 0, 0);
        transform.position = startPos;
    }
}
