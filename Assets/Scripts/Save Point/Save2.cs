using UnityEngine;
using System.Collections;

public class Save2 : MonoBehaviour
{
    public GameObject Player;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player == null)
        {
            Debug.LogError("Player not found in the scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SavePlayerData());
        }
    }

    private IEnumerator SavePlayerData()
    {
        if (Player == null)
        {
            Debug.LogError("Player object is not assigned!");
            yield break; // Dừng coroutine nếu không có Player
        }

        float posX = Player.transform.position.x;
        float posY = Player.transform.position.y;
        float posZ = Player.transform.position.z;

        int idUser = 1; // ID của người chơi, bạn có thể thay đổi theo logic game

        // Lưu vị trí vào server
        yield return StartCoroutine(API.Instance.SavePlayerPosition(idUser, posX, posY, posZ));

        Debug.Log($"Saved Player Position to server: X: {posX}, Y: {posY}, Z: {posZ}");
    }
}