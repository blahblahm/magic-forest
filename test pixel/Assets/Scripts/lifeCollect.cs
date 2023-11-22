using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeCollect : MonoBehaviour
{
    private numberOfLifes NumberOfLifes;
    private AudioSource audioLife;

    void Awake(){

        NumberOfLifes = GameObject.Find("lifeNumber").GetComponent<numberOfLifes>();
        
        audioLife = GameObject.Find("audioLife").GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && NumberOfLifes.lifeCounter < 3)
        {
            NumberOfLifes.lifeCounter++;
            gameObject.SetActive(false);
            
            audioLife.Play();
        }
        else 
        {
          gameObject.SetActive(false); 
          audioLife.Play(); 
        }
    }
}
