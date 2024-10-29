using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; private set; }
    [SerializeField] private int _health;
    [SerializeField] private DragDoll _dragDoll;
    public bool IsAlive { get; private set; } = true;

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
        if (!IsAlive) return; // Nếu đã chết thì không làm gì thêm
        IsAlive = false;
        _dragDoll.EnableRagdoll();
        Invoke("RestartLevel", 1.5f);
    }

    private void RestartLevel()
    {
        IsAlive = true;
        
        // Kiểm tra xem có save point không, gọi phương thức respawn tương ứng
        bool hasSavedPoint = PlayerPrefs.HasKey("PlayerPosX") &&
                             PlayerPrefs.HasKey("PlayerPosY") &&
                             PlayerPrefs.HasKey("PlayerPosZ");
                             
        PlayerRespawn.Instance.OnPlayerDeath(hasSavedPoint);
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
