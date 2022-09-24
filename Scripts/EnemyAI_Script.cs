using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Script : MonoBehaviour
{
    [Header("スタートまでの時間")]
    public float startTime = 0f;
    [Header("移動速度X")]
    public float moveSpeedX = 0f;
    [Header("移動速度Y")]
    public float moveSpeedY = 0f;
    [Header("目標位置")]
    public float pointX = 0f;
    [Header("目標時間")]
    public float pointTime = 0f;
    [Header("ウェイト")]
    public float waitTime = 0f;

    Rigidbody2D rd;
    bool startFlag = false;
    bool moveFlag = false;
    float xKakunou;
    float pointX2;
    float transformMemo;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        xKakunou = moveSpeedX;
        pointX2 = pointX + transform.position.x;
        transformMemo = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(Move());

    }

    private void Update()
    {
        //StartCoroutine(MoveUpdate());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    IEnumerator Move()
    {
        if (moveFlag == false & startFlag == false)
        {
            yield return new WaitForSeconds(startTime);
            moveFlag = true;
            startFlag = true;
        }


        //向き
        if (pointX > 0)
        {
            transform.localScale = new Vector2(-1, 1);
            moveSpeedX = xKakunou;
        }
        if (pointX < 0)
        {
            transform.localScale = new Vector2(1, 1);
            moveSpeedX = xKakunou * -1;
        }
        //移動
        if (moveFlag == true)
        {
            transform.Translate(new Vector2(moveSpeedX, moveSpeedY));
        }
        yield return new WaitForSeconds(pointTime);
        moveFlag = false;
        transform.Translate(new Vector2(0, 0));
        pointX2 = pointX + transform.position.x;
        yield return new WaitForSeconds(waitTime);
        moveFlag = true;

        /*
        transform.Translate(new Vector2(moveSpeedX, moveSpeedY));
        if (pointX > 0)
        {
            if (pointX2 > transform.position.x)
            {
                transform.Translate(new Vector2(0, 0));
                yield return new WaitForSeconds(waitTime);
                pointX2 = pointX + transform.position.x;
                transformMemo = transform.position.x;
            }
        }
        if (pointX < 0)
        {
            if (pointX2 < transform.position.x)
            {
                transform.Translate(new Vector2(0, 0));
                yield return new WaitForSeconds(waitTime);
                pointX2 = pointX + transform.position.x;
                transformMemo = transform.position.x;
            }
        }
        */

    }


}

