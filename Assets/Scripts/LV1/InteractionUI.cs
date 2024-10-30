using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject uiImage; // UI Image sẽ bật khi nhấn E
    [SerializeField] private Transform door; // Đối tượng cửa sẽ di chuyển
    [SerializeField] private float moveDistance = 5f; // Khoảng cách cửa di chuyển trên trục X
    [SerializeField] private Transform player; // Tham chiếu đến đối tượng Player

    private bool isInRange = false;
    private bool hasActivated = false; // Đã thực hiện tương tác chưa

    // Hàm khóa tương tác, gọi từ Keypad khi mật khẩu đúng
    public void LockInteraction()
    {
        hasActivated = true;
        Debug.Log("Phím E đã bị vô hiệu hóa");
        MoveDoor(); // Di chuyển cửa khi khóa tương tác
        // Tắt UI nếu đã khóa tương tác
        if (uiImage.activeSelf)
        {
            uiImage.SetActive(false);
        }
    }

    public void MoveDoor() // Hàm để di chuyển cửa
    {
        if (door != null)
        {
            door.position += new Vector3(moveDistance, 0, 0);
            Debug.Log("Cửa đã di chuyển sang phải.");
        }
        else
        {
            Debug.Log("Không tìm thấy đối tượng cửa.");
        }
    }

    void Start()
    {
        // Đảm bảo UI ban đầu bị tắt
        if (uiImage != null)
            uiImage.SetActive(false);

        // Ẩn con trỏ chuột ban đầu
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Kiểm tra nếu người chơi trong phạm vi và chưa bị khóa
        if (isInRange && !hasActivated)
        {
            // Chỉ bật UI khi nhấn E
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleUI();
            }
        }
        else if (isInRange && hasActivated && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Phím E đã bị vô hiệu hóa, không thể bật UI");
        }
    }

    private void ToggleUI()
    {
        // Đổi trạng thái UI chỉ khi đang trong phạm vi
        if (isInRange && uiImage != null)
        {
            bool isActive = uiImage.activeSelf;
            uiImage.SetActive(!isActive);
            Cursor.lockState = isActive ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isActive;
        }
    }

    private void UpdateIsInRange()
    {
        if (player != null)
        {
            // Kiểm tra khoảng cách giữa player và InteractionUI
            float distance = Vector3.Distance(player.position, transform.position);
            bool wasInRange = isInRange; // Lưu trạng thái trước đó

            isInRange = distance < 3f; // Thay đổi giá trị 3f theo yêu cầu của bạn

            // Tắt hoặc bật UI dựa trên trạng thái isInRange
            if (wasInRange && !isInRange && uiImage.activeSelf)
            {
                uiImage.SetActive(false); // Tắt UI nếu Player đi ra ngoài
                Cursor.lockState = CursorLockMode.Locked; // Khóa con trỏ chuột
                Cursor.visible = false; // Ẩn con trỏ chuột
            }
        }
    }

    void FixedUpdate()
    {
        UpdateIsInRange(); // Cập nhật trạng thái isInRange
    }
}
