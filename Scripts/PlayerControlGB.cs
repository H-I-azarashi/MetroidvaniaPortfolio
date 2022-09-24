using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerControlGB : MonoBehaviour
{
    float bullet_count = 0, crouching;
    public float move;
    bool is_jump, bullet_ok, crouching_flag, damage_flag;
    string hp_s, mp_s;
    public bool forcedToEvent; //�����C�x���g�t���O�i�����A�j�����[�V�����p�j
    public static PlayerControl instance; //�V���O���g���p
                                          //�O���[�o���ϐ��̌Ăяo���錾
                                          //GlobalVariables gv;
                                          //GameObject obj_gv;
    //�������s�A�j���p�̃t���O
    bool walk_Kyousei = false;

    [Header("���s���x")]
    public float speed;
    [Header("�W�����v���x")]
    public float speed_jump;
    [Header("����HP")]
    public float hp_max = 8f;
    [Header("����MP")]
    public float mp_max = 30f;
    [Header("HP")]
    public float hp = 8f;
    [Header("MP")]
    public float mp = 30f;
    [Header("�U����")]
    public int atk = 3;
    [Header("�e��")]
    public float bullet_total_count = 0;
    [Header("�e")]
    public GameObject bullet;
    [Header("�q�b�g�}�[�N")]
    public GameObject hitmark;
    [Header("���x���A�b�v�|�b�v")]
    public GameObject levelUpPop;
    public bool lr;
    [Header("����HP�e�L�X�g�\��")]
    public Text hp_text;
    [Header("����MP�e�L�X�g�\��")]
    public Text mp_text;
    [Header("�ő�HP�e�L�X�g�\��")]
    public Text hp_max_text;
    [Header("�ő�MP�e�L�X�g�\��")]
    public Text mp_max_text;
    [Header("���x���e�L�X�g�\��")]
    public Text lv_text;
    [Header("HP�o�[�\��")]
    public Image hp_Bar;
    [Header("MP�o�[�\��")]
    public Image mp_Bar;
    [Header("�o���l�o�[�\��")]
    public Image exp_Bar;
    [Header("���@�����A�C�R��")]
    public List<Image> soubiIcon_magic = new List<Image>();
    [Header("�h����A�C�R��")]
    public List<Image> soubiIcon_bougu = new List<Image>();
    [Header("�A�N�Z�T���[�����A�C�R��")]
    public List<Image> soubiIcon_akuse = new List<Image>();
    [Header("���n")]
    public AudioClip chakuti;
    [Header("�W�����v��")]
    public AudioClip jumpSE;
    [Header("�v���C���[�U����")]
    public AudioClip player_atk_sound;
    [Header("�v���C���[�U���q�b�g��")]
    public AudioClip player_atk_damage;
    [Header("�񕜖��@")]
    public AudioClip cureSE;
    [Header("�����@")]
    public AudioClip fireSE;
    [Header("�X���@")]
    public AudioClip iceSE;
    [Header("�����@")]
    public AudioClip thunderSE;
    [Header("�G�ɑ̓����肵�����̃_���[�W��")]
    public AudioClip player_EnemyBodyHit;
    [Header("�v���C���[���S")]
    public AudioClip player_dead_sound;
    [Header("�g�p�O���[�o���ϐ�")]
    public GlobalVariables_ScriptableObject globalVariables;
    [Header("�A�C�e���f�[�^�x�[�X")]
    public ItemDataBaseGB itemDataBase;
    [Header("�g�p�o���l�e�[�u��")]
    public ExpTableScript expTable;
    [Header("�T�E���h�}�l�[�W���[")]
    public SoundManagerScipt soundManager;
    [Header("�w�肵���ꏊ�ɔz�u")]
    public bool haiti = false;
    [Header("���Ⴊ�ݓ���")]
    public bool shagami = true;


    AudioSource audioSource;


    Rigidbody2D rd;
    Vector2 pos, pos_hit;
    Transform tra;
    Animator animator;
    SpriteRenderer sprite;

    GameObject se_manager;

    Tween tween;
    bool tween_swich;
    bool levelup = false;

    string attakAnime, attakJumpAnime;

    //�U������Ɩh�䔻��ϐ�
    GameObject atk_col, def_col;
    float atkSX, defSX, atkSY, defSY;

    
    [Header("�X�^�[�g����ɒ��n�������Ȃ��Ȃ�悤�̐ڒn�t���O")]
    public bool settiSoundFlag = false;

    private void Awake()
    {

        //�w�肵���ꏊ�ɔz�u
        if (globalVariables.transitionFlag == true)
        {
            transform.position = globalVariables.transitionPos;
            globalVariables.transitionFlag = false;
        }


    }
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        tra = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        /*
        atk_col = GameObject.FindGameObjectWithTag("PlayerAttack");
        def_col = GameObject.FindGameObjectWithTag("PlayerDefence");

        atkSX = atk_col.transform.localScale.x;
        defSX = def_col.transform.localScale.x;
        atkSY = atk_col.transform.localScale.y;
        defSY = def_col.transform.localScale.y;
        */
        //�X�e�[�^�X��UI�ɔ��f
        UIdraw();
        Soubi();

        //�����O���t�B�b�N
        //animator.SetBool("anime_dress_kenari", true);

        //�_��DOTween
        tween = DOTween.TweensById("tenmetu")[0];

        //canvas���C���[�W�ƃe�L�X�g�擾

        //�W�����v�␳
        speed_jump *= 100;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (forcedToEvent == false & globalVariables.controller==true & walk_Kyousei==false)
        {
            move = Input.GetAxisRaw("Horizontal");
            //Debug.Log("Horizontal�̐��l "+move);

            //���s�A�j���i�W�����v���͕s�j
            if (is_jump == false)
            {
                animator.SetFloat("anime_walk", Mathf.Abs(move));
            }
            else
            {
                animator.SetFloat("anime_walk", 0);
            }
            //�ړ�
            rd.AddForce(new Vector2(move * speed, 0));

            //����
            if (move > 0)
            {
                transform.localScale = new Vector2(-1, 1);

            }
            if (move < 0)
            {
                transform.localScale = new Vector2(1, 1);
            }
        }

    }

    void Update()
    {
        crouching = Input.GetAxisRaw("Vertical");
        //���Ⴊ�ݒ��t���O
        if (shagami == true)
        {

            if (crouching != 0)
            {
                crouching_flag = true;
            }
            else
            {
                crouching_flag = false;
            }

            if (forcedToEvent == false)
            {
                //���Ⴊ��
                if (is_jump == false & globalVariables.controller == true)
                {
                    animator.SetFloat("anime_crouching_getdown_move", crouching);
                    if (crouching == 0)
                    {
                        is_jump = false;
                    }

                }
            }


        }



        //�W�����v
        if (Input.GetButtonDown("Jump") & bullet_ok == false & globalVariables.controller == true)
        {
            if (is_jump == false)
            {

                rd.AddForce(Vector2.up * speed_jump);

            }

        }
        animator.SetBool("anime_jump", is_jump);
        //globalVariables.obj_controller�������ɂȂ�����󒆂Œ�~����
        if (globalVariables.obj_controller == false)
        {
            rd.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rd.constraints = RigidbodyConstraints2D.None;
            rd.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        //�e���ˁi�{�^���������ςȂ��ŘA�˂ł��邪�蓮�A�ł���Α�R���Ă�I�ȁj
        if (globalVariables.controller == true)
        {
            if (Input.GetButton("Fire3"))
            {

                if (is_jump == true || crouching_flag == true)
                {
                    animator.SetBool("CB_BulletShot_Crouching", true);
                }
                else
                {
                    animator.SetBool("CB_BulletShot_stand", true);
                }

                shoot();
            }
            if (Input.GetButtonUp("Fire3"))
            {
                animator.SetBool("CB_BulletShot_Crouching", false);
                animator.SetBool("CB_BulletShot_stand", false);
                bullet_count = 0;
            }
        }


        //�������U��
        if (is_jump == false & globalVariables.controller == true & itemDataBase.soubi_bougu != -1)
        {
            if (crouching_flag == false)
            {
                if (Input.GetButtonDown("Fire1") && !animator.GetCurrentAnimatorStateInfo(0).IsName(attakAnime)
            && !animator.IsInTransition(0))
                {
                    forcedToEvent = true;
                    animator.SetFloat("anime_walk", 0);
                    animator.SetBool("anime_stand_attack", true);
                    if (shagami == true)
                    {
                        animator.SetFloat("anime_crouching_getdown_move", 0);
                    }
                    
                    
                }

            }

        }

        //���Ⴊ�݌��U��
        if (shagami == true & globalVariables.controller == true & itemDataBase.soubi_bougu != -1)
        {
            if (is_jump == false)
            {
                if (crouching_flag == true)
                {
                    if (Input.GetButtonDown("Fire1") && !animator.GetCurrentAnimatorStateInfo(0).IsName(attakAnime)
                && !animator.IsInTransition(0))
                    {
                        forcedToEvent = true;
                        animator.SetFloat("anime_walk", 0);
                        animator.SetBool("anime_crouching_attack", true);
                        //animator.SetFloat("anime_crouching_getdown_move", 0);
                        
                    }

                }

            }
        }
        //�W�����v���U��
        if (is_jump == true & globalVariables.controller == true & itemDataBase.soubi_bougu != -1)
        {
            if (crouching_flag == false)
            {
                if (Input.GetButtonDown("Fire1") && !animator.GetCurrentAnimatorStateInfo(0).IsName(attakJumpAnime)
                && !animator.IsInTransition(0))
                {
                    //forcedToEvent = true;
                    //animator.SetFloat("anime_walk", 0);
                    animator.SetBool("anime_jump_attack", true);
                    rd.AddForce(new Vector2(move, 0));
                    

                }

            }

        }
        //forcedToEvent��true�̎��̏������X
        if (forcedToEvent == true)
        {

        }
        //Debug.Log("�L�[����t���O"+forcedToEvent);


    }
    public void Ground_OnTriggerEnter2D()
    {
        //�n�ʂɐڒn�������̔���
        is_jump = false;
        //forcedToEvent = false;
        animator.SetBool("anime_jump_attack", false);
        animator.SetBool("anime_jump", is_jump);
        Debug.Log("�ݒu����");
        if (settiSoundFlag == true)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(soundManager.chakuti);
        }
        settiSoundFlag = true;
    }

    public void Ground_OnTriggerExit2D()
    {
        //�n�ʂ��痣�ꂽ���̔���
        is_jump = true;
        animator.SetFloat("anime_walk", 0);
        Debug.Log("�o��");
        audioSource.Stop();
        audioSource.PlayOneShot(soundManager.jumpSE);
    }

    void shoot()
    {
        //�e���˔��� MP��0�ɂȂ�����łĂȂ�����



    }

    void AnimeEnd()
    {
        animator.SetBool("anime_stand_attack", false);
        animator.SetBool("anime_jump_attack", false);
        if (shagami == true)
        {
            animator.SetBool("anime_crouching_attack", false);
        }
        forcedToEvent = false;
        is_jump = false;
    }

    void Crouching()
    {
        forcedToEvent = false;
        animator.SetBool("anime_crouching", true);
        animator.SetBool("anime_crouching_getdown_move", false);
        is_jump = false;
        crouching_flag = true;

    }

    void Crouching_return()
    {
        is_jump = false;
        Debug.Log("�������オ���Ă�");

    }

    public void UIdraw()
    {
        //���l���f
        hp_text.text = globalVariables.hp.ToString();
        hp_max_text.text = globalVariables.hp_max.ToString();
        mp_text.text = globalVariables.mp.ToString();
        mp_max_text.text = globalVariables.mp_max.ToString();
        lv_text.text = globalVariables.lv.ToString();

        //�o�[���f
        hp_Bar.rectTransform.localScale = new Vector3(((float)globalVariables.hp / (float)globalVariables.hp_max) * 1f, 1f, 1f);
        mp_Bar.rectTransform.localScale = new Vector3(((float)globalVariables.mp / (float)globalVariables.mp_max) * 1f, 1f, 1f);
        exp_Bar.rectTransform.localScale = new Vector3(((float)globalVariables.exp / (float)expTable.nextExp[globalVariables.lv]) * 1f, 1f, 1f);
    }

    public void Exp(int aaa)
    {
        levelup = false;
        //�o���l�擾
        while (true)
        {
            if (aaa >= globalVariables.exp)
            {
                //���x���A�b�v���Ă�����x���[�v
                aaa -= globalVariables.exp;
                globalVariables.exp = expTable.nextExp[globalVariables.lv];

                globalVariables.lv += 1;
                globalVariables.hp_max += expTable.hp[globalVariables.lv];
                globalVariables.hp = globalVariables.hp_max;
                globalVariables.mp_max += expTable.mp[globalVariables.lv];
                globalVariables.mp = globalVariables.mp_max;
                globalVariables.str += expTable.str[globalVariables.lv];
                globalVariables.def += expTable.def[globalVariables.lv];
                
                levelup = true;
                Debug.Log("���[�v�� EXP=" + aaa);
            }
            else
            {
                //�o���l�e�[�u���̒l�ɖ����Ȃ��̂Ōo���l�i�[���ďI��
                globalVariables.exp -= aaa;
                Debug.Log("���[�v������ EXP=" + aaa);
                break;
            }
        }
        //���x���A�b�v���o
        if (levelup == true) StartCoroutine(LevelUp());
        //�Q�[�W�E���l���f
        UIdraw();
    }

    IEnumerator LevelUp()
    {
        globalVariables.controller = false;
        globalVariables.obj_controller = false;
        Instantiate(levelUpPop, transform.position, transform.rotation);
        yield return new WaitForSeconds(4f);
        globalVariables.controller = true;
        globalVariables.obj_controller = true;
        Debug.Log("���x���A�b�v�I");
    }
    public void Damage(int damage)
    {
        if (damage_flag == false)
        {
            //����t���O��؂��Ė��G�g���K�[�N��
            forcedToEvent = true;
            //�L�����A�j���؂�ւ�
            animator.SetFloat("anime_walk", 0);
            animator.SetBool("CB_BulletShot_Crouching", false);
            animator.SetBool("CB_BulletShot_stand", false);
            //HP�����炵��UI�ɔ��f

            //HP�o�[���f
            
            //�_���[�W�}�[�N�\��
            pos_hit = transform.position;
            Instantiate(hitmark, pos_hit, transform.rotation);
            //HP��0�ɂȂ�Ȃ�������_���[�W�A�N�V�����\�����ĕ��A�A0�Ȃ玀�S�A�N�V����
            if (hp > 0)
            {
                if (crouching == 0)
                {
                    animator.SetBool("anime_stand_damage_1", true);

                }
                else
                {
                    animator.SetBool("anime_crouching_damage", true);
                }

                if (transform.localScale.x == 1)
                {
                    rd.AddForce(new Vector2(4000, 0));
                }
                else
                {
                    rd.AddForce(new Vector2(-4000, 0));
                }
                Debug.Log("�_���[�W�󂯂�" + hp);
            }
            else
            {
                StartCoroutine(Gameover());
            }

        }

    }

    void Damage_re()
    {
        forcedToEvent = false;
        is_jump = false;
        damage_flag = false;
        Debug.Log("�_���[�W���A");
        animator.SetBool("anime_stand_damage_1", false);
        animator.SetBool("anime_crouching_damage", false);

    }

    IEnumerator Gameover()
    {
        //sprite.sprite = death_sprite;
        animator.SetBool("anime_Death", true);
        if (lr == false)
        {
            rd.AddForce(new Vector2(9000, 0));
        }
        else
        {
            rd.AddForce(new Vector2(-9000, 0));
        }
        Debug.Log("����");
        
        yield return new WaitForSeconds(2f);
        SceneManager.GetActiveScene();
    }

    //�e���ʉ��Đ�
    public void AttackSE()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(soundManager.player_atk_sound);
    }

    public void DamageSE()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(soundManager.player_EnemyBodyHit);
    }

    public void CureSE()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(soundManager.cureSE);
    }

    public void FireSE()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(soundManager.fireSE);
    }

    public void IceSE()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(soundManager.iceSE);
    }

    public void ThunderSE()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(soundManager.thunderSE);
    }

    public void DeadSE()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(soundManager.player_dead_sound);
    }

    public void Tween_damage_Play()
    {
        if (tween_swich == false)
        {
            DOTween.TweensById("tenmetu").ForEach((tween) =>
            {
                tween.Play();
            });
            //Debug.Log("Tween on");
        }
        tween_swich = true;

    }
    public void Tween_damage_Pause()
    {
        if (tween_swich == true)
        {
            DOTween.TweensById("tenmetu").ForEach((tween) =>
            {
                tween.Rewind();
            });
            //Debug.Log("Tween off");
        }
        tween_swich = false;
    }

    public void Soubi()
    {
        //�A�C�R�����Z�b�g
        for (int i = 0; i < soubiIcon_magic.Count; i++) soubiIcon_magic[i].gameObject.SetActive(false);
        for (int i = 0; i < soubiIcon_bougu.Count; i++) soubiIcon_bougu[i].gameObject.SetActive(false);
        for (int i = 0; i < soubiIcon_akuse.Count; i++) soubiIcon_akuse[i].gameObject.SetActive(false);
        //�A�C�R���\��
        if (itemDataBase.soubi_magic>=0) soubiIcon_magic[itemDataBase.soubi_magic].gameObject.SetActive(true);
        if (itemDataBase.soubi_bougu >= 0) soubiIcon_bougu[itemDataBase.soubi_bougu].gameObject.SetActive(true);
        if (itemDataBase.soubi_akuse >= 0) soubiIcon_akuse[itemDataBase.soubi_akuse].gameObject.SetActive(true);

        //�A�j���[�V�������Z�b�g
        animator.SetBool("anime_dress", false);
        animator.SetBool("anime_dress_kenari", false);
        animator.SetBool("anime_hadaka", false);
        animator.SetBool("anime_cross", false);
        animator.SetBool("anime_Sister", false);
        animator.SetBool("anime_Maid", false);
        animator.SetBool("anime_Bani", false);
        animator.SetBool("anime_Bani(kenzen)", false);
        switch (itemDataBase.soubi_bougu)
        {
            /*case -1:
                attakAnime = "Ginette_dress_ATTACK";
                attakJumpAnime = "Ginette_dress_Jump_ATTACK";
                animator.SetBool("anime_dress_kenari", true);
                break;
            */
            case 0:
                if (globalVariables.r18 == true)
                {
                    attakAnime = "Ginette_hadaka_ATTACK";
                    attakJumpAnime = "Ginette_hadaka_Jump_ATTACK";
                    animator.SetBool("anime_hadaka", true);
                }
                else
                {
                    attakAnime = "Ginette_dress_ATTACK";
                    attakJumpAnime = "Ginette_dress_Jump_ATTACK";
                    animator.SetBool("anime_dress_kenari", true);
                }
                break;
            case 1:
                attakAnime = "Ginette_dress_ATTACK";
                attakJumpAnime = "Ginette_dress_Jump_ATTACK";
                animator.SetBool("anime_dress_kenari", true);
                break;
            case 2:
                attakAnime = "Ginette_Cross_ATTACK";
                attakJumpAnime = "Ginette_Cross_Jump_ATTACK";
                animator.SetBool("anime_cross", true);
                break;
            case 3:
                attakAnime = "Ginette_Sister_ATTACK";
                attakJumpAnime = "Ginette_Sister_Jump_ATTACK";
                animator.SetBool("anime_Sister", true);
                break;
            case 4:
                attakAnime = "Ginette_Maid_ATTACK";
                attakJumpAnime = "Ginette_Maid_Jump_ATTACK";
                animator.SetBool("anime_Maid", true);
                break;
            case 5:
                if(globalVariables.r18 == true)
                {
                    attakAnime = "Ginette_Bani_ATTACK";
                    attakJumpAnime = "Ginette_Bani_Jump_ATTACK";
                    animator.SetBool("anime_Bani", true);
                }
                else
                {
                    attakAnime = "Ginette_Bani(kenzen)_ATTACK";
                    attakJumpAnime = "Ginette_Bani(kenzen)_Jump_ATTACK";
                    animator.SetBool("anime_Bani(kenzen)", true);
                }

                break;
        }

    }

    public void WalkAnime(bool walk)
    {
        //�^�C�����C���ȂǂŌĂяo�����s�A�j��
        if (walk == true)
        {
            walk_Kyousei = true;
            animator.SetFloat("anime_walk", 1f);
        }
        else
        {
            animator.SetFloat("anime_walk", 0);
            walk_Kyousei = false;
        }

    }

    public void AttackAnime(bool attack)
    {
        //�^�C�����C���ȂǂŌĂяo���U���A�j��
        animator.SetBool("anime_stand_attack", attack);
    }

}
