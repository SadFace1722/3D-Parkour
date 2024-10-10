using UnityEngine;
using Cinemachine;
using Unity.Android.Gradle.Manifest;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController _controller;
    [SerializeField] Camera _cam;
    public bool _isGrounded, _isFalling, _isMoving, _isHideCursor, _isAlive;

    [SerializeField] float _moveSpeed, _jumpForce, _gravity;
    float x, z;
    [SerializeField] Vector3 _Hor, _Ver, _camR, _camF;
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        _isAlive = true;
    }
    private void Update()
    {
        _isGrounded = _controller.isGrounded;
        _isMoving = _isGrounded && _Hor.sqrMagnitude > 0.1f;
        _isFalling = _isGrounded && _Ver.sqrMagnitude > 0;
        if (_isAlive)
        {
            HandleInput();
            HandleMove();
            HanldeGraviy();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                HandleJump();
            }
        }
        else
        {
            HanldeGraviy();
        }
    }
    void HandleInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideAndShowCursor();
        }

        _camF = _cam.transform.forward.normalized;
        _camR = _cam.transform.right.normalized;

        _camF.y = 0;
        _camR.y = 0;

        _Hor = _camF * z + _camR * x;
    }
    void HandleMove()
    {
        if (_Hor.sqrMagnitude > 0.1f)
        {
            Quaternion rota = Quaternion.LookRotation(_Hor);
            transform.rotation = Quaternion.Slerp(transform.rotation, rota, _moveSpeed * Time.deltaTime);
        }
        _controller.Move(_Hor * _moveSpeed * Time.deltaTime);
    }
    void HanldeGraviy()
    {
        if (_isGrounded)
        {
            if (_Ver.y < 0)
            {
                _Ver.y = -2f;
            }
        }
        else
        {
            _Ver.y -= _gravity * Time.deltaTime;
        }
        _controller.Move(_Ver * Time.deltaTime);
    }
    public void HandleJump()
    {
        if (_isGrounded && _isAlive)
        {
            _Ver.y = _jumpForce;
        }
    }

    void HideAndShowCursor()
    {
        {
            _isHideCursor = !_isHideCursor;
            if (_isHideCursor)
            {
                // Ẩn con trỏ chuột và khóa nó vào giữa màn hình
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                // Hiện con trỏ chuột và cho phép nó di chuyển
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
