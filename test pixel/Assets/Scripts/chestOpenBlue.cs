using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chestOpenBlue : MonoBehaviour
{
    private numberOfKeys NumberOfKeys;

    [SerializeField] private Text locked;
    [SerializeField] private Text unlocked;
    private Animator anim;
    private bool openingAllowed;
    private potionAppearBlue potion;
    Collider2D box;
    [SerializeField] private AudioSource chestSound;

    void Awake(){

        NumberOfKeys = GameObject.Find("keyNumber").GetComponent<numberOfKeys>(); 
        potion = GameObject.Find("potion_blue").GetComponent<potionAppearBlue>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        locked.gameObject.SetActive(false);
        unlocked.gameObject.SetActive(false);
        box = GetComponent<Collider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (openingAllowed && Input.GetKeyDown(KeyCode.E))
        {
            OpenUp();
            Invoke("LateActive", 1f);
            openingAllowed = false;
            box.enabled = false;
            chestSound.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && NumberOfKeys.counter > 0)
        {
            unlocked.gameObject.SetActive(true);
            openingAllowed = true;
        }
        else if (other.gameObject.tag == "Player" && NumberOfKeys.counter == 0) {
            locked.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            unlocked.gameObject.SetActive(false);
            locked.gameObject.SetActive(false);
            openingAllowed = false;
        } 
    }

    private void OpenUp()
        {
            anim.SetBool("isOpen", true);
            NumberOfKeys.counter--;
            unlocked.gameObject.SetActive(false);
        }

    public void LateActive() {
        potion.Active();
    }
    
}   
