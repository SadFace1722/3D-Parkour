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
            uiImage.SetActive(false);
            Ans.text = "";

            if (interactionUI != null)
            {
                interactionUI.MoveDoor(); // Gọi hàm MoveDoor khi mật khẩu đúng
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
        if (Ans.text.Length > 0)
        {
            Ans.text = Ans.text.Substring(0, Ans.text.Length - 1);
        }
    }

    private IEnumerator ClearText()
    {
        yield return new WaitForSeconds(1f);
        Ans.text = "";
    }
}
