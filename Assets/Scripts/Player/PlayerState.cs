using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; private set; }
    [SerializeField] private int _health;
    [SerializeField] private DragDoll _dragDoll;
    public bool IsAlive { get; private set; } = true;

    // Biến để lưu vị trí respawn
    private Vector3 respawnPosition;

    private void Awake()
    {
        IsAlive = true;
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _health = 100;
        _dragDoll = GetComponent<DragDoll>();

        // Lưu vị trí mặc định (có thể thay đổi tùy theo thiết kế game)
        respawnPosition = new Vector3(0, 1, 0); // Ví dụ: vị trí mặc định là (0, 1, 0)
    }

    private void Update()
    {
        if (_health <= 0)
        {
            Death();
        }
        ActiveRagDoll();
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
        if (_health <= 0)
        {
            _health = 0;
        }
    }

    public void Death()
    {
        if (!IsAlive) return;
        IsAlive = false;
        _dragDoll.EnableRagdoll();
        Invoke("ReSpawn", 1.5f);
    }

    private void ReSpawn()
    {
        IsAlive = true;
        _health = 100;

        // Gọi hàm LoadPlayerPosition từ PlayerController để nạp lại vị trí
        PlayerController._instance.LoadPlayerPosition();
    }


    private void ActiveRagDoll()
    {
        if (IsAlive)
        {
            _dragDoll.DisableRagdoll();
        }
        else
        {
            _dragDoll.EnableRagdoll();
        }
    }
}
