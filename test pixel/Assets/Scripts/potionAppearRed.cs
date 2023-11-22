using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class potionAppearRed : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Text potion_pickUp;
    [SerializeField] private Image hud_potion_red;
    public bool pickUpAllowed;
    public int disappear;

    void Start()
    {
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
        potion_pickUp.gameObject.SetActive(false);
        disappear = 0;
    }

    void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            pickUp();
            disappear++;
            gameObject.SetActive(false);
            
        }
    }

    public void Active()
    {
        gameObject.SetActive(true);
        anim.SetBool("chestOpen", true); 
        potion_pickUp.gameObject.SetActive(true);
        pickUpAllowed = true;
        
    }

    public void pickUp()
    {
        potion_pickUp.gameObject.SetActive(false);
        hud_potion_red.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        
    }

    public void Retreat()
    {
        hud_potion_red.GetComponent<Image>().color = new Color32(102, 101, 101, 100);
    }
    
}
