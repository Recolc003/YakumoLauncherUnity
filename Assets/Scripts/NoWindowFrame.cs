using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class NoWindowFrame : MonoBehaviour
{

    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern int FindWindow(String className, String windowName);
    [DllImport("user32.dll")]
    private static extern int GetForegroundWindow();

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
    private static extern uint GetWindowLongPtr(int hWnd, int nIndex);
    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
    private static extern uint SetWindowLongPtr(int hWnd, int nIndex, uint dwNewLong);
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern int SetWindowPos(int hwnd, int hwndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    int handle; // ウィンドウ

    const String WINDOW_NAME = "New Unity Project"; // ウィンドウの名前(とりあえずプロジェクト名をベタ打ち

    const int GWL_STYLE = -16;                      // ウィンドウスタイルを書き換えるためのオフセット
    const int WS_BORDER = 0x00800000;               // 境界線を持つウィンドウを作成
    const int WS_DLGFRAME = 0x00400000;             // ダイアログボックスのスタイルの境界を持つウィンドウを作成
    const int WS_CAPTION = WS_BORDER | WS_DLGFRAME; // タイトルバーを持つウィンドウを作成

    //const int HWND_TOP = 0x0;         // 前面に表示
    const int HWND_TOPMOST = -1;        // 最前面に表示

    const uint SWP_NOSIZE = 0x1;           // 現在のサイズを維持(cxとcyパラメータを無視)
    const uint SWP_NOMOVE = 0x2;           // 現在の位置を維持(xとyパラメータを無視)
    const int SWP_FRAMECHANGED = 0x0020;   // SetWindowLongの内容を適用
    const int SWP_SHOWWINDOW = 0x0040;     // ウィンドウを表示
    const int SWP_NOOWNERZORDER = 0x0200;  // Zorder 変更無効

    void Start()
    {
        //handle = FindWindow(null, WINDOW_NAME); // 指定したウィンドウを取得
        var handle = GetForegroundWindow();
        // タイトルバーを非表示
        uint style = GetWindowLongPtr(handle, GWL_STYLE); // ウィンドウの情報を取得
        SetWindowLongPtr(handle, GWL_STYLE, style ^ WS_CAPTION); // ウィンドウの属性を変更

        // SetWindowLongによる変更を適用
        // 暫定で400×400にリサイズ
        SetWindowPos(handle, HWND_TOPMOST, 0, 0, 400, 400, SWP_NOMOVE | SWP_FRAMECHANGED | SWP_SHOWWINDOW | SWP_NOOWNERZORDER);
    }

}