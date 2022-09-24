using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GlobalVariables", menuName = "ScriptableObjects/GlobalVariables", order = 1)]
public class GlobalVariables_ScriptableObject : ScriptableObject
{
    //�O���[�o���ϐ��X�N���v�g

    [Header("���x��")]
    public int lv = 1;
    [Header("�ő�HP")]
    public int hp_max = 1;
    [Header("����HP")]
    public int hp = 1;
    [Header("�ő�MP")]
    public int mp_max = 1;
    [Header("����MP")]
    public int mp = 1;
    [Header("�U����")]
    public int str = 1;
    [Header("�h���")]
    public int def = 1;
    [Header("�o����")]
    public int keiken = 0;
    [Header("������")]
    public int gold = 0;
    [Header("EXP")]
    public int exp = 0;
    [Header("�ݐ�EXP")]
    public int exp_total = 0;
    [Header("�����x")]
    public int inrando = 0;
    [Header("�G�b�`")]
    public int h = 0;
    [Header("�Z�N�n��")]
    public int sekuhara = 0;
    [Header("�t�F��")]
    public int fera = 0;
    [Header("�Z�b�N�X")]
    public int sex = 0;
    [Header("�A�i��")]
    public int anal = 0;
    [Header("�J�ڍ��W")]
    public Vector2 transitionPos;
    [Header("�R���g���[���[�̎�t����itrue�Ŏ󂯕t���\�Ɂj")]
    public bool controller = true;
    [Header("�I�u�W�F�N�g����̎�t����itrue�Ŏ󂯕t���\�Ɂj")]
    public bool obj_controller = true;
    [Header("�I�u�W�F�N�g����")]
    public bool reborn = false;
    [Header("�I�u�W�F�N�g�S����")]
    public bool dead = false;
    [Header("���폊���t���O")]
    public bool bukishoji = false;
    [Header("���j���[�J�[�\���J�E���g")]
    public int menuCount = 0;
    [Header("�A�C�e���J�[�\���J�E���g")]
    public int itemCount = 0;
    [Header("���@�J�[�\���J�E���g")]
    public int magicCount = 0;
    [Header("�h��J�[�\���J�E���g")]
    public int bouguCount = 0;
    [Header("�A�N�Z�T���[�J�[�\���J�E���g")]
    public int akuseCount = 0;
    [Header("BGM����")]
    public float bgmVolume = 1f;
    [Header("SE����")]
    public float seVolume = 1f;
    [Header("�t�@�C��No")]
    public int fileNum = 0;
    [Header("���S���[�h�itrue��R18���[�h�j")]
    public bool r18;
    [Header("�J�ڃt���O")]
    public bool transitionFlag = false;
    [Header("���݃V�[����")]
    public string nowScene_name = "GB_Main";
    [Header("�폜�V�[����")]
    public string delScene_name = "GB_Main";







}
