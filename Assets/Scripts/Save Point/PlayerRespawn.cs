using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerRespawn : MonoBehaviour
{
    public static PlayerRespawn Instance { get; private set; }
    public GameObject Player;

    // Lưu trữ các vị trí spawn cho từng scene
    private readonly Dictionary<string, Vector3> sceneSpawnPositions = new Dictionary<string, Vector3>
    {
        { "Tutorial", new Vector3(0, 0, 0) },  // Thay đổi x, y, z cho scene này
        { "Level1", new Vector3(36, 13, -61.7f) },    // Thay đổi x, y, z cho scene này
        { "Level2", new Vector3(20, 0, -5) },   // Thay đổi x, y, z cho scene này
        { "Level3", new Vector3(30, 0, 10) },   // Thay đổi x, y, z cho scene này
        { "Level4", new Vector3(40, 0, 15) },   // Thay đổi x, y, z cho scene này
        { "Boss", new Vector3(50, 0, 20) }      // Thay đổi x, y, z cho scene này
    };

    private Vector3 currentSpawnPosition; // Lưu vị trí spawn hiện tại

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Player = GameObject.FindGameObjectWithTag("Player");
            ClearSavedData(); // Xóa dữ liệu khi vào scene mới
        }
    }

    private void Start()
    {
        StartCoroutine(RespawnPlayerAfterDelay(0.1f)); // Gọi coroutine để spawn sau 0.1 giây
    }

    private IEnumerator RespawnPlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Chờ 0.1 giây
        RespawnPlayer(); // Gọi phương thức respawn
    }

    public void RespawnPlayer()
    {
        // Lấy tên scene hiện tại
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Kiểm tra xem có vị trí spawn cho scene không
        if (sceneSpawnPositions.TryGetValue(currentSceneName, out Vector3 spawnPosition))
        {
            currentSpawnPosition = spawnPosition; // Lưu vị trí spawn
            Player.transform.position = currentSpawnPosition; // Spawn tại vị trí đã chỉ định
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
            // Hồi sinh tại vị trí lưu nếu đã chạm vào save point
            RespawnAtSavePoint();
        }
        else
        {
            // Hồi sinh tại vị trí default đã setup cho scene
            Player.transform.position = currentSpawnPosition; // Hồi sinh tại vị trí mặc định
            Debug.Log($"Respawned at default position after death: {currentSpawnPosition}");
        }
    }

    public void RespawnAtSavePoint()
    {
        // Lấy vị trí từ PlayerPrefs nếu đã được lưu
        if (PlayerPrefs.HasKey("PlayerPosX") &&
            PlayerPrefs.HasKey("PlayerPosY") &&
            PlayerPrefs.HasKey("PlayerPosZ"))
        {
            float playerPosX = PlayerPrefs.GetFloat("PlayerPosX");
            float playerPosY = PlayerPrefs.GetFloat("PlayerPosY");
            float playerPosZ = PlayerPrefs.GetFloat("PlayerPosZ");
            Player.transform.position = new Vector3(playerPosX, playerPosY, playerPosZ);

            Debug.Log($"Respawned at saved position: X: {playerPosX}, Y: {playerPosY}, Z: {playerPosZ}");
        }
        else
        {
            Debug.LogError("No saved position found for respawn.");
            Player.transform.position = currentSpawnPosition; // Hồi sinh tại vị trí mặc định nếu không tìm thấy
        }
    }

    private void ClearSavedData()
    {
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");
        PlayerPrefs.DeleteKey("PlayerPosZ");
        PlayerPrefs.Save();
        Debug.Log("Cleared all saved data for the new scene.");
    }
}
