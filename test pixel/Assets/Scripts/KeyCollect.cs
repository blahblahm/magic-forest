using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollect : MonoBehaviour
{
    private numberOfKeys NumbersOfKeys;
    private AudioSource audioKey;

    void Awake(){

        NumbersOfKeys = GameObject.Find("keyNumber").GetComponent<numberOfKeys>(); // Pronalazi se GO brojKljuceva na kome treba da se pronađe skripta BrojacKljuceva.
        
        audioKey = GameObject.Find("audioKey").GetComponent<AudioSource>();
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
        if (other.gameObject.tag == "Player")
        {
            NumbersOfKeys.counter++;
            gameObject.SetActive(false);
            
            audioKey.Play();
        }
    }
}
