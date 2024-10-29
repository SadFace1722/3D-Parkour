using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float amplitude = 0.5f; // Độ cao di chuyển lên xuống
    public float frequency = 1f;   // Tần số di chuyển lên xuống

    [Header("Rotation Settings")]
    public float rotationSpeed = 50f; // Tốc độ xoay của vật phẩm

    private Vector3 startPosition;

    // Danh sách chứa các vật phẩm đã thu thập
    private static List<GameObject> collectedItems = new List<GameObject>();

    // Tham chiếu đến một script quản lý thời gian
    public TimeMaze timeManager; // Đảm bảo rằng đã gán script này trong Inspector

    // Biến theo dõi chỉ số vật phẩm hiện tại
    private static int currentItemIndex = 0; // Khởi tạo từ 0 cho item1

    void Start()
    {
        // Lưu lại vị trí ban đầu
        startPosition = transform.position;
    }

    void Update()
    {
        // Di chuyển vật phẩm lên xuống
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPosition + new Vector3(0, yOffset, 0);

        // Xoay vật phẩm quanh trục Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra nếu đối tượng va chạm có tag là "Player"
        if (other.CompareTag("Player"))
        {
            // Kiểm tra tag của vật phẩm để xác định thứ tự
            if (CompareTag("item" + (currentItemIndex + 1)))
            {
                // Lưu vật phẩm vào danh sách khi Player chạm vào
                collectedItems.Add(this.gameObject);
                Debug.Log("Player đã thêm vào: " + gameObject.name);

                // Tăng thời gian nếu vật phẩm hợp lệ
                AddTimeToPlayer();

                // Tăng chỉ số vật phẩm hiện tại
                currentItemIndex++;

                // Kiểm tra nếu đã thu thập đủ vật phẩm
                if (currentItemIndex >= 3) // Giả sử chỉ có 3 vật phẩm
                {
                    Debug.Log("Đã thu thập đủ vật phẩm!");
                    // Thực hiện hành động nào đó khi thu thập đủ
                    // Bạn có thể mở cửa hoặc chuyển cảnh ở đây
                }

                // Xóa vật phẩm khỏi scene
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Vật phẩm không đúng thứ tự: " + gameObject.name);
            }
        }
    }

    private void AddTimeToPlayer()
    {
        // Gọi phương thức tăng thời gian trong TimeManager
        if (timeManager != null)
        {
            timeManager.AddTime(10f); // Tăng thêm 10 giây (hoặc giá trị bạn muốn)
            Debug.Log("Thời gian đã được tăng thêm.");
        }
        else
        {
            Debug.LogWarning("TimeManager chưa được gán.");
        }
    }
}
