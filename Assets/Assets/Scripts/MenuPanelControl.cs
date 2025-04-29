using UnityEngine;

public class MenuPanelControl : MonoBehaviour
{
    public GameObject MenuPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && MenuPanel.IsActive(False))
        {
            MenuPanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && MenuPanel.IsActive(True))
        {
            MenuPanel.SetActive(false);
        }
    }
}
