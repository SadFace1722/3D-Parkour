using System.Collections;
using UnityEngine;

public class Flat : MonoBehaviour
{
    Animator _anim;
    ButtonScript _buttonScript;
    [SerializeField] float _timeToActive;  // Thời gian chờ trước khi kích hoạt animation

    void Start()
    {
        _anim = GetComponent<Animator>();
        _buttonScript = GameObject.Find("Button").GetComponent<ButtonScript>();
    }
    private void Update()
    {
        FlatAnim();
    }
    void FlatAnim()
    {
        if (_buttonScript._isActive)
        {
            StartCoroutine(WaitAndActivate());
        }
    }
    IEnumerator WaitAndActivate()
    {
        yield return new WaitForSeconds(_timeToActive);
        _anim.SetBool("Flat", _buttonScript._isActive);
    }
}
