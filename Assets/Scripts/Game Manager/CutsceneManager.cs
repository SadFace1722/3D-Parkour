using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager instance;
    public GameObject[] cutscenes;
    private PlayableDirector playableDirector;
    public CinemachineVirtualCamera playerVirtualCamera;
    private int currentCutsceneIndex = 0;

    // Biến để theo dõi trạng thái cutscene
    private bool cutsceneDone = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        playerVirtualCamera = GameObject.Find("3rd Camera").GetComponent<CinemachineVirtualCamera>();
        foreach (var cutscene in cutscenes)
        {
            cutscene.SetActive(false);
        }
    }

    void Update()
    {
        if (currentCutsceneIndex < cutscenes.Length && cutscenes[currentCutsceneIndex].activeSelf && playableDirector != null)
        {
            double currentTime = playableDirector.time;
            if (currentTime >= playableDirector.duration)
            {
                OnCutsceneDone();
            }
        }
    }

    public void ToggleCutscene(int index)
    {
        if (index >= 0 && index < cutscenes.Length)
        {
            foreach (var cutscene in cutscenes)
            {
                cutscene.SetActive(false);
            }

            cutscenes[index].SetActive(true);
            Debug.Log("Cutscene " + index + " activated.");

            if (playerVirtualCamera != null)
            {
                playerVirtualCamera.gameObject.SetActive(false);
            }

            playableDirector = cutscenes[index].GetComponent<PlayableDirector>();
            if (playableDirector != null)
            {
                playableDirector.Play();
            }
        }
    }

    private void OnCutsceneDone()
    {
        Debug.Log("Cutscene " + currentCutsceneIndex + " is done.");

        // Đánh dấu cutscene là đã hoàn thành
        cutsceneDone = true;

        if (playerVirtualCamera != null)
        {
            playerVirtualCamera.gameObject.SetActive(true);
        }

        cutscenes[currentCutsceneIndex].SetActive(false);
        currentCutsceneIndex++;

        // Kiểm tra nếu còn cutscene nào khác
        if (currentCutsceneIndex < cutscenes.Length)
        {
            ToggleCutscene(currentCutsceneIndex);
        }
        else
        {
            // Gọi hàm CompleteLevel từ GameManager khi tất cả cutscene đã hoàn thành
            GameManager.Instance.CompleteLevel(); 
        }
    }

    // Hàm trả về trạng thái cutscene
    public bool IsCutsceneDone()
    {
        return cutsceneDone;
    }
}
