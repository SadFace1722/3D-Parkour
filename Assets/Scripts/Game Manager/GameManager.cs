using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private const string PlayerLevelKey = "PlayerLevel"; // Khóa lưu cấp độ người chơi
    private const string PlayerPositionKey = "PlayerPosition"; // Khóa lưu vị trí người chơi
    private const string CurrentSceneKey = "CurrentScene"; // Khóa lưu scene hiện tại

    // Từ điển lưu vị trí mặc định cho mỗi cấp độ
    private Dictionary<int, Vector3> defaultSpawnPositions = new Dictionary<int, Vector3>
    {
        { 0, new Vector3(0, 0, 0) }, // Tutorial
        { 1, new Vector3(47f, 15f, 10.6f) }, // Level1
        { 2, new Vector3(20, 0, 0) }, // Level2
        { 3, new Vector3(30, 0, 0) }, // Level3
        { 4, new Vector3(40, 0, 0) }, // Level4
        { 5, new Vector3(50, 0, 0) }, // Boss
    };

    private void Awake()
    {
        // Kiểm tra xem có nhiều hơn một GameManager không
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ GameManager giữa các scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Lưu cấp độ người chơi
    public void SavePlayerLevel(int level)
    {
        PlayerPrefs.SetInt(PlayerLevelKey, level);
        PlayerPrefs.Save();
    }

    // Tải cấp độ người chơi
    public int LoadPlayerLevel()
    {
        return PlayerPrefs.GetInt(PlayerLevelKey, 0); // Mặc định là cấp độ 0 (Tutorial)
    }

    // Lưu vị trí người chơi
    public void SavePlayerPosition(Vector3 position)
    {
        PlayerPrefs.SetFloat(PlayerPositionKey + "_x", position.x);
        PlayerPrefs.SetFloat(PlayerPositionKey + "_y", position.y);
        PlayerPrefs.SetFloat(PlayerPositionKey + "_z", position.z);
        PlayerPrefs.Save();
    }

    // Tải vị trí người chơi
    public Vector3 LoadPlayerPosition()
    {
        float x = PlayerPrefs.GetFloat(PlayerPositionKey + "_x", 0); // Mặc định là (0, 0, 0)
        float y = PlayerPrefs.GetFloat(PlayerPositionKey + "_y", 0);
        float z = PlayerPrefs.GetFloat(PlayerPositionKey + "_z", 0);
        return new Vector3(x, y, z);
    }

    // Lưu scene hiện tại
    public void SaveCurrentScene(string sceneName)
    {
        PlayerPrefs.SetString(CurrentSceneKey, sceneName);
        PlayerPrefs.Save();
    }

    // Tải scene hiện tại
    public string LoadCurrentScene()
    {
        return PlayerPrefs.GetString(CurrentSceneKey, "Tutorial"); // Mặc định là "Tutorial"
    }

    // Xóa dữ liệu người chơi
    public void DeletePlayerData()
    {
        PlayerPrefs.DeleteAll(); // Xóa tất cả dữ liệu
        PlayerPrefs.Save();
    }

    // Hoàn thành cấp độ và chuyển đến cấp độ tiếp theo
    public void CompleteLevel()
    {
        int currentLevel = LoadPlayerLevel();
        SavePlayerLevel(currentLevel + 1); // Lưu cấp độ tiếp theo
        StartCoroutine(LoadNextLevel(currentLevel + 1));
    }

    // Coroutine để tải cấp độ tiếp theo
    private IEnumerator LoadNextLevel(int nextLevel)
    {
        // Thời gian delay trước khi chuyển cảnh
        yield return new WaitForSeconds(1f); // Thay đổi thời gian delay nếu cần

        string nextScene = GetSceneName(nextLevel);

        // Lưu scene hiện tại trước khi chuyển đi
        SaveCurrentScene(nextScene);

        // Tải scene tiếp theo
        SceneManager.LoadScene(nextScene);

        // Gọi phương thức để đặt vị trí người chơi
        yield return new WaitForSeconds(0.5f); // Đợi một chút để scene load xong
        SetPlayerSpawnPosition(nextLevel);
    }

    // Hàm lấy tên scene dựa vào cấp độ
    private string GetSceneName(int level)
    {
        switch (level)
        {
            case 0: return "Tutorial";
            case 1: return "Level1";
            case 2: return "Level2";
            case 3: return "Level3";
            case 4: return "Level4";
            case 5: return "Boss";
            default: return "Tutorial"; // Trả về Tutorial nếu không xác định
        }
    }

    public void SetPlayerSpawnPosition(int level)
    {
        if (PlayerController._instance != null)
        {
            PlayerController._instance.transform.position = Vector3.zero; // Reset về vị trí (0, 0, 0)
            Debug.Log("Player position reset to (0, 0, 0)");
        }

        // Sử dụng vị trí mặc định cho cấp độ hiện tại
        Vector3 spawnPosition = defaultSpawnPositions[level];
        if (PlayerController._instance != null)
        {
            PlayerController._instance.transform.position = spawnPosition;
        }
    }
    // Hàm để khởi động game
    public void StartGame()
    {
        // Kiểm tra nếu chưa có dữ liệu lưu trữ nào
        if (PlayerPrefs.HasKey(PlayerLevelKey))
        {
            // Nạp scene hiện tại
            string currentScene = LoadCurrentScene();
            SceneManager.LoadScene(currentScene);

            // Gọi để đặt vị trí người chơi sau khi scene được tải
            int playerLevel = LoadPlayerLevel();
            SetPlayerSpawnPosition(playerLevel);
        }
        else
        {
            // Nếu không có dữ liệu, bắt đầu từ scene "Tutorial"
            SceneManager.LoadScene("Tutorial");
        }
    }
}
