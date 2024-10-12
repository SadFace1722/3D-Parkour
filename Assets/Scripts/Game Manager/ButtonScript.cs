using System.Collections;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool _isActive;
    [SerializeField] float _time;
    Animator _anim;
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    void Update()
    {
        animButton();
    }

    public void OpenOrClose()
    {
        _isActive = !_isActive;
    }

    void animButton()
    {
        _anim.SetBool("Active", _isActive);
    }
}
