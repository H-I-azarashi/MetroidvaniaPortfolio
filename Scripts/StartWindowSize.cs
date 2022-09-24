using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWindowSize : MonoBehaviour
{
    public int ScreenWidth;
    public int ScreenHeight;

    void Awake()
    {
        // PC�����r���h��������T�C�Y�ύX
        if (Application.platform == RuntimePlatform.WindowsPlayer ||
        Application.platform == RuntimePlatform.OSXPlayer ||
        Application.platform == RuntimePlatform.LinuxPlayer)
        {
            Screen.SetResolution(ScreenWidth, ScreenHeight, false);
        }
    }
}
