using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public static PlayerRespawn _instance;
    public string _levelID;
    public GameObject _player;
    public string _cubeTag = "Cube";

    private GameObject[] _cubes;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _cubes = GameObject.FindGameObjectsWithTag("Green");
        _cubes = GameObject.FindGameObjectsWithTag("Red");
        _cubes = GameObject.FindGameObjectsWithTag("Blue");
        Respawn();
    }

    public void Respawn()
    {
        if (PlayerPrefs.HasKey(_levelID + "_PlayerPosX"))
        {
            PlayerState._instance._isAlive = true;
            float playerPosX = PlayerPrefs.GetFloat(_levelID + "_PlayerPosX");
            float playerPosY = PlayerPrefs.GetFloat(_levelID + "_PlayerPosY");
            float playerPosZ = PlayerPrefs.GetFloat(_levelID + "_PlayerPosZ");
            _player.transform.position = new Vector3(playerPosX, playerPosY, playerPosZ);
        }


        if (_cubes != null)
        {

            for (int i = 0; i < _cubes.Length; i++)
            {
                if (PlayerPrefs.HasKey(_levelID + "_Cube" + i + "_PosX"))
                {
                    float cubePosX = PlayerPrefs.GetFloat(_levelID + "_Cube" + i + "_PosX");
                    float cubePosY = PlayerPrefs.GetFloat(_levelID + "_Cube" + i + "_PosY");
                    float cubePosZ = PlayerPrefs.GetFloat(_levelID + "_Cube" + i + "_PosZ");
                    _cubes[i].transform.position = new Vector3(cubePosX, cubePosY, cubePosZ);
                }
            }
        }
    }
}
