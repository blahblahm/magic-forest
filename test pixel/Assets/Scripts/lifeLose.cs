using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lifeLose : MonoBehaviour
{
    private numberOfLifes NumberOfLifes;
    private Vector3 respawnPoint;
	private int life = 3;


	void Awake() 
	{
		NumberOfLifes = GameObject.Find ("lifeNumber").GetComponent<numberOfLifes> ();
	}

    void Start () {
        respawnPoint = transform.position;
		NumberOfLifes.lifeCounter = life;
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
		
        if (collision.tag == "Bounds")
        {
			Death();
        }
		else if (collision.tag == "Checkpoint")
		{
			respawnPoint = transform.position;
		}
     }

	public void Death() {
		life--;
		NumberOfLifes.lifeCounter = life;
	    transform.position = respawnPoint;
		if (NumberOfLifes.lifeCounter == 0) {
			SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
		}
	}
}
