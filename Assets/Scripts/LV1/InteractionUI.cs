using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject uiImage; // UI Image sẽ bật khi nhấn E
    [SerializeField] private Transform door; // Đối tượng cửa sẽ di chuyển
    [SerializeField] private float moveDistance = 5f; // Khoảng cách cửa di chuyển trên trục X
    private bool isInRange = false;
    private bool hasActivated = false; // Đã thực hiện tương tác chưa

    // Hàm khóa tương tác, gọi từ Keypad khi mật khẩu đúng
    public void LockInteraction()
    {
        hasActivated = true;
        Debug.Log("Phím E đã bị vô hiệu hóa");
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
        // Kiểm tra nếu người chơi trong phạm vi và nhấn E, và chưa bị khóa
        if (isInRange && !hasActivated && Input.GetKeyDown(KeyCode.E))
        {
            ToggleUI();
        }
        else if (isInRange && hasActivated && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Phím E đã bị vô hiệu hóa, không thể bật UI");
        }
    }

    private void ToggleUI()
    {
        // Đổi trạng thái UI và con trỏ chuột
        if (uiImage != null)
        {
            bool isActive = uiImage.activeSelf;
            uiImage.SetActive(!isActive);
            Cursor.lockState = isActive ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isActive;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (uiImage.activeSelf)
            {
                ToggleUI();
            }
        }
    }
}
