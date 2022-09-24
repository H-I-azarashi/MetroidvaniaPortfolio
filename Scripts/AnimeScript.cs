using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeScript : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void WalkAnime(bool walk)
    {
        //�^�C�����C���ȂǂŌĂяo�����s�A�j��
        if (walk == true)
        {
            
            animator.SetFloat("anime_walk", 1f);
        }
        else
        {
            animator.SetFloat("anime_walk", 0);
            
        }

    }

    public void AttackAnime(bool attack)
    {
        //�^�C�����C���ȂǂŌĂяo���U���A�j��
        animator.SetBool("anime_stand_attack", attack);
    }
}
