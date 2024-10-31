using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _instance;
    [SerializeField] PlayerState _state;
    public CharacterController _controller;
    [SerializeField] Camera _cam;
    [SerializeField] CinemachineFreeLook _freeLook;
    [SerializeField] RayChecker _ray;
    public bool _isGrounded, _isFalling, _isMoving, _isClimbing, _isHideCursor;

    [SerializeField] float _moveSpeed, _jumpForce, _gravity, _climpSpeed;
    float x, z;
    public Vector3 _Hor, _Ver, _camR, _camF;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        _controller = GetComponent<CharacterController>();
        _cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        _ray = GameObject.Find("Main Camera").GetComponent<RayChecker>();
        _freeLook = GameObject.Find("3rd Camera").GetComponent<CinemachineFreeLook>();
        _state = GetComponent<PlayerState>();

        _isHideCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Khi khởi tạo, nạp vị trí người chơi nếu có
        LoadPlayerPosition();
    }

    private void Update()
    {
        if (_controller != null && _controller.enabled)
        {
            _isGrounded = _controller.isGrounded;
            _isMoving = _isGrounded && _Hor.sqrMagnitude > 0.1f;
            _isFalling = !_isGrounded && _Ver.sqrMagnitude > 0.1f;

            if (_state.IsAlive)
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
        if (_isGrounded && _state.IsAlive)
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
            _freeLook.m_XAxis.m_InputAxisName = "Mouse X";
            _freeLook.m_YAxis.m_InputAxisName = "Mouse Y";
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _freeLook.m_XAxis.m_InputAxisValue = 0;
            _freeLook.m_YAxis.m_InputAxisValue = 0;

            _freeLook.m_XAxis.m_InputAxisName = null;
            _freeLook.m_YAxis.m_InputAxisName = null;
        }
    }

    void HandleClimb()
    {
        if (Stair._instance != null)
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

    // Lưu vị trí người chơi
    public void SavePlayerPosition()
    {
        GameManager.Instance.SavePlayerPosition(transform.position);
    }

    // Nạp vị trí người chơi từ GameManager
    public void LoadPlayerPosition()
    {
        transform.position = GameManager.Instance.LoadPlayerPosition();
    }

    // Gọi hàm này khi chạm vào save point
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SavePoint"))
        {
            SavePlayerPosition();
            Debug.Log("Player has saved the position at: " + transform.position);
        }
    }
}
