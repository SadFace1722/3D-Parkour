using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerState _state;
    [SerializeField] CharacterController _controller;
    [SerializeField] Camera _cam;
    [SerializeField] RayChecker _ray;
    public bool _isGrounded, _isFalling, _isMoving, _isClimbing, _isHideCursor;

    [SerializeField] float _moveSpeed, _jumpForce, _gravity, _climpSpeed;
    float x, z;
    public Vector3 _Hor, _Ver, _camR, _camF;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        _ray = GameObject.Find("Main Camera").GetComponent<RayChecker>();
        _state = GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (_controller != null && _controller.enabled)
        {
            _isGrounded = _controller.isGrounded;
            _isMoving = _isGrounded && _Hor.sqrMagnitude > 0.1f;
            _isFalling = !_isGrounded && _Ver.sqrMagnitude > 0.1f;

            if (_state._isAlive)
            {
                HandleInput();
                HandleMove();
                HandleGravity();
                HandleClimb();
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
                _Ver.y = -2f;
            }
        }
        else if (!_isClimbing)
        {
            _Ver.y -= _gravity * Time.deltaTime;
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
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    void HandleClimb()
    {
        if (Stair._instance.OnStair && _ray.CanTouchObject())
        {
            _isClimbing = true;
            _Ver.y = 0;

            if (z > 0)
            {
                _Ver.y = _climpSpeed;
            }
            else if (z < 0)
            {
                _Ver.y = -_climpSpeed;
            }
            else
            {
                _Ver.y = 0;
            }
        }
        else
        {
            _isClimbing = false;
        }
    }
}
