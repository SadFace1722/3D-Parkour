using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayChecker : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    [SerializeField] float _distanceRay = 10f;
    [SerializeField] float _distanceNearObject = 5f;
    Ray _ray;
    public RaycastHit _hitRay;
    [SerializeField] Image Crosshair;

    void Start()
    {
        if (Crosshair == null)
        {
            Crosshair = GameObject.Find("Crosshair").GetComponent<Image>();
        }
    }

    void Update()
    {
        ChangeColorCrosshair();
    }

    public bool RayCheck()
    {
        _ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(_ray.origin, _ray.direction * _distanceRay, Color.red);
        return Physics.Raycast(_ray.origin, _ray.direction, out _hitRay, _distanceRay, _layerMask);
    }

    bool NearObject()
    {
        float distanceToHitObject = Vector3.Distance(transform.parent.parent.position, _hitRay.point);
        return distanceToHitObject <= _distanceNearObject;
    }

    public bool CanTouchObject()
    {
        return RayCheck() && NearObject();
    }
    void ChangeColorCrosshair()
    {
        if (CanTouchObject())
        {
            Crosshair.color = Color.red;
        }
        else
        {
            Crosshair.color = Color.white;
        }
    }
}
