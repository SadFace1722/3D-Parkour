using System.Collections;
using UnityEngine;

public class InteractObject : MonoBehaviour
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

            TouchObject();
        }
    }

    void PickObject()
    {
        // Kiểm tra raycast có trúng object với layer "PickObject" và khoảng cách đủ gần
        if (_rayChecker._hitRay.collider != null &&
            _rayChecker._hitRay.collider.gameObject.layer == LayerMask.NameToLayer("PickObject") &&
            _rayChecker.NearObject())
        {
            _currentObject = _rayChecker._hitRay.collider.gameObject;
            _rb = _currentObject.GetComponent<Rigidbody>();
            _box = _currentObject.GetComponent<BoxCollider>();

            if (_rb != null && _box != null)
            {
                // Đặt object vào tay
                _currentObject.transform.SetParent(_handPlayer.transform);
                _currentObject.transform.localPosition = Vector3.zero;
                _currentObject.transform.localRotation = Quaternion.Euler(-90f, 0, 0);

                // Điều chỉnh trạng thái của Rigidbody và Collider
                _rb.isKinematic = true;
                _rb.useGravity = false;
                _box.isTrigger = true;
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
                // Khôi phục trạng thái của Rigidbody và Collider
                _rb.isKinematic = false;
                _rb.useGravity = true;
                _box.isTrigger = false;
            }

            _currentObject = null;
        }
    }

    void TouchObject()
    {
        // Kiểm tra raycast có trúng object với layer "TouchObject" và khoảng cách đủ gần
        if (_rayChecker._hitRay.collider != null &&
            _rayChecker._hitRay.collider.gameObject.layer == LayerMask.NameToLayer("TouchObject") &&
            _rayChecker.NearObject())
        {
            ButtonScript buttonScript = _rayChecker._hitRay.collider.gameObject.GetComponent<ButtonScript>();
            if (buttonScript != null)
            {
                buttonScript.OpenOrClose();
            }
        }
    }
}
