using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Script : MonoBehaviour
{
    [Header("�X�^�[�g�܂ł̎���")]
    public float startTime = 0f;
    [Header("�ړ����xX")]
    public float moveSpeedX = 0f;
    [Header("�ړ����xY")]
    public float moveSpeedY = 0f;
    [Header("�ڕW�ʒu")]
    public float pointX = 0f;
    [Header("�ڕW����")]
    public float pointTime = 0f;
    [Header("�E�F�C�g")]
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


        //����
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
        //�ړ�
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

