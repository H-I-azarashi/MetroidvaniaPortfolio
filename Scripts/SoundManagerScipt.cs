using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScipt : MonoBehaviour
{
    AudioSource audioSource;
    [Header("���̑�SE")]
    public AudioClip[] sounds = new AudioClip[0];
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
    //�v���C���[�I�[�f�B�I�\�[�X
    GameObject player;
    AudioSource audioSource_player;
    [Header("�J�[�\���ړ�")]
    public AudioClip cursor_sound;
    [Header("����")]
    public AudioClip kettei_sound;
    [Header("�L�����Z��")]
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
