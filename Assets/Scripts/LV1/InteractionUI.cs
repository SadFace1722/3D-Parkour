using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject uiImage; // UI Image sẽ bật khi nhấn E
    private bool isInRange = false;

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
        // Kiểm tra nếu người chơi đang trong phạm vi và nhấn E
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleUI();
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
            uiImage.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
