using UnityEngine;

public class SwitchInteraction : MonoBehaviour
{
    public LightPuzzle lightPuzzle;  // Tham chiếu tới script LightPuzzle
    public string switchType;        // Định dạng công tắc: "A", "B", hoặc "C"

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra nếu người chơi tương tác với công tắc
        if (other.CompareTag("Player"))
        {
            // Gọi hàm thích hợp dựa trên switchType
            if (switchType == "A")
            {
                lightPuzzle.ToggleSwitchA();
            }
            else if (switchType == "B")
            {
                lightPuzzle.ToggleSwitchB();
            }
            else if (switchType == "C")
            {
                lightPuzzle.ToggleSwitchC();
            }
        }
    }
}
