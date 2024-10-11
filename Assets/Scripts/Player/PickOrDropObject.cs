using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PickOrDropObject : MonoBehaviour
{
    RayChecker _rayChecker;
    public GameObject _currentObject;
    [SerializeField] Rigidbody _rb;
    [SerializeField] BoxCollider _box;
    [SerializeField] GameObject _handPlayer;

    void Start()
    {
        _rayChecker = GameObject.Find("Main Camera").GetComponent<RayChecker>();
        _handPlayer = GameObject.Find("Hand");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_currentObject == null)
            {
                PickObject();
            }
            else
            {
                DropObject();
            }
        }
    }

    void PickObject()
    {
        if (_rayChecker._hitRay.collider != null)
        {
            _currentObject = _rayChecker._hitRay.collider.gameObject;
            _rb = _currentObject.GetComponent<Rigidbody>();
            _box = _currentObject.GetComponent<BoxCollider>();

            // Đảm bảo _currentObject đã có giá trị
            if (_currentObject != null)
            {
                _currentObject.transform.SetParent(_handPlayer.transform);  // Đặt đối tượng vào tay người chơi
                _currentObject.transform.localPosition = Vector3.zero;      // Đặt vị trí cục bộ về (0,0,0) so với tay
                _currentObject.transform.localRotation = Quaternion.Euler(-90f, 0, 0);  // Đặt rotation về (0,0,0)

                if (_rb != null && _box != null)
                {
                    _rb.isKinematic = true;
                    _rb.useGravity = false;
                    _box.isTrigger = true;
                }
            }
        }
    }

    void DropObject()
    {
        if (_currentObject != null)
        {
            _currentObject.transform.SetParent(null);

            if (_rb != null && _box != null)
            {
                _rb.isKinematic = false;
                _rb.useGravity = true;
                _box.isTrigger = false;
            }

            // Reset _currentObject sau khi thả
            _currentObject = null;
        }
    }
}
