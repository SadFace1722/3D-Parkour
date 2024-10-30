using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button startButton;
    public Button settingsButton;
    public Button exitButton;
    public Button clearDataButton;
    public GameObject baseMenu;
    public GameObject settingsMenu;
    public Button backButton;

    private void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
        backButton.onClick.AddListener(OnBackButtonClicked);
        clearDataButton.onClick.AddListener(OnClearDataButtonClicked);

        settingsMenu.SetActive(false);
    }

    private void OnStartButtonClicked()
    {
        LoadPlayerLevel();
    }

    private void OnSettingsButtonClicked()
    {
        ShowSettingsMenu();
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }

    private void OnBackButtonClicked()
    {
        ShowBaseMenu();
    }

    private void OnClearDataButtonClicked()
    {
        GameManager.Instance.DeletePlayerData();
        Debug.Log("Player data cleared.");
        ShowBaseMenu();
    }

    private void LoadPlayerLevel()
    {
        // Tải cấp độ người chơi
        int playerLevel = GameManager.Instance.LoadPlayerLevel();

        // Tải scene tương ứng với cấp độ người chơi
        string sceneName = GetSceneName(playerLevel);
        SceneManager.LoadScene(sceneName);

        // Đặt vị trí cho người chơi
        GameManager.Instance.SetPlayerSpawnPosition(playerLevel); // Gọi phương thức để đặt vị trí
    }



    private string GetSceneName(int level)
    {
        switch (level)
        {
            case 0: return "Tutorial";
            case 1: return "Level1";
            case 2: return "Level2";
            case 3: return "Level3";
            case 4: return "Level4";
            case 5: return "Boss";
            default: return "Tutorial"; // Trả về Tutorial nếu không xác định
        }
    }

    private void ShowSettingsMenu()
    {
        baseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    private void ShowBaseMenu()
    {
        settingsMenu.SetActive(false);
        baseMenu.SetActive(true);
    }
}
