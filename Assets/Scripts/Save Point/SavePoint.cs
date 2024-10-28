using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public static SavePoint _instance;
    public string _levelID;
    public GameObject _player;

    private GameObject[] _cubes;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _cubes = GameObject.FindGameObjectsWithTag("Red");
        _cubes = GameObject.FindGameObjectsWithTag("Green");
        _cubes = GameObject.FindGameObjectsWithTag("Blue");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat(_levelID + "_PlayerPosX", _player.transform.position.x);
            PlayerPrefs.SetFloat(_levelID + "_PlayerPosY", _player.transform.position.y);
            PlayerPrefs.SetFloat(_levelID + "_PlayerPosZ", _player.transform.position.z);

            if (_cubes != null)
            {
                for (int i = 0; i < _cubes.Length; i++)
                {
                    PlayerPrefs.SetFloat(_levelID + "_Cube" + i + "_PosX", _cubes[i].transform.position.x);
                    PlayerPrefs.SetFloat(_levelID + "_Cube" + i + "_PosY", _cubes[i].transform.position.y);
                    PlayerPrefs.SetFloat(_levelID + "_Cube" + i + "_PosZ", _cubes[i].transform.position.z);
                }
            }

            PlayerPrefs.Save();
        }
    }
}
