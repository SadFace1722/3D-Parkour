using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    Animator _anim;
    void Start()
    {
        _playerController = transform.parent.GetComponent<PlayerController>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnim();
    }
    void PlayerAnim()
    {
        if (_playerController._isGrounded)
        {
            if (_playerController._isMoving)
            {
                _anim.SetInteger("Mode", 1);
            }
            else
            {
                _anim.SetInteger("Mode", 0);
            }
        }
        else
        {
            _anim.SetInteger("Mode", 2);
        }
    }
}
