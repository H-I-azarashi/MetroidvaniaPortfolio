using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using E7.Introloop;

public class BGM_ManagerScript : MonoBehaviour
{
    AudioSource audioSource;
    [Header("loopありBGM")]
    public IntroloopAudio introloopAudio;
    [Header("loopなしBGM")]
    public AudioClip sounds;
    [Header("フェードイン時間")]
    public float fedein = 0f;
    [Header("フェードアウト時間")]
    public float fedeout = 0f;
    [Header("すぐ再生する")]
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


    // ポーズする
    public void Pause(float fedeout)
    {
        IntroloopPlayer.Instance.Pause(fedeout);

        // フェードアウトしながらポーズもできる
        // IntroloopPlayer.Instance.Pause(fadetime);
    }

    // 再開する
    public void Resume(float fedein)
    {
        IntroloopPlayer.Instance.Resume(fedein);

        // フェードインしながら再開もできる
        // IntroloopPlayer.Instance.Resume(fadetime);
    }

    // 指定した時間にシークする
    public void Seek(float elapsedTime)
    {
        IntroloopPlayer.Instance.Seek(elapsedTime);
    }

    // 停止する
    public void Stop(float fedeout)
    {
        IntroloopPlayer.Instance.Stop(fedeout);

        // フェードアウトしながら停止もできる
        // IntroloopPlayer.Instance.Stop(fadetime);
    }
}
