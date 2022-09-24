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
        //タイムラインなどで呼び出す歩行アニメ
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
        //タイムラインなどで呼び出す攻撃アニメ
        animator.SetBool("anime_stand_attack", attack);
    }
}
