using System;
using UnityEngine;

public class PositionController : MonoBehaviour
{
    // 位置の適用先
    private Transform _transform;

    // 直前のマウスカーソルの座標
    private Vector2 _lastMousePosition;

    // スクリーン座標からワールド座標への変換用
    private Camera _mainCamera;
    
    // 位置初期値
    private Vector3 _originPosition;

    // スケール初期値
    private Vector3 _originScale;


	private void Start()
	{
        _transform = gameObject.transform;
        _lastMousePosition = Vector2.zero;

	    _originPosition = _transform.position;
	    _originScale = _transform.localScale;

        // 注：Cameraに「MainCamera」のタグが設定されていないとnullになる
        _mainCamera = Camera.main;
	}
    
    
    private void Update ()
    {
	    if ((_transform == null) || (_mainCamera == null))
	    {
            // 設定ミス有り
	        return;
	    }

        
        if (Input.GetMouseButton(1) &&
            (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) &&
            (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            // Ctrl + 右クリックでリセット
            _transform.position = _originPosition;
            _transform.localScale = _originScale;

            return;
        }


        if (Input.GetMouseButton(0) &&
            (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
        {
            // Ctrl + 左ドラッグで移動
            MoveObject();
        }
        else if (Input.GetMouseButton(0) &&
            (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            // Alt + 左ドラッグで拡縮
            ScaleObject();
        }


    }


    private void MoveObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // マウスドラッグを開始した場所を記録
            _lastMousePosition = Input.mousePosition;
        }


        // 現在のフレームのマウスカーソルの位置
        var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // ひとつ前のフレームのマウスカーソルの位置
        var lastMousePosition = _mainCamera.ScreenToWorldPoint(_lastMousePosition);


        // モデルの位置に反映
        _transform.position = new Vector3(
            _transform.position.x + (mousePosition.x - lastMousePosition.x),
            _transform.position.y + (mousePosition.y - lastMousePosition.y),
            _transform.position.z
            );


        // 後処理
        _lastMousePosition = Input.mousePosition;
    }


    private void ScaleObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // マウスドラッグを開始した場所を記録
            _lastMousePosition = Input.mousePosition;
        }


        // 現在のフレームのマウスカーソルの位置
        var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition).y;
        // ひとつ前のフレームのマウスカーソルの位置
        var lastMousePosition = _mainCamera.ScreenToWorldPoint(_lastMousePosition).y;


        // モデルの位置に反映
        _transform.localScale = new Vector3(
            _transform.localScale.x + (mousePosition - lastMousePosition),
            _transform.localScale.y + (mousePosition - lastMousePosition),
            _transform.localScale.z
            );


        // 後処理
        _lastMousePosition = Input.mousePosition;
    }


    [Serializable]
    internal struct Transition
    {
        [SerializeField]
        public Vector3 Position;

        [SerializeField]
        public float Scale;
    }


}
