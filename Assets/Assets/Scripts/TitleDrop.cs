using UnityEngine;

public class TitleDrop : MonoBehaviour
{
    public GameObject Canvas;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Canvas.SetActive(false);
        }
    }
}
