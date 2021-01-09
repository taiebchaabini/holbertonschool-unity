using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    // GameObject which displays textbox informations (image and text)
    public GameObject textBox;
    // Button used for the current TextBox
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = textBox.transform.parent.GetComponent<Button>();
    }

 

    // Enables or disables the GameObject depending on his current state.
    public void toggleEnable()
    {
        button.interactable = false;

        StartCoroutine(DelayButton(button, 0.5f));

        if (textBox.activeInHierarchy)
        {
            textBox.SetActive(false);
        }
        else
        {
            textBox.SetActive(true);
        }
    }

    IEnumerator DelayButton(Button button, float seconds) {
        yield return new WaitForSeconds(seconds);
        button.interactable = true;
    }
}
