using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SavePlayerData();
        }
    }

    private void SavePlayerData()
    {
        float posX = Player.transform.position.x;
        float posY = Player.transform.position.y;
        float posZ = Player.transform.position.z;

        PlayerPrefs.SetFloat("PlayerPosX", posX);
        PlayerPrefs.SetFloat("PlayerPosY", posY);
        PlayerPrefs.SetFloat("PlayerPosZ", posZ);

        PlayerPrefs.Save();

        Debug.Log($"Saved Player Position: X: {posX}, Y: {posY}, Z: {posZ}");
    }
}
