using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Thêm namespace để sử dụng UI

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

    // Biến password
    private static string password = ""; // Lưu trữ chuỗi password

    // Tham chiếu tới UI Text để hiển thị password cuối cùng
    public Text passwordText; // Gán đối tượng Text trong Inspector

    void Start()
    {
        // Lưu lại vị trí ban đầu
        startPosition = transform.position;

        // Đảm bảo passwordText trống lúc đầu
        if (passwordText != null)
        {
            passwordText.text = "";
        }
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

                // Thêm số tương ứng với vật phẩm vào password
                UpdatePassword();

                // Tăng thời gian nếu vật phẩm hợp lệ
                AddTimeToPlayer();

                // Tăng chỉ số vật phẩm hiện tại
                currentItemIndex++;

                // Kiểm tra nếu đã thu thập đủ vật phẩm
                if (currentItemIndex >= 3) // Giả sử chỉ có 3 vật phẩm
                {
                    Debug.Log("Đã thu thập đủ vật phẩm!");
                    Debug.Log("Password cuối cùng: " + password); // Chỉ hiện khi đủ 3 vật phẩm

                    // Hiển thị password trên UI Text
                    if (passwordText != null)
                    {
                        passwordText.text = "Password: " + password;
                    }
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

    private void UpdatePassword()
    {
        // Thêm giá trị tương ứng vào password dựa trên tag của vật phẩm
        switch (gameObject.tag)
        {
            case "item1":
                password += "3"; // Thêm "3" vào password
                break;
            case "item2":
                password += "5"; // Thêm "5" vào password
                break;
            case "item3":
                password += "2"; // Thêm "2" vào password
                break;
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
