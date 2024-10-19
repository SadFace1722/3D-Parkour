using System.Collections;
using UnityEngine;
using UnityEngine.Networking; // Thêm dòng này để sử dụng UnityWebRequest

public class CheckpointManager : MonoBehaviour
{
    public string savePositionUrl = "http://localhost:3000/save-position"; // API URL cho việc lưu vị trí
    private string userId = "603c5f6f5e1b2c0015b2b5e3"; // Thay thế bằng ID người dùng thực tế
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

        string jsonData = JsonUtility.ToJson(positionData); // Chuyển đổi dữ liệu thành JSON

        // Tạo request POST
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
}