using UnityEngine;

public class MenuPanelControl : MonoBehaviour
{
    public GameObject MenuPanel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && MenuPanel.activeSelf == false)
        {
            MenuPanel.gameObject.SetActive(true);
            Debug.Log("MenuPanel is active");
        }
        else if (Input.GetKeyDown(KeyCode.Q) && MenuPanel.activeSelf == true)
        {
            MenuPanel.gameObject.SetActive(false);
            Debug.Log("MenuPanel is inactive");
        }
    }
}
