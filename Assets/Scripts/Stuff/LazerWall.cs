using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LazerWall : MonoBehaviour
{
    PlayerState _state;
    void Start()
    {
        _state = GameObject.Find("Player").GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _state.Death();
            StartCoroutine(ResetLevel());
        }
    }

    IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}