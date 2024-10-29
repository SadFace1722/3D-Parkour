using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MazeEntrance : MonoBehaviour
{
    [SerializeField] private float rayDistance = 1.5f; // Khoảng cách raycast
    [SerializeField] private Text entranceMessage; // UI Text để hiển thị thông báo

    private bool isInMaze = false; // Kiểm tra có ở trong vùng maze không

    void Start()
    {
        // Ẩn UI Text ban đầu
        entranceMessage.gameObject.SetActive(false);
    }

    void Update()
    {
        CheckGroundTag();
    }

    void CheckGroundTag()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance))
        {
            // Kiểm tra tag "Maze"
            if (hit.collider.CompareTag("Maze"))
            {
                if (!isInMaze)
                {
                    isInMaze = true; // Đánh dấu là đang ở trong maze
                    ShowEntranceMessage(); // Hiển thị thông báo khi vào maze
                }
            }
            else
            {
                isInMaze = false; // Đánh dấu là không còn trong maze
            }
        }
    }

    private void ShowEntranceMessage()
    {
        entranceMessage.gameObject.SetActive(true); // Hiện thông báo
        entranceMessage.text = "Bạn đã vào khu vực mê cung!"; // Thay đổi nội dung thông báo
        StartCoroutine(HideMessageAfterDelay(5f)); // Gọi coroutine để ẩn thông báo sau 5 giây
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Chờ trong khoảng thời gian được chỉ định
        entranceMessage.gameObject.SetActive(false); // Ẩn thông báo
    }
}
