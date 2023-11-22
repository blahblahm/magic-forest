using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWalking : MonoBehaviour
{
   
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isRunning", true);
    }

    void Update(){
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else if (currentPoint == pointA.transform)
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
            changeDirection();
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
            changeDirection();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
	// void FixedUpdate () {
	//     GetComponent<Rigidbody2D>().velocity = new Vector2 (transform.localScale.x * maxVelocity, GetComponent<Rigidbody2D> ().velocity.y);
	
	//     Collider2D[] frontalniSudari = Physics2D.OverlapPointAll (frontTest.position);
	
	//     foreach (Collider2D c in frontalniSudari) {
	// 	changeDirection();
	// }   

	// }
	
	void changeDirection()
	{
        Vector3 karScale = transform.localScale;
        karScale.x *= -1;
        transform.localScale = karScale;
	}
}
