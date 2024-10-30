using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적개체 스크립트 
public class Enemy : MonoBehaviour
{
    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {

    }

    public void EnemyOnClick()
    {
        anim.SetTrigger("Hit");

    }

    // 적이 체력이 다닳았을때 사망 처리를 위한 스크립트 
    public void EnemyDie()
    {
        Destroy(gameObject);
        Debug.Log("사망");
    }
}
