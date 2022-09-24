using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class ItemGetScript : MonoBehaviour
{
    [Header("�O���[�o���ϐ�")]
    public GlobalVariables_ScriptableObject globalVariables;
    [Header("�A�C�e���f�[�^�x�[�X")]
    public ItemDataBaseGB itemDataBase;
    public enum Shurui
    {
        Item,Magic,Bougu,Akuse

    }
    public enum Action
    {
        None,Delete

    }
    [Header("�ڐG������N��")]
    public bool tri = false;
    [Header("���")]
    public Shurui shurui;
    [Header("�A�C�e���ԍ�")]
    public int num;
    [Header("������")]
    public int zougen = 1;
    [Header("�A�C�e���I�u�W�F�N�g��������")]
    public bool action = false;
    [Header("�A�C�e���Q�b�g���b�Z�[�W")]
    public bool getMessage = false;

    //�Q�[���}�l�[�W���[
    GameObject gameManager;
    //�T�E���h�}�l�[�W���[�I�u�W�F�N�g
    GameObject soundManagerObj;
    //�T�E���h�}�l�[�W���[�֌W�̕ϐ�
    SoundManagerScipt soundManager;
    AudioSource audioSource;

    MessageMini messageMini;
    string file_name;
    bool del;
    //�J�E���g
    int count = 0;

    private void Start()
    {
        //�Q�[���}�l�[�W���[�擾
        gameManager= GameObject.Find("GameManager");
        //�T�E���h�}�l�[�W���[�擾
        soundManagerObj = GameObject.Find("SoundManager");
        soundManager = soundManagerObj.GetComponent<SoundManagerScipt>();
        audioSource = soundManagerObj.GetComponent<AudioSource>();
        //���b�Z�[�W�\���E�B���h�E���e�L�X�g�擾
        messageMini = gameManager.GetComponent<MessageMini>();
        //�t�@�C�����쐬
        file_name = "Save/date";
        file_name += globalVariables.fileNum.ToString();
        file_name += ".es3";
        //�A�C�e���I�u�W�F�N�g�����ςȂ������Ԃ����[�h
        if(del==true) Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tri == true & collision.gameObject.tag == "Player")
        {
            //���ʉ�
            audioSource.Stop();
            audioSource.PlayOneShot(soundManager.kettei_sound);
            //�A�C�e���Q�b�g����
            ItemGet();
        }

    }

    private void Update()
    {

    }

    void ItemGet()
    {
        //�A�C�e��
        if (shurui == Shurui.Item)
        {
            itemDataBase.item_shoji[num] += zougen;
            itemDataBase.item_shoji[num] = Mathf.Clamp(itemDataBase.item_shoji[num], 0, 99);
        }
        //���@
        if (shurui == Shurui.Magic)
        {
            itemDataBase.magic_shoji[num] += zougen;
            itemDataBase.magic_shoji[num] = Mathf.Clamp(itemDataBase.magic_shoji[num], 0, 99);
        }
        //�h��
        if (shurui == Shurui.Bougu)
        {
            itemDataBase.bougu_shoji[num] += zougen;
            itemDataBase.bougu_shoji[num] = Mathf.Clamp(itemDataBase.bougu_shoji[num], 0, 99);
        }
        //�A�N�Z�T���[
        if (shurui == Shurui.Akuse)
        {
            itemDataBase.akuse_shoji[num] += zougen;
            itemDataBase.akuse_shoji[num] = Mathf.Clamp(itemDataBase.akuse_shoji[num], 0, 99);
        }
        //�A�C�e���I�u�W�F�N�g����
        if (action == true)
        {
            Destroy(gameObject);
            del = true;
        }
        //�A�C�e���Q�b�g���b�Z�[�W�\��
        if (getMessage == true)
        {
            if (shurui == Shurui.Item) messageMini.ItemGet(itemDataBase.itemName[num].ToString());
            if (shurui == Shurui.Magic) messageMini.ItemGet(itemDataBase.magicName[num].ToString());
            if (shurui == Shurui.Bougu) messageMini.ItemGet(itemDataBase.bouguName[num].ToString());
            if (shurui == Shurui.Akuse) messageMini.ItemGet(itemDataBase.akuseName[num].ToString());
        }
    }

}
