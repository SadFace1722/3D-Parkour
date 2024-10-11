using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFlat : MonoBehaviour
{
    [SerializeField] BoxCollider _box, _boxChild;
    [SerializeField] Rigidbody _rigid;
    [SerializeField] float _timeToActiveTrap = 2f;
    Animator _anim;

    void Start()
    {
        _box = GetComponent<BoxCollider>();
        _boxChild = transform.GetChild(0).GetComponent<BoxCollider>();
        _box.isTrigger = true;
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _anim.SetBool("Shake", true);
            Invoke("ActiveTrap", _timeToActiveTrap);
        }
    }

    void ActiveTrap()
    {
        _box.isTrigger = true;
        _boxChild.isTrigger = true;
        _rigid.useGravity = true;
        _rigid.isKinematic = false;
        _anim.SetBool("Shake", false);
    }
}
