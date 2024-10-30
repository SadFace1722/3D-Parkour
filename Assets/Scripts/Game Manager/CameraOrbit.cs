using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // GameObject mà camera sẽ xoay quanh
    public float distance = 5.0f; // Khoảng cách từ camera tới GameObject
    public float rotationSpeed = 50.0f; // Tốc độ xoay quanh trục Y

    private float currentAngle = 0.0f; // Giá trị góc xoay hiện tại

    private void Start()
    {
        // Đặt camera ở vị trí ban đầu
        currentAngle = transform.eulerAngles.y;
    }

    private void LateUpdate()
    {
        if (target)
        {
            // Tăng giá trị góc xoay theo thời gian để tạo hiệu ứng tự động xoay
            currentAngle += rotationSpeed * Time.deltaTime;

            // Tạo rotation từ góc
            Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
            // Tính toán vị trí camera
            Vector3 position = target.position - rotation * Vector3.forward * distance;

            // Đặt rotation cho camera và vị trí
            transform.rotation = rotation;
            transform.position = position;
        }
    }
}
