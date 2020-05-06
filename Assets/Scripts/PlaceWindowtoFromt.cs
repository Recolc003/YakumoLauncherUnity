
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlaceWindowtoFromt : MonoBehaviour
{
#if !UNITY_EDITOR && PLATFORM_STANDALONE_WIN
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOMOVE = 0x0002;
    const int SWP_FRAMECHANGED = 0x0020;   // SetWindowLongの内容を適用
    const int SWP_NOOWNERZORDER = 0x0200;  // Zorder 変更無効
    const uint TOPMOST_FLAGS = (SWP_NOSIZE | SWP_NOMOVE);

    const int GWL_STYLE = -16;                      // ウィンドウスタイルを書き換えるためのオフセット
    const int WS_BORDER = 0x00800000;               // 境界線を持つウィンドウを作成
    const int WS_DLGFRAME = 0x00400000;             // ダイアログボックスのスタイルの境界を持つウィンドウを作成
    const int WS_CAPTION = WS_BORDER | WS_DLGFRAME; // タイトルバーを持つウィンドウを作成

    [DllImport("user32.dll")]
    private static extern int GetForegroundWindow();

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetWindowPos(int hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint flags);

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
    private static extern uint GetWindowLongPtr(int hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
    private static extern uint SetWindowLongPtr(int hWnd, int nIndex, uint dwNewLong);
#endif

    private void Start()
    {
#if !UNITY_EDITOR && PLATFORM_STANDALONE_WIN
        var handle = GetForegroundWindow();
        //uint style = GetWindowLongPtr(handle, GWL_STYLE); // ウィンドウの情報を取得
        //SetWindowLongPtr(handle, GWL_STYLE, style ^ WS_CAPTION); // ウィンドウの属性を変更
        SetWindowPos(handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS | SWP_FRAMECHANGED | SWP_NOOWNERZORDER);
#endif
    }
}
