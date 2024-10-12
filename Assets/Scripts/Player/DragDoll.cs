using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDoll : MonoBehaviour
{
    private Rigidbody[] _ragdollRigidbodies;
    private Animator _animator;
    private CharacterController _characterController;
    void Awake()
    {
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _animator = transform.GetChild(1).GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        DisableRagdoll();
    }

    void Update()
    {

    }

    public void DisableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }

        _animator.enabled = true;
        _characterController.enabled = true;
    }

    public void EnableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = false;
        }

        _animator.enabled = false;
        _characterController.enabled = false;
    }
}
