using UnityEngine;
using UnityEngine.UI;

public class MenuTabControl : MonoBehaviour
{
    public Image [] Tabs;
    public GameObject [] Pages;
    public GameObject QuestsText;

    void Start()
    {
        ActivateTab(0);
    }


   public void ActivateTab(int NumTabs)
    {
        for (int i = 0; i < Pages.Length; i++)
        {
            Debug.Log("Deactivated tabs");
            Pages[i].SetActive(false);
            Tabs[i].color = Color.grey;
            Debug.Log("Deactivated tabs");
        }
        Debug.Log("Activated tabs");
        Pages[NumTabs].SetActive(true);
        //triggers the text to update
        if (NumTabs == 1) 
        {
            QuestsText.SetActive(true);
        }
        else
        {
            QuestsText.SetActive(false);
        }
        Tabs[NumTabs].color = Color.white;
        Debug.Log("Activated tabs");
    }
}
