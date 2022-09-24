using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class EnemyControlGB : MonoBehaviour
{
    //現在HP
    int hp = 0;
    //敵側のオーディオソース
    AudioSource audioSource_enemy;
    //無敵時間
    int muteki_time = 60;
    //カウント
    int count = 0;
    //無敵フラグ
    bool muteki = false;
    //プレイヤー防御力
    int def_player = 0;
    //基礎ダメージ
    int damage_kiso = 0;
    //乱数
    int random_num = 0;
    //総合ダメージ
    int damage_total = 0;
    bool tween_swich = false;
    Tween tween;

    int akuseDef, bouguDef;



    [Header("ヒットマーク")]
    public GameObject hitmark;
    [Header("死亡オブジェクト")]
    public GameObject dead;
    [Header("常時無敵")]
    public bool mutekiMode;
    [Header("エネミーの名前")]
    public string objName;
    [Header("レベル")]
    public float lv = 1;
    [Header("最大HP")]
    public int hp_MAX = 1;
    [Header("攻撃力")]
    public int atk = 1;
    [Header("守備力")]
    public int def = 1;
    [Header("経験値")]
    public int exp = 0;
    [Header("お金")]
    public int gold = 0;
    [Header("ドロップアイテム")]
    public GameObject dropItem;
    [Header("ドロップ率0～100")]
    public int dropProbability = 0;

    //[Header("プレイヤー")]
    //public GameObject player_object;
    GameObject player_object;
    //プレイヤー側のオーディオソース
    AudioSource audioSource;
    //サウンドマネージャー
    GameObject soundmanager;
    //サウンドマネージャースクリプト
    SoundManagerScipt soundManagerScipt;
    //[Header("ゲームマネージャー")]
    //public GameObject gameManager;
    GameObject gameManager;
    //通常タイムライン
    PlayableDirector playable;
    //ダメージタイムライン
    PlayableDirector damageplayable;
    //点滅オブジェクト
    GameObject blinkingObj;
    //public GameObject player_sound;
    //スプライト
    SpriteRenderer spriteRenderer;

    [Header("敵に体当たりした時のダメージ音")]
    public AudioClip player_EnemyBodyHit;

    [Header("使用グローバル変数")]
    public GlobalVariables_ScriptableObject globalVariables;
    [Header("アイテムデータベース")]
    public ItemDataBaseGB itemDataBase;
    [Header("ダメージポップアップ")]
    public GameObject damagePopUp;

    GameObject playerDefence;

    //Player関係のコンポーネントを取得
    PlayerDamageControlGB player;
    PlayerControlGB playerControl;

    MessageMini messageMini;
    // Start is called before the first frame update
    void Start()
    {

        //プレイヤー検索
        player_object = GameObject.FindWithTag("Player");
        //ゲームマネジャー取得
        gameManager= GameObject.FindWithTag("GameManager");
        //サウンドマネージャー取得
        soundmanager= GameObject.FindWithTag("SoundManager");
        soundManagerScipt = soundmanager.GetComponent<SoundManagerScipt>();
        //最大HPを現在HPに代入
        hp = hp_MAX;
        //自身のオーディオソース取得
        audioSource_enemy = GetComponent<AudioSource>();
        //点滅DOTween
        //tween = GetComponent<Tween>();
        tween = DOTween.TweensById("tenmetu")[0];
        player = player_object.GetComponent<PlayerDamageControlGB>();
        playerControl = player_object.GetComponent<PlayerControlGB>();
        //audioSource取得
        audioSource = player_object.GetComponent<AudioSource>();
        //オーディオテスト
        //audioSource.PlayOneShot(player_EnemyBodyHit);
        //メッセージ表示ウィンドウ＆テキスト取得
        messageMini = gameManager.GetComponent<MessageMini>();
        //スプライトレンダラー取得
        spriteRenderer = GetComponent<SpriteRenderer>();
        //タイムライン取得
        playable = GetComponent<PlayableDirector>();
        //点滅オブジェクトとタイムライン取得
        blinkingObj = transform.GetChild(2).gameObject;
        Debug.Log("子オブジェクト" + blinkingObj);
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

        //HP0になったら経験値をプレイヤーに取得させて死亡オブジェクト表示or死亡タイムライン起動
        if(hp <= 0)
        {
            GameObject obj= Instantiate(dead, transform.position, transform.rotation);
            //obj.GetComponent<Transform>().transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            playerControl.Exp(exp);
            Debug.Log("ぶっ倒してexpゲット" + exp);
            Destroy(gameObject);
        }
        //ポーズをかけたら止まる
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
        //オブジェクトに触れたプレイヤーにダメージ
        if (collision.gameObject.tag == "PlayerDefence" & player.muteki == false & globalVariables.obj_controller==true)
        {
            Debug.Log("当たり判定入った");

            //ダメージ計算
            //・プレイヤーが受けるダメージ
            //基礎ダメージ = 攻撃力 - 防御力
            //ダメージ = 基礎ダメージ +（レベル * 0.25の乱数)
            
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
            Debug.Log("プレイヤー "+damage_total + "ダメージ");
            audioSource.PlayOneShot(player_EnemyBodyHit);

            player.Damage(damage_total);
        }

        //プレイヤーの物理攻撃判定に触れたエネミーがダメージ
        if (collision.gameObject.tag == "PlayerAttack" & muteki == false & mutekiMode==false & globalVariables.controller==true)
        {
            
            /*
             * ・敵に与えるダメージ（物理）
             * 基礎ダメージ=攻撃力-防御力
             * ダメージ=基礎ダメージ+(攻撃力+武器レベル/10の乱数)×（レベル/8)
            */
            int atk_total = itemDataBase.buki_atk + globalVariables.str;
            int rnd= (atk_total + itemDataBase.buki_lv) / 10;
            if (rnd == 0) rnd = 1;
            damage_kiso = atk_total - def;
            damage_total = damage_kiso + (Random.Range(0,rnd)) * (globalVariables.lv / 8);
            hp -= damage_total;
            hp = Mathf.Clamp(hp, 0, 9999);
            Debug.Log("敵 " + damage_total + "ダメージ");
            audioSource.PlayOneShot(soundManagerScipt.player_atk_damage);
            if (muteki == false)
            {
                //ヒットマーク
                if (hp > 0) 
                    Instantiate(hitmark, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
                //ダメージポップアップ
                string ddd = damage_total.ToString();
                GameObject pop = Instantiate(damagePopUp, transform.position, transform.rotation);
                pop.GetComponentInChildren<Text>().text = ddd;





                Debug.Log("エネミーダメージ受けて無敵時間入る");
                muteki = true;
            }
            //エネミー情報メッセージを表示
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
        //カメラに表示されると指定タイムラインが再生される
        playable.Play();
        enabled = true;
        //spriteRenderer.enabled = true;
    }

    private void OnBecameInvisible()
    {
        //カメラから離れると指定タイムラインが停止される
        //playable.Pause();
        enabled = false;
        //spriteRenderer.enabled = false;

    }
    public void TimelinePlay()
    {
        //再生
        playable.Play();
    }

    public void TimelinePause()
    {
        //一時停止
        playable.Pause();
    }

    public void TimelineResume()
    {
        //再開
        playable.Resume();
    }

    public void TimelineStop()
    {
        //停止
        playable.Stop();
    }

    public void Damage(int damage)
    {
        //敵にダメージを与える
        Instantiate(hitmark, transform.position, transform.rotation);
        hp -= Mathf.Clamp(damage - def, 0, 999);
        audioSource_enemy.PlayOneShot(soundManagerScipt.player_atk_damage);
        Debug.Log(damage + "ダメージ…敵のHP" + hp);
        if (hp <= 0)
        {
            /*if (timeline_use == false)
            {
                playable.Stop();
            }
            */
            //animator.SetBool("anime_dead", true);
            Debug.Log("敵しんだ" + hp);
            Dead();

        }


    }


    public void Dead()
    {
        Destroy(gameObject);
        //gameObject.SetActive(false);//非表示
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
