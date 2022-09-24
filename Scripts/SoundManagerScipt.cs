using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScipt : MonoBehaviour
{
    AudioSource audioSource;
    [Header("その他SE")]
    public AudioClip[] sounds = new AudioClip[0];
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
    //プレイヤーオーディオソース
    GameObject player;
    AudioSource audioSource_player;
    [Header("カーソル移動")]
    public AudioClip cursor_sound;
    [Header("決定")]
    public AudioClip kettei_sound;
    [Header("キャンセル")]
    public AudioClip cancel_sound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        audioSource_player = player.GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

    }

    void Sound(int a)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(sounds[a]);
    }

    void Sound_player(int a)
    {
        audioSource.Stop();
        audioSource_player.PlayOneShot(sounds[a]);
    }

    void Stop()
    {
        audioSource.Stop();
    }

}
