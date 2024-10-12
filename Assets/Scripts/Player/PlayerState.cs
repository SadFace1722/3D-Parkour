using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] int _health;
    public bool _isAlive;
    void Start()
    {
        _health = 100;  
        _isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_health <= 0)
        {
            _isAlive = false;
        }
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
    }
}
