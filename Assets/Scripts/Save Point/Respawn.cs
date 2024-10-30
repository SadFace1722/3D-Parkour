using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Networking; // Thêm thư viện này để sử dụng UnityWebRequest

public class Respawn : MonoBehaviour
{
    public static Respawn Instance { get; private set; }
    public GameObject Player;

    // Lưu trữ các vị trí spawn cho từng scene
    private readonly Dictionary<string, Vector3> sceneSpawnPositions = new Dictionary<string, Vector3>
    {
        { "Tutorial", new Vector3(0, 0, 0) },
        { "Level1", new Vector3(36, 13, -61.7f) },
        { "Level2", new Vector3(20, 0, -5) },
        { "Level3", new Vector3(30, 0, 10) },
        { "Level4", new Vector3(40, 0, 15) },
        { "Boss", new Vector3(50, 0, 20) }
    };

    private Vector3 currentSpawnPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Player = GameObject.FindGameObjectWithTag("Player");
            ClearSavedData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(RespawnPlayerAfterDelay(0.1f));
    }

    private IEnumerator RespawnPlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (sceneSpawnPositions.TryGetValue(currentSceneName, out Vector3 spawnPosition))
        {
            currentSpawnPosition = spawnPosition;
            Player.transform.position = currentSpawnPosition;
            Debug.Log($"Respawned at {currentSceneName} default position: {currentSpawnPosition}");
        }
        else
        {
            Debug.LogError($"No spawn position defined for scene: {currentSceneName}");
        }
    }

    public void OnPlayerDeath(bool hasSavedPoint)
    {
        if (hasSavedPoint)
        {
            RespawnAtSavePoint();
        }
        else
        {
            Player.transform.position = currentSpawnPosition;
            Debug.Log($"Respawned at default position after death: {currentSpawnPosition}");
        }
    }

    public void RespawnAtSavePoint()
    {
        // Lấy vị trí từ server thông qua API
        StartCoroutine(GetPlayerPositionFromAPI());
    }

    private IEnumerator GetPlayerPositionFromAPI()
    {
        string url = "http://localhost:3000"; // Địa chỉ API của bạn
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error while getting player position: {webRequest.error}");
                // Hồi sinh tại vị trí mặc định nếu không tìm thấy
                Player.transform.position = currentSpawnPosition;
            }
            else
            {
                // Giả sử API trả về một JSON với các thông tin vị trí
                string jsonResponse = webRequest.downloadHandler.text;
                Vector3 savedPosition = JsonUtility.FromJson<Vector3>(jsonResponse); // Chuyển đổi JSON thành Vector3
                Player.transform.position = savedPosition;

                Debug.Log($"Respawned at saved position from API: X: {savedPosition.x}, Y: {savedPosition.y}, Z: {savedPosition.z}");
            }
        }
    }

    public void SavePlayerPositionToAPI(Vector3 position)
    {
        StartCoroutine(SavePlayerPosition(position));
    }

    private IEnumerator SavePlayerPosition(Vector3 position)
    {
        string url = "http://localhost:3000"; // Địa chỉ API của bạn
        string json = JsonUtility.ToJson(position); // Chuyển đổi vị trí thành JSON

        using (UnityWebRequest webRequest = UnityWebRequest.Put(url, json))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error while saving player position: {webRequest.error}");
            }
            else
            {
                Debug.Log("Player position saved successfully.");
            }
        }
    }

    private void ClearSavedData()
    {
        // Xóa dữ liệu đã lưu ở local nếu cần
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");
        PlayerPrefs.DeleteKey("PlayerPosZ");
        PlayerPrefs.Save();
        Debug.Log("Cleared all saved data for the new scene.");
    }
}