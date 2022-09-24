using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using E7.Introloop;

public class BGM_ManagerScript : MonoBehaviour
{
    AudioSource audioSource;
    [Header("loop����BGM")]
    public IntroloopAudio introloopAudio;
    [Header("loop�Ȃ�BGM")]
    public AudioClip sounds;
    [Header("�t�F�[�h�C������")]
    public float fedein = 0f;
    [Header("�t�F�[�h�A�E�g����")]
    public float fedeout = 0f;
    [Header("�����Đ�����")]
    public bool startPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (startPlay == true) Play();
    }

    public void Play()
    {
        IntroloopPlayer.Instance.Stop();
        IntroloopPlayer.Instance.Play(introloopAudio, fedein);
    }


    // �|�[�Y����
    public void Pause(float fedeout)
    {
        IntroloopPlayer.Instance.Pause(fedeout);

        // �t�F�[�h�A�E�g���Ȃ���|�[�Y���ł���
        // IntroloopPlayer.Instance.Pause(fadetime);
    }

    // �ĊJ����
    public void Resume(float fedein)
    {
        IntroloopPlayer.Instance.Resume(fedein);

        // �t�F�[�h�C�����Ȃ���ĊJ���ł���
        // IntroloopPlayer.Instance.Resume(fadetime);
    }

    // �w�肵�����ԂɃV�[�N����
    public void Seek(float elapsedTime)
    {
        IntroloopPlayer.Instance.Seek(elapsedTime);
    }

    // ��~����
    public void Stop(float fedeout)
    {
        IntroloopPlayer.Instance.Stop(fedeout);

        // �t�F�[�h�A�E�g���Ȃ����~���ł���
        // IntroloopPlayer.Instance.Stop(fadetime);
    }
}
