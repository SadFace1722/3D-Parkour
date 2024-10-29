using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Thêm thư viện cho UI

public class TimeMaze : MonoBehaviour
{
    [SerializeField] private float rayDistance = 1.5f; // Khoảng cách raycast
    [SerializeField] private float maxEscapeTime = 30f; // Thời gian tối đa để thoát (có thể chỉnh sửa trong Inspector)

    private float startTime; // Thời gian bắt đầu
    private float elapsedTime = 0f; // Thời gian đã trôi qua
    private bool isInMaze = false; // Biến kiểm tra có đang ở trong vùng maze không
    PlayerState _state;

    [SerializeField] private Text timeText; // UI Text để hiển thị thời gian đã trôi qua

    void Start()
    {
        _state = GameObject.Find("Player").GetComponent<PlayerState>();
        // Ẩn UI Text ban đầu
        timeText.gameObject.SetActive(false);
    }

    void Update()
    {
        CheckGroundTag();

        // Cập nhật thời gian đã trôi qua chỉ khi đã bắt đầu
        if (isInMaze)
        {
            elapsedTime = Time.time - startTime; // Tính thời gian đã trôi qua
            timeText.text = $"{elapsedTime:F2}"; // Chỉ hiển thị số mà không có chữ

            // Kiểm tra thời gian thoát khỏi mê cung
            if (elapsedTime > maxEscapeTime)
            {
                Die(); // Gọi phương thức chết nếu vượt quá thời gian
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
                // Nếu đứng trong vùng maze và chưa bắt đầu tính thời gian
                if (!isInMaze)
                {
                    StartEscape(); // Bắt đầu tính thời gian
                    timeText.gameObject.SetActive(true); // Hiển thị UI Text
                }
                isInMaze = true; // Đánh dấu là đang ở trong maze
                // Debug.Log($"Player is standing on ground with tag 'Maze'.");
            }
            else
            {
                // Nếu không đứng trên tag "Maze", không làm gì cả
                // Debug.Log("Player is not on ground with tag 'Maze'");
            }
        }
    }

    public void StartEscape()
    {
        startTime = Time.time; // Ghi lại thời gian bắt đầu
        Debug.Log("Started escaping the maze.");
    }

    private void Die()
    {
        Debug.Log("Player has died after failing to escape the maze in time.");
        _state.Death();
        StartCoroutine(ResetLevel());
    }

    IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
