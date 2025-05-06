using UnityEngine;

public class QuitFunction : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
