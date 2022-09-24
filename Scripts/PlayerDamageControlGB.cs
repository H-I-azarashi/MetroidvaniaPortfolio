using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerDamageControlGB : MonoBehaviour
{
    //�v���C���[�擾�p�ϐ�
    //GameObject plyer_object;
    //PlayerControlGB player;
    //���G�t���O
    public bool muteki;
    [Header("�_���[�W�󂯂Ă���̖��G����")]
    public int muteki_time = 60;
    public int count = 0;

    //PlayerControlGB
    PlayerControlGB playerControl;
    [Header("�g�p�O���[�o���ϐ�")]
    public GlobalVariables_ScriptableObject globalVariables;
    [Header("�_���[�W�|�b�v�A�b�v")]
    public GameObject damagePopUp;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponent<PlayerControlGB>();
        muteki = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(muteki==true& count <= muteki_time)
        {
            count++;
            playerControl.Tween_damage_Play();
        }
        else
        {
            count = 0;
            muteki = false;
            playerControl.Tween_damage_Pause();


        }
    }

    public void Damage(int damage)
    {
        if(muteki==false)
        {
            //HP��������
            
            globalVariables.hp = Mathf.Clamp((globalVariables.hp- damage), 0, 999);
            //�_���[�W�|�b�v�A�b�v
            string ddd = damage.ToString();
            GameObject pop = Instantiate(damagePopUp, transform.position, transform.rotation);
            pop.GetComponentInChildren<Text>().text = ddd;
            //UI�ĕ`��
            playerControl.UIdraw();




            Debug.Log("�v���C���[�_���[�W�󂯂Ė��G���ԓ���");
            muteki = true;
        }
    }
}
