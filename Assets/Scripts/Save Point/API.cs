using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class API : MonoBehaviour
{
    private string baseUrl = "http://localhost:3000"; // Địa chỉ API
    public static API Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ API không bị hủy khi chuyển scene
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Phương thức lưu vị trí
    public IEnumerator SavePlayerPosition(int idUser, float x_position, float y_position, float z_position)
    {
        PositionData data = new PositionData { idUser = idUser, x_position = x_position, y_position = y_position, z_position = z_position };
        string jsonData = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest($"{baseUrl}/save-position", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Vị trí người chơi đã được lưu!");
        }
        else
        {
            Debug.LogError("Lỗi khi lưu vị trí: " + request.error);
        }
    }

    // Phương thức lấy vị trí cuối cùng
    public IEnumerator GetLastPlayerPosition(int idUser)
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"{baseUrl}/get-last-position/{idUser}"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                PositionData positionData = JsonUtility.FromJson<PositionData>(request.downloadHandler.text);
                PlayerPrefs.SetFloat("PlayerPosX", positionData.x_position);
                PlayerPrefs.SetFloat("PlayerPosY", positionData.y_position);
                PlayerPrefs.SetFloat("PlayerPosZ", positionData.z_position);
                PlayerPrefs.Save();

                Debug.Log($"Đã đồng bộ vị trí cuối cùng từ server: X: {positionData.x_position}, Y: {positionData.y_position}, Z: {positionData.z_position}");
            }
            else
            {
                Debug.LogError("Lỗi khi lấy vị trí cuối cùng: " + request.error);
            }
        }
    }
}


[System.Serializable]
public class PositionData
{
    public int idUser;
    public float x_position;
    public float y_position;
    public float z_position;
}
[System.Serializable]
public class SavedPositionData
{
    public float x_position;
    public float y_position;
    public float z_position;
}