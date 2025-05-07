using System;
using UnityEngine;
using System.Collections;

public class Norak : MonoBehaviour
{
    public Character norak;
    bool died = false;
    private Animator animator;

    public norakattackPlayer swordScript;


    private void Start()
    {
        norak = new Character();
        norak.health = 1f;
        animator = GetComponent<Animator>();



    }

    private void Update()
    {
        if (!died && norak.health == 0)
        {
            
            

            StartCoroutine(DeadNorak());
            died = true;
        }
    }

    IEnumerator DeadNorak()
    {
        GetComponent<Norak_MLAgent>().enabled = false;
        swordScript.enabled = false;

        animator.SetTrigger("isNorakDeath");
        yield return null;
    }


}


