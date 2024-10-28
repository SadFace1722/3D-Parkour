using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    [SerializeField] GameManager MenuInGame;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ResetAllData();
        }
    }
    void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All player data has been reset.");
    }
}
