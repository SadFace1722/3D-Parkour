using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigButton : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] MoveObject _moveObject;
    [SerializeField] GameObject _wallLazer;
    [SerializeField] bool _active;
    [SerializeField] String nameTag;

    private HashSet<GameObject> objectsInTrigger = new HashSet<GameObject>();  // Sử dụng HashSet để lưu đối tượng trong trigger

    void Start()
    {
        _anim = transform.GetComponent<Animator>();
        _moveObject = transform.GetChild(0).GetComponent<MoveObject>();
    }

    void Update()
    {
        ActiveBigButton();
        _moveObject.Move(_active);
        if (_wallLazer != null)
        {
            _wallLazer.SetActive(!_active);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsValidTag(other.gameObject))
        {
            objectsInTrigger.Add(other.gameObject);
            _active = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsValidTag(other.gameObject))
        {
            objectsInTrigger.Remove(other.gameObject);
            if (objectsInTrigger.Count == 0)
            {
                _active = false;
            }
        }
    }

    private bool IsValidTag(GameObject obj)
    {
        return obj.CompareTag(nameTag) || obj.CompareTag("Player");
    }

    void ActiveBigButton()
    {
        _anim.SetBool("Active", _active);
    }
}
