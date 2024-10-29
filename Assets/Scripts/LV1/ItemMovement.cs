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
            // Lưu vật phẩm vào danh sách khi Player chạm vào
            collectedItems.Add(this.gameObject);

            Debug.Log("Player đã thêm vào");

            // Xóa vật phẩm khỏi scene
            Destroy(gameObject);
        }
    }
}
