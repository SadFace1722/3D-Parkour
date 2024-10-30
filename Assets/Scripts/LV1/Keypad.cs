using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    [SerializeField] private Text Ans;
    [SerializeField] private GameObject uiImage; // UI sẽ tắt khi nhập đúng
    [SerializeField] private InteractionUI interactionUI; // Tham chiếu đến InteractionUI

    private string Answer = "352";

    public void Number(int number)
    {
        Ans.text += number.ToString();
    }

    public void Execute()
    {
        if (Ans.text == Answer)
        {
            // Nếu đúng mã, tắt UI
            uiImage.SetActive(false);
            Ans.text = ""; // Xóa nội dung sau khi hoàn tất

            // Gọi hàm để khóa phím E trong InteractionUI
            if (interactionUI != null)
            {
                interactionUI.LockInteraction(); // Khóa phím E sau khi nhập đúng
                interactionUI.MoveDoor(); // Di chuyển cửa sau khi khóa phím E
                Debug.Log("Mật khẩu đúng. UI đã tắt, phím E bị vô hiệu hóa và cửa đã di chuyển.");
            }
        }
        else
        {
            Ans.text = "INCORRECT";
            StartCoroutine(ClearText());
        }
    }

    public void DeleteLast()
    {
        // Xóa ký tự cuối cùng trong Ans.text
        if (Ans.text.Length > 0)
        {
            Ans.text = Ans.text.Substring(0, Ans.text.Length - 1);
        }
    }

    private IEnumerator ClearText()
    {
        // Đợi một thời gian ngắn trước khi xóa mã vừa nhập
        yield return new WaitForSeconds(1f);
        Ans.text = "";
    }
}
