using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class EnemyControlGB : MonoBehaviour
{
    //����HP
    int hp = 0;
    //�G���̃I�[�f�B�I�\�[�X
    AudioSource audioSource_enemy;
    //���G����
    int muteki_time = 60;
    //�J�E���g
    int count = 0;
    //���G�t���O
    bool muteki = false;
    //�v���C���[�h���
    int def_player = 0;
    //��b�_���[�W
    int damage_kiso = 0;
    //����
    int random_num = 0;
    //�����_���[�W
    int damage_total = 0;
    bool tween_swich = false;
    Tween tween;

    int akuseDef, bouguDef;



    [Header("�q�b�g�}�[�N")]
    public GameObject hitmark;
    [Header("���S�I�u�W�F�N�g")]
    public GameObject dead;
    [Header("�펞���G")]
    public bool mutekiMode;
    [Header("�G�l�~�[�̖��O")]
    public string objName;
    [Header("���x��")]
    public float lv = 1;
    [Header("�ő�HP")]
    public int hp_MAX = 1;
    [Header("�U����")]
    public int atk = 1;
    [Header("�����")]
    public int def = 1;
    [Header("�o���l")]
    public int exp = 0;
    [Header("����")]
    public int gold = 0;
    [Header("�h���b�v�A�C�e��")]
    public GameObject dropItem;
    [Header("�h���b�v��0�`100")]
    public int dropProbability = 0;

    //[Header("�v���C���[")]
    //public GameObject player_object;
    GameObject player_object;
    //�v���C���[���̃I�[�f�B�I�\�[�X
    AudioSource audioSource;
    //�T�E���h�}�l�[�W���[
    GameObject soundmanager;
    //�T�E���h�}�l�[�W���[�X�N���v�g
    SoundManagerScipt soundManagerScipt;
    //[Header("�Q�[���}�l�[�W���[")]
    //public GameObject gameManager;
    GameObject gameManager;
    //�ʏ�^�C�����C��
    PlayableDirector playable;
    //�_���[�W�^�C�����C��
    PlayableDirector damageplayable;
    //�_�ŃI�u�W�F�N�g
    GameObject blinkingObj;
    //public GameObject player_sound;
    //�X�v���C�g
    SpriteRenderer spriteRenderer;

    [Header("�G�ɑ̓����肵�����̃_���[�W��")]
    public AudioClip player_EnemyBodyHit;

    [Header("�g�p�O���[�o���ϐ�")]
    public GlobalVariables_ScriptableObject globalVariables;
    [Header("�A�C�e���f�[�^�x�[�X")]
    public ItemDataBaseGB itemDataBase;
    [Header("�_���[�W�|�b�v�A�b�v")]
    public GameObject damagePopUp;

    GameObject playerDefence;

    //Player�֌W�̃R���|�[�l���g���擾
    PlayerDamageControlGB player;
    PlayerControlGB playerControl;

    MessageMini messageMini;
    // Start is called before the first frame update
    void Start()
    {

        //�v���C���[����
        player_object = GameObject.FindWithTag("Player");
        //�Q�[���}�l�W���[�擾
        gameManager= GameObject.FindWithTag("GameManager");
        //�T�E���h�}�l�[�W���[�擾
        soundmanager= GameObject.FindWithTag("SoundManager");
        soundManagerScipt = soundmanager.GetComponent<SoundManagerScipt>();
        //�ő�HP������HP�ɑ��
        hp = hp_MAX;
        //���g�̃I�[�f�B�I�\�[�X�擾
        audioSource_enemy = GetComponent<AudioSource>();
        //�_��DOTween
        //tween = GetComponent<Tween>();
        tween = DOTween.TweensById("tenmetu")[0];
        player = player_object.GetComponent<PlayerDamageControlGB>();
        playerControl = player_object.GetComponent<PlayerControlGB>();
        //audioSource�擾
        audioSource = player_object.GetComponent<AudioSource>();
        //�I�[�f�B�I�e�X�g
        //audioSource.PlayOneShot(player_EnemyBodyHit);
        //���b�Z�[�W�\���E�B���h�E���e�L�X�g�擾
        messageMini = gameManager.GetComponent<MessageMini>();
        //�X�v���C�g�����_���[�擾
        spriteRenderer = GetComponent<SpriteRenderer>();
        //�^�C�����C���擾
        playable = GetComponent<PlayableDirector>();
        //�_�ŃI�u�W�F�N�g�ƃ^�C�����C���擾
        blinkingObj = transform.GetChild(2).gameObject;
        Debug.Log("�q�I�u�W�F�N�g" + blinkingObj);
        damageplayable = blinkingObj.GetComponent<PlayableDirector>();
    }


    void Update()
    {
        if (globalVariables.obj_controller == true)
        {
            if (muteki == true & count <= muteki_time)
            {
                count++;
                //damageplayable.Play();
                //Tween_damage_Play();
            }
            else
            {
                count = 0;
                muteki = false;
                //Tween_damage_Pause();
                //Debug.Log("Tween off");


            }
        }

        //HP0�ɂȂ�����o���l���v���C���[�Ɏ擾�����Ď��S�I�u�W�F�N�g�\��or���S�^�C�����C���N��
        if(hp <= 0)
        {
            GameObject obj= Instantiate(dead, transform.position, transform.rotation);
            //obj.GetComponent<Transform>().transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            playerControl.Exp(exp);
            Debug.Log("�Ԃ��|����exp�Q�b�g" + exp);
            Destroy(gameObject);
        }
        //�|�[�Y����������~�܂�
        if (globalVariables.obj_controller == true)
        {
            TimelineResume();
        }
        else
        {
            TimelinePause();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //�I�u�W�F�N�g�ɐG�ꂽ�v���C���[�Ƀ_���[�W
        if (collision.gameObject.tag == "PlayerDefence" & player.muteki == false & globalVariables.obj_controller==true)
        {
            Debug.Log("�����蔻�������");

            //�_���[�W�v�Z
            //�E�v���C���[���󂯂�_���[�W
            //��b�_���[�W = �U���� - �h���
            //�_���[�W = ��b�_���[�W +�i���x�� * 0.25�̗���)
            
            if (itemDataBase.soubi_akuse == -1)
            {
                akuseDef = 0;
            }
            else
            {
                akuseDef = itemDataBase.akuse_def[itemDataBase.soubi_akuse];
            }

            if (itemDataBase.soubi_bougu == -1)
            {
                bouguDef = 0;
            }
            else
            {
                bouguDef = itemDataBase.bougu_def[itemDataBase.soubi_bougu];
            }
            
            damage_kiso = atk - (globalVariables.def + akuseDef + bouguDef);
            int a = (int)Mathf.Floor(lv * 0.25f);
            if (a == 0) a = 1;
            damage_total = damage_kiso + (Random.Range(0, a+1));
            Debug.Log("�v���C���[ "+damage_total + "�_���[�W");
            audioSource.PlayOneShot(player_EnemyBodyHit);

            player.Damage(damage_total);
        }

        //�v���C���[�̕����U������ɐG�ꂽ�G�l�~�[���_���[�W
        if (collision.gameObject.tag == "PlayerAttack" & muteki == false & mutekiMode==false & globalVariables.controller==true)
        {
            
            /*
             * �E�G�ɗ^����_���[�W�i�����j
             * ��b�_���[�W=�U����-�h���
             * �_���[�W=��b�_���[�W+(�U����+���탌�x��/10�̗���)�~�i���x��/8)
            */
            int atk_total = itemDataBase.buki_atk + globalVariables.str;
            int rnd= (atk_total + itemDataBase.buki_lv) / 10;
            if (rnd == 0) rnd = 1;
            damage_kiso = atk_total - def;
            damage_total = damage_kiso + (Random.Range(0,rnd)) * (globalVariables.lv / 8);
            hp -= damage_total;
            hp = Mathf.Clamp(hp, 0, 9999);
            Debug.Log("�G " + damage_total + "�_���[�W");
            audioSource.PlayOneShot(soundManagerScipt.player_atk_damage);
            if (muteki == false)
            {
                //�q�b�g�}�[�N
                if (hp > 0) 
                    Instantiate(hitmark, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
                //�_���[�W�|�b�v�A�b�v
                string ddd = damage_total.ToString();
                GameObject pop = Instantiate(damagePopUp, transform.position, transform.rotation);
                pop.GetComponentInChildren<Text>().text = ddd;





                Debug.Log("�G�l�~�[�_���[�W�󂯂Ė��G���ԓ���");
                muteki = true;
            }
            //�G�l�~�[��񃁃b�Z�[�W��\��
            messageMini.EnemyInformation(objName,(int)lv,hp,hp_MAX);
        }
        if(collision.gameObject.tag == "OnBecameCollider" & globalVariables.obj_controller == true)
        {



        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "OnBecameCollider" & globalVariables.obj_controller == true)
        {


        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {



        
    }
    private void OnBecameVisible()
    {
        //�J�����ɕ\�������Ǝw��^�C�����C�����Đ������
        playable.Play();
        enabled = true;
        //spriteRenderer.enabled = true;
    }

    private void OnBecameInvisible()
    {
        //�J�������痣���Ǝw��^�C�����C������~�����
        //playable.Pause();
        enabled = false;
        //spriteRenderer.enabled = false;

    }
    public void TimelinePlay()
    {
        //�Đ�
        playable.Play();
    }

    public void TimelinePause()
    {
        //�ꎞ��~
        playable.Pause();
    }

    public void TimelineResume()
    {
        //�ĊJ
        playable.Resume();
    }

    public void TimelineStop()
    {
        //��~
        playable.Stop();
    }

    public void Damage(int damage)
    {
        //�G�Ƀ_���[�W��^����
        Instantiate(hitmark, transform.position, transform.rotation);
        hp -= Mathf.Clamp(damage - def, 0, 999);
        audioSource_enemy.PlayOneShot(soundManagerScipt.player_atk_damage);
        Debug.Log(damage + "�_���[�W�c�G��HP" + hp);
        if (hp <= 0)
        {
            /*if (timeline_use == false)
            {
                playable.Stop();
            }
            */
            //animator.SetBool("anime_dead", true);
            Debug.Log("�G����" + hp);
            Dead();

        }


    }


    public void Dead()
    {
        Destroy(gameObject);
        //gameObject.SetActive(false);//��\��
        //Instantiate(dead, transform.position, transform.rotation);
    }

    void ItemDrop()
    {
        Instantiate(dropItem, transform.position, transform.rotation);
    }

    void Tween_damage_Play()
    {
        if (tween_swich == false)
        {
            damageplayable.Play();
        }
        tween_swich = true;

    }
    void Tween_damage_Pause()
    {
        if (tween_swich == true)
        {
            damageplayable.Pause();
        }
        tween_swich = false;
    }

}
