using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour
{
    [SerializeField] private float moveDistance = 5f; // Khoảng cách di chuyển lên/xuống
    [SerializeField] private float speed = 2f; // Tốc độ di chuyển

    private Vector3 startingPosition; // Vị trí bắt đầu của bệ
    private int direction = 1; // Hướng di chuyển (1 = lên, -1 = xuống)

    void Start()
    {
        // Lưu vị trí bắt đầu của bệ
        startingPosition = transform.position;
    }

    void Update()
    {
        // Tính toán vị trí mục tiêu dựa vào vị trí bắt đầu và hướng
        float targetY = startingPosition.y + moveDistance * direction;

        // Di chuyển bệ về vị trí mục tiêu theo trục Y
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), speed * Time.deltaTime);

        // Đổi hướng khi đạt đến điểm cuối cùng
        if (Mathf.Abs(transform.position.y - targetY) < 0.1f)
        {
            direction *= -1; // Đảo chiều
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Kiểm tra nếu vật thể va chạm là player
        if (hit.collider.CompareTag("Player"))
        {
            CharacterController playerController = hit.collider.GetComponent<CharacterController>();

            // Tạo hướng đẩy player theo cùng hướng di chuyển của bệ
            Vector3 pushDirection = new Vector3(0, direction * speed, 0) * Time.deltaTime;

            // Đẩy player bằng cách di chuyển CharacterController
            playerController.Move(pushDirection);
        }
    }
}
