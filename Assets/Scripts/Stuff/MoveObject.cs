using System.Collections;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] float _moveDistance = 5f;
    [SerializeField] float _moveSpeed = 2f;
    [SerializeField] Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }


    public void Move(bool _active)
    {
        Vector3 targetPosition;

        if (_active)
        {
            targetPosition = _startPosition + new Vector3(0, _moveDistance, 0);
        }
        else
        {
            targetPosition = _startPosition;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
    }
}
