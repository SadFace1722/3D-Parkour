using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeMaze : MonoBehaviour
{
    [SerializeField] private float rayDistance = 1.5f; // Khoảng cách raycast
    [SerializeField] private float maxEscapeTime = 30f; // Thời gian tối đa để thoát

    private float remainingTime; // Thời gian còn lại
    private bool isInMaze = false; // Kiểm tra có ở trong vùng maze không
    PlayerState _state;

    [SerializeField] private Text timeText; // UI Text để hiển thị thời gian còn lại

    void Start()
    {
        _state = GameObject.Find("Player").GetComponent<PlayerState>();
        remainingTime = maxEscapeTime; // Khởi tạo thời gian còn lại
        timeText.gameObject.SetActive(false); // Ẩn UI Text ban đầu
    }

    void Update()
    {
        CheckGroundTag();

        // Cập nhật thời gian còn lại
        if (isInMaze)
        {
            remainingTime -= Time.deltaTime; // Giảm thời gian còn lại
            timeText.text = $"{remainingTime:F2}"; // Hiển thị thời gian còn lại

            // Kiểm tra thời gian thoát khỏi mê cung
            if (remainingTime <= 0)
            {
                Die(); // Gọi phương thức chết nếu hết thời gian
            }
        }
    }

    void CheckGroundTag()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance))
        {
            // Kiểm tra tag "Maze"
            if (hit.collider.CompareTag("Maze"))
            {
                // Nếu đứng trong vùng maze
                if (!isInMaze)
                {
                    StartEscape(); // Bắt đầu tính thời gian
                    timeText.gameObject.SetActive(true); // Hiển thị UI Text
                }
                isInMaze = true; // Đánh dấu là đang ở trong maze
            }
        }
    }

    public void StartEscape()
    {
        remainingTime = maxEscapeTime; // Khởi động lại thời gian
        Debug.Log("Started escaping the maze.");
    }

    private void Die()
    {
        Debug.Log("Player has died after failing to escape the maze in time.");
        _state.Death();
    }

    public void AddTime(float amount)
    {
        remainingTime += amount; // Tăng thời gian còn lại
        Debug.Log($"Thời gian đã được tăng thêm: {amount} giây.");
    }
}
