using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LastDoor : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private void Start()
    {
        Player = GameObject.Find("Char");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.SetActive(false);
            CutsceneManager.instance.ToggleCutscene(0);
            StartCoroutine(WaitForCutsceneToEnd());
        }
    }

    private IEnumerator WaitForCutsceneToEnd()
    {
        while (!CutsceneManager.instance.IsCutsceneDone())
        {
            yield return null;
        }

        LoadNextScene();
    }

    private void LoadNextScene()
    {
        Player.SetActive(true);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);

    }
}
