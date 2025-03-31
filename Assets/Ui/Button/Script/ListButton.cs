using System.Collections.Generic;
using UnityEngine;

public class ListButton : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject quitButton;

    [SerializeField] private GameObject listButton;
    
    private bool canContinue = false; 

    private void Start()
    {
        InstanceButtons();
    }

    private void InstanceButtons()
    {
        List<GameObject> buttons = new List<GameObject> { startButton, quitButton };
        if (canContinue)
        {
            buttons.Insert(1, continueButton); 
        }
        
        float buttonWidth = startButton.GetComponent<RectTransform>().rect.width;
        float spacing = 20f;
        float startX = -(buttons.Count - 1) * (buttonWidth + spacing) / 2;
        
        for (int i = 0; i < buttons.Count; i++)
        {
            GameObject btn = Instantiate(buttons[i], listButton.transform);
            RectTransform rectTransform = btn.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(buttonWidth, rectTransform.sizeDelta.y);
            rectTransform.anchoredPosition = new Vector2(startX + i * (buttonWidth + spacing), 0);
        }
    }
}
