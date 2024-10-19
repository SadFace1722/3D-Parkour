using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Checkpoint : MonoBehaviour
{
    public string savePositionUrl = "http://localhost:3000/save-position"; // API URL cho việc lưu vị trí
    private string userId = ""; // Thay thế bằng ID người dùng thực tế
    private Vector3 lastCheckpointPosition;

    // Hàm được gọi khi người chơi đến checkpoint
    public void ReachCheckpoint(Vector3 checkpointPosition)
    {
        // Kiểm tra nếu người chơi đã đến checkpoint mới
        if (lastCheckpointPosition != checkpointPosition)
        {
            // Lưu vị trí mới
            StartCoroutine(SavePosition(checkpointPosition));
            lastCheckpointPosition = checkpointPosition; // Cập nhật vị trí checkpoint cuối cùng
        }
    }

    // Coroutine gửi yêu cầu POST để lưu vị trí người chơi
    IEnumerator SavePosition(Vector3 position)
    {
        // Dữ liệu để gửi lên server
        PositionData positionData = new PositionData();
        positionData.userId = userId;
        positionData.x_position = position.x;
        positionData.y_position = position.y;
        positionData.z_position = position.z;

        string jsonData = JsonUtility.ToJson(positionData);

        using (UnityWebRequest www = new UnityWebRequest(savePositionUrl, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json"); // Đặt header

            yield return www.SendWebRequest();

            // Kiểm tra kết quả
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Position saved successfully: " + jsonData);
            }
            else
            {
                Debug.LogError("Failed to save position: " + www.error);
            }
        }
    }
}

// Class để lưu dữ liệu vị trí
[System.Serializable]
public class PositionData
{
    public string userId;
    public float x_position;
    public float y_position;
    public float z_position;
}