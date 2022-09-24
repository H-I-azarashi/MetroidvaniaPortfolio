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
    public bool forcedToEvent; //強制イベントフラグ（強制アニメモーション用）
    public static PlayerControl instance; //シングルトン用
                                          //グローバル変数の呼び出し宣言
                                          //GlobalVariables gv;
                                          //GameObject obj_gv;
    //強制歩行アニメ用のフラグ
    bool walk_Kyousei = false;

    [Header("歩行速度")]
    public float speed;
    [Header("ジャンプ速度")]
    public float speed_jump;
    [Header("初期HP")]
    public float hp_max = 8f;
    [Header("初期MP")]
    public float mp_max = 30f;
    [Header("HP")]
    public float hp = 8f;
    [Header("MP")]
    public float mp = 30f;
    [Header("攻撃力")]
    public int atk = 3;
    [Header("弾数")]
    public float bullet_total_count = 0;
    [Header("弾")]
    public GameObject bullet;
    [Header("ヒットマーク")]
    public GameObject hitmark;
    [Header("レベルアップポップ")]
    public GameObject levelUpPop;
    public bool lr;
    [Header("現在HPテキスト表示")]
    public Text hp_text;
    [Header("現在MPテキスト表示")]
    public Text mp_text;
    [Header("最大HPテキスト表示")]
    public Text hp_max_text;
    [Header("最大MPテキスト表示")]
    public Text mp_max_text;
    [Header("レベルテキスト表示")]
    public Text lv_text;
    [Header("HPバー表示")]
    public Image hp_Bar;
    [Header("MPバー表示")]
    public Image mp_Bar;
    [Header("経験値バー表示")]
    public Image exp_Bar;
    [Header("魔法装備アイコン")]
    public List<Image> soubiIcon_magic = new List<Image>();
    [Header("防具装備アイコン")]
    public List<Image> soubiIcon_bougu = new List<Image>();
    [Header("アクセサリー装備アイコン")]
    public List<Image> soubiIcon_akuse = new List<Image>();
    [Header("着地")]
    public AudioClip chakuti;
    [Header("ジャンプ音")]
    public AudioClip jumpSE;
    [Header("プレイヤー攻撃音")]
    public AudioClip player_atk_sound;
    [Header("プレイヤー攻撃ヒット音")]
    public AudioClip player_atk_damage;
    [Header("回復魔法")]
    public AudioClip cureSE;
    [Header("炎魔法")]
    public AudioClip fireSE;
    [Header("氷魔法")]
    public AudioClip iceSE;
    [Header("雷魔法")]
    public AudioClip thunderSE;
    [Header("敵に体当たりした時のダメージ音")]
    public AudioClip player_EnemyBodyHit;
    [Header("プレイヤー死亡")]
    public AudioClip player_dead_sound;
    [Header("使用グローバル変数")]
    public GlobalVariables_ScriptableObject globalVariables;
    [Header("アイテムデータベース")]
    public ItemDataBaseGB itemDataBase;
    [Header("使用経験値テーブル")]
    public ExpTableScript expTable;
    [Header("サウンドマネージャー")]
    public SoundManagerScipt soundManager;
    [Header("指定した場所に配置")]
    public bool haiti = false;
    [Header("しゃがみ動作")]
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

    //攻撃判定と防御判定変数
    GameObject atk_col, def_col;
    float atkSX, defSX, atkSY, defSY;

    
    [Header("スタート直後に着地音がしなくなるようの接地フラグ")]
    public bool settiSoundFlag = false;

    private void Awake()
    {

        //指定した場所に配置
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
        //ステータスをUIに反映
        UIdraw();
        Soubi();

        //初期グラフィック
        //animator.SetBool("anime_dress_kenari", true);

        //点滅DOTween
        tween = DOTween.TweensById("tenmetu")[0];

        //canvas内イメージとテキスト取得

        //ジャンプ補正
        speed_jump *= 100;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (forcedToEvent == false & globalVariables.controller==true & walk_Kyousei==false)
        {
            move = Input.GetAxisRaw("Horizontal");
            //Debug.Log("Horizontalの数値 "+move);

            //歩行アニメ（ジャンプ中は不可）
            if (is_jump == false)
            {
                animator.SetFloat("anime_walk", Mathf.Abs(move));
            }
            else
            {
                animator.SetFloat("anime_walk", 0);
            }
            //移動
            rd.AddForce(new Vector2(move * speed, 0));

            //向き
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
        //しゃがみ中フラグ
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
                //しゃがみ
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



        //ジャンプ
        if (Input.GetButtonDown("Jump") & bullet_ok == false & globalVariables.controller == true)
        {
            if (is_jump == false)
            {

                rd.AddForce(Vector2.up * speed_jump);

            }

        }
        animator.SetBool("anime_jump", is_jump);
        //globalVariables.obj_controllerが無効になったら空中で停止する
        if (globalVariables.obj_controller == false)
        {
            rd.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rd.constraints = RigidbodyConstraints2D.None;
            rd.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        //弾発射（ボタン押しっぱなしで連射できるが手動連打すれば沢山撃てる的な）
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


        //立ち剣攻撃
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

        //しゃがみ剣攻撃
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
        //ジャンプ剣攻撃
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
        //forcedToEventがtrueの時の処理諸々
        if (forcedToEvent == true)
        {

        }
        //Debug.Log("キー操作フラグ"+forcedToEvent);


    }
    public void Ground_OnTriggerEnter2D()
    {
        //地面に接地した時の判定
        is_jump = false;
        //forcedToEvent = false;
        animator.SetBool("anime_jump_attack", false);
        animator.SetBool("anime_jump", is_jump);
        Debug.Log("設置した");
        if (settiSoundFlag == true)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(soundManager.chakuti);
        }
        settiSoundFlag = true;
    }

    public void Ground_OnTriggerExit2D()
    {
        //地面から離れた時の判定
        is_jump = true;
        animator.SetFloat("anime_walk", 0);
        Debug.Log("出た");
        audioSource.Stop();
        audioSource.PlayOneShot(soundManager.jumpSE);
    }

    void shoot()
    {
        //弾発射判定 MPが0になったら打てなくする



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
        Debug.Log("今立ち上がってる");

    }

    public void UIdraw()
    {
        //数値反映
        hp_text.text = globalVariables.hp.ToString();
        hp_max_text.text = globalVariables.hp_max.ToString();
        mp_text.text = globalVariables.mp.ToString();
        mp_max_text.text = globalVariables.mp_max.ToString();
        lv_text.text = globalVariables.lv.ToString();

        //バー反映
        hp_Bar.rectTransform.localScale = new Vector3(((float)globalVariables.hp / (float)globalVariables.hp_max) * 1f, 1f, 1f);
        mp_Bar.rectTransform.localScale = new Vector3(((float)globalVariables.mp / (float)globalVariables.mp_max) * 1f, 1f, 1f);
        exp_Bar.rectTransform.localScale = new Vector3(((float)globalVariables.exp / (float)expTable.nextExp[globalVariables.lv]) * 1f, 1f, 1f);
    }

    public void Exp(int aaa)
    {
        levelup = false;
        //経験値取得
        while (true)
        {
            if (aaa >= globalVariables.exp)
            {
                //レベルアップしてもう一度ループ
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
                Debug.Log("ループ中 EXP=" + aaa);
            }
            else
            {
                //経験値テーブルの値に満たないので経験値格納して終了
                globalVariables.exp -= aaa;
                Debug.Log("ループ抜けた EXP=" + aaa);
                break;
            }
        }
        //レベルアップ演出
        if (levelup == true) StartCoroutine(LevelUp());
        //ゲージ・数値反映
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
        Debug.Log("レベルアップ！");
    }
    public void Damage(int damage)
    {
        if (damage_flag == false)
        {
            //操作フラグを切って無敵トリガー起動
            forcedToEvent = true;
            //キャラアニメ切り替え
            animator.SetFloat("anime_walk", 0);
            animator.SetBool("CB_BulletShot_Crouching", false);
            animator.SetBool("CB_BulletShot_stand", false);
            //HPを減らしてUIに反映

            //HPバー反映
            
            //ダメージマーク表示
            pos_hit = transform.position;
            Instantiate(hitmark, pos_hit, transform.rotation);
            //HPが0にならなかったらダメージアクション表示して復帰、0なら死亡アクション
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
                Debug.Log("ダメージ受けた" + hp);
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
        Debug.Log("ダメージ復帰");
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
        Debug.Log("死んだ");
        
        yield return new WaitForSeconds(2f);
        SceneManager.GetActiveScene();
    }

    //各効果音再生
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
        //アイコンリセット
        for (int i = 0; i < soubiIcon_magic.Count; i++) soubiIcon_magic[i].gameObject.SetActive(false);
        for (int i = 0; i < soubiIcon_bougu.Count; i++) soubiIcon_bougu[i].gameObject.SetActive(false);
        for (int i = 0; i < soubiIcon_akuse.Count; i++) soubiIcon_akuse[i].gameObject.SetActive(false);
        //アイコン表示
        if (itemDataBase.soubi_magic>=0) soubiIcon_magic[itemDataBase.soubi_magic].gameObject.SetActive(true);
        if (itemDataBase.soubi_bougu >= 0) soubiIcon_bougu[itemDataBase.soubi_bougu].gameObject.SetActive(true);
        if (itemDataBase.soubi_akuse >= 0) soubiIcon_akuse[itemDataBase.soubi_akuse].gameObject.SetActive(true);

        //アニメーションリセット
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
        //タイムラインなどで呼び出す歩行アニメ
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
        //タイムラインなどで呼び出す攻撃アニメ
        animator.SetBool("anime_stand_attack", attack);
    }

}
