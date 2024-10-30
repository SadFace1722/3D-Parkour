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

    public void MoveDoor() // Hàm công khai để gọi khi mật khẩu đúng
    {
        if (door != null)
        {
            door.position += new Vector3(moveDistance, 0, 0);
            hasActivated = true; // Khóa lại phím E
            ToggleUI(false); // Tắt UI
        }
    }

    void Start()
    {
        if (uiImage != null)
            uiImage.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (isInRange && !hasActivated && Input.GetKeyDown(KeyCode.E))
        {
            ToggleUI(true);
        }
    }

    private void ToggleUI(bool state)
    {
        if (uiImage != null)
        {
            uiImage.SetActive(state);
            Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = state;
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
            ToggleUI(false);
        }
    }
}
