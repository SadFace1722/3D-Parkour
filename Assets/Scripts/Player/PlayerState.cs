using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState _instance;
    [SerializeField] int _health;

    [SerializeField] DragDoll _dragDoll;
    public bool _isAlive;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
    void Start()
    {
        _health = 100;
        _isAlive = true;
        _dragDoll = GetComponent<DragDoll>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_health <= 0)
        {
            _isAlive = false;
        }
        ActiveRagDoll();
    }
    public void TakeDamge(int _amount)
    {
        _health -= _amount;
        if (_health <= 0)
        {
            _health = 0;
        }
    }
    public void Death()
    {
        _isAlive = false;
        _dragDoll.EnableRagdoll();
        Invoke("RestartLevel", 3f);
    }
    void RestartLevel()
    {
        _isAlive = true;
        PlayerRespawn._instance.Respawn();
    }
    void ActiveRagDoll()
    {
        if (_isAlive)
        {
            _dragDoll.DisableRagdoll();
        }
        else if (!_isAlive)
        {
            _dragDoll.EnableRagdoll();
        }
    }
}
