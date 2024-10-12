using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    [SerializeField] InteractObject _interactObject;
    [SerializeField] PlayerState _state;
    Animator _anim;
    int _CarryIndexAnim;
    void Start()
    {
        _playerController = transform.parent.GetComponent<PlayerController>();
        _interactObject = transform.parent.GetComponent<InteractObject>();
        _state = transform.parent.GetComponent<PlayerState>();
        _anim = GetComponent<Animator>();
        _CarryIndexAnim = _anim.GetLayerIndex("Carry");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnim();
        CarryAnim();
        DeathAnim();
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
    void CarryAnim()
    {
        if (_interactObject._currentObject != null)
        {
            _anim.SetLayerWeight(_CarryIndexAnim, 1);
        }
        else
        {
            _anim.SetLayerWeight(_CarryIndexAnim, 0);
        }
    }
    void DeathAnim()
    {
        _anim.SetBool("Death", !_state._isAlive);
    }
}
