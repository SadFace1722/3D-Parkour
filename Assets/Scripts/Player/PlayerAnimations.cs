using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    [SerializeField] PickOrDropObject _pickOrDropObject;
    Animator _anim;
    int _CarryIndexAnim;
    void Start()
    {
        _playerController = transform.parent.GetComponent<PlayerController>();
        _pickOrDropObject = transform.parent.GetComponent<PickOrDropObject>();
        _anim = GetComponent<Animator>();
        _CarryIndexAnim = _anim.GetLayerIndex("Carry");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnim();
        CarryAnim();
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
        if (_pickOrDropObject._currentObject != null)
        {
            _anim.SetLayerWeight(_CarryIndexAnim, 1);
        }
        else
        {
            _anim.SetLayerWeight(_CarryIndexAnim, 0);
        }
    }
}
