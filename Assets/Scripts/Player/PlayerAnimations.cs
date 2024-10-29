using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    [SerializeField] InteractObject _interactObject;
    [SerializeField] PlayerState _state;
    [SerializeField] RayChecker _ray;
    Animator _anim;
    int _carryIndexAnim, _climbIndexAnim;
    void Start()
    {
        _playerController = transform.parent.GetComponent<PlayerController>();
        _interactObject = transform.parent.GetComponent<InteractObject>();
        _state = transform.parent.GetComponent<PlayerState>();
        _ray = GameObject.Find("Main Camera").GetComponent<RayChecker>();
        _anim = GetComponent<Animator>();
        _carryIndexAnim = _anim.GetLayerIndex("Carry");
        _climbIndexAnim = _anim.GetLayerIndex("Climb");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnim();
        CarryAnim();
        DeathAnim();
        ClimbAnim();
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
            _anim.SetLayerWeight(_carryIndexAnim, 1);
        }
        else
        {
            _anim.SetLayerWeight(_carryIndexAnim, 0);
        }
    }
    void DeathAnim()
    {
        _anim.SetBool("Death", !_state.IsAlive);
    }
    void ClimbAnim()
    {
        if (Stair._instance != null)
        {
            if (Stair._instance.OnStair == true && _ray.CanTouchObject())
            {
                _anim.SetLayerWeight(_climbIndexAnim, 1);

                if (_playerController._Ver.sqrMagnitude > 0.1)
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
                _anim.SetLayerWeight(_climbIndexAnim, 0);
            }
        }
    }
}
