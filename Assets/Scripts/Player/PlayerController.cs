using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerState _state;
    [SerializeField] CharacterController _controller;
    [SerializeField] Camera _cam;
    public bool _isGrounded, _isFalling, _isMoving, _isHideCursor;

    [SerializeField] float _moveSpeed, _jumpForce, _gravity;
    float x, z;
    [SerializeField] Vector3 _Hor, _Ver, _camR, _camF;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        _state = GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (_controller != null && _controller.enabled) // Kiểm tra nếu controller hoạt động
        {
            _isGrounded = _controller.isGrounded;
            _isMoving = _isGrounded && _Hor.sqrMagnitude > 0.1f;
            _isFalling = !_isGrounded && _Ver.sqrMagnitude > 0.1f;

            if (_state._isAlive)
            {
                HandleInput();
                HandleMove();
                HandleGravity(); // Sửa tên từ HanldeGraviy thành đúng là HandleGravity
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    HandleJump();
                }
            }
            else
            {
                HandleGravity();
            }
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
        _Hor = (_camF * z + _camR * x).normalized;
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

    void HandleGravity()
    {
        if (_isGrounded)
        {
            if (_Ver.y < 0)
            {
                _Ver.y = -2f; // Đặt lại tốc độ rơi khi nhân vật tiếp đất
            }
        }
        else
        {
            _Ver.y -= _gravity * Time.deltaTime; // Áp dụng trọng lực
        }
        _controller.Move(_Ver * Time.deltaTime);
    }

    public void HandleJump()
    {
        if (_isGrounded && _state._isAlive)
        {
            _Ver.y = _jumpForce;
        }
    }

    void HideAndShowCursor()
    {
        _isHideCursor = !_isHideCursor;
        if (_isHideCursor)
        {
            Cursor.lockState = CursorLockMode.Locked; // Ẩn và khóa con trỏ
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // Hiện con trỏ
            Cursor.visible = true;
        }
    }
}
