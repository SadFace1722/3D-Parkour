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
            ShowUI();
        }
    }

    private void ShowUI()
    {
        // Bật UI, hiển thị con trỏ và khóa màn hình
        if (uiImage != null)
        {
            uiImage.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Vô hiệu hóa điều khiển của Player
            // (nếu bạn có script điều khiển player thì nên tắt nó đi)
            Time.timeScale = 0f; // Dừng thời gian trong game
        }
    }

    private void HideUI()
    {
        // Tắt UI và khóa lại con trỏ, mở lại màn hình
        if (uiImage != null)
        {
            uiImage.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f; // Bật lại thời gian trong game
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
            HideUI();
        }
    }
}
