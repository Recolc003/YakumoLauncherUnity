using UnityEngine;
using System;
using System.Globalization;
using System.Runtime.InteropServices;


public class WindowTransparent : MonoBehaviour
{
#if !UNITY_EDITOR && PLATFORM_STANDALONE_WIN
    [DllImport("user32.dll")]
    private static extern int GetForegroundWindow();

    [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
    private static extern int GetWindowLong(int hWnd, int nIndex);
    [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
    private static extern int SetWindowLong(int hWnd, int nIndex, int dwNewLong);
    [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
    private static extern Boolean SetLayeredWindowAttributes(int hwnd, uint crKey, byte bAlpha, uint dwFlags);
    
    const int GWL_EXSTYLE = -20;
    const int WS_EX_LAYERED = 0x80000;
    const int LWA_COLORKEY = 0x1;
#endif


    private void Start()
    {
#if !UNITY_EDITOR && PLATFORM_STANDALONE_WIN
        // 透過する色をMain CameraのBackgroundから取得。
        // ※TagをMainCakeraに設定していない場合は取得できない。
        var backgroundColor = Camera.main.backgroundColor;

        // 透過する色はBGRで指定するので、ColorのRとBの値を入れ替える。
        var swap = new Color(
            backgroundColor.b,
            backgroundColor.g,
            backgroundColor.r,
            0
            );
        var color = uint.Parse(ColorUtility.ToHtmlStringRGB(swap), NumberStyles.AllowHexSpecifier);


        var handle = GetForegroundWindow();

        var extStyle = GetWindowLong(handle, GWL_EXSTYLE);
        SetWindowLong(handle, GWL_EXSTYLE, extStyle | WS_EX_LAYERED);        
        SetLayeredWindowAttributes(handle, color, 0, LWA_COLORKEY);
#endif
    }
}