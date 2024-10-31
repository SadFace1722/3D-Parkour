using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
    public Light light1, light2, light3;
    public GameObject exitDoor;

    public void ToggleSwitchA()
    {
        ToggleLight(light1);
        ToggleLight(light2);
    }

    public void ToggleSwitchB()
    {
        ToggleLight(light2);
        ToggleLight(light3);
    }

    public void ToggleSwitchC()
    {
        ToggleLight(light1);
        ToggleLight(light3);
    }

    void ToggleLight(Light light)
    {
        light.enabled = !light.enabled;
    }

    private void Update()
    {
        if (!light1.enabled && !light2.enabled && !light3.enabled)
        {
            OpenExitDoor();
        }
    }

    void OpenExitDoor()
    {
        exitDoor.SetActive(false);
        Debug.Log("All lights are off! Door opened.");
    }
}
