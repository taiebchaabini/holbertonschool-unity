using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransition : MonoBehaviour
{
    // Camera on which we will apply our fade
    public GameObject mainCamera;
    // Used for blackscreen transition
    private GameObject image;
    // Animation component and clip used for the fadeinout effect
    private Animation customAnimation;


    // Start is called before the first frame update
    void Start()
    {
       image = transform.Find("Image").gameObject;
       customAnimation = transform.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the UI position and rotation according to camera's values.
        transform.localRotation = mainCamera.transform.rotation;
        transform.position = mainCamera.transform.position;
    }

    // Plays fadeInOut animation
    public void fadeInOut()
    {
        image.GetComponent<Image>().enabled = true;
        customAnimation.enabled = true;
        
        customAnimation.Play();
    }
    
    // Used when the animation ends.
    public void complete()
    {
        image.GetComponent<Image>().enabled = false;
        customAnimation.enabled = false;
        customAnimation.Stop();
    }
}
