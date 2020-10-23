using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
 

public class MenuSFX_Hover : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler{
     // Main Hover sound
     private AudioSource audioDataHover;
      // Main Click sound
     private AudioSource audioDataClick;
    
     void Start(){
          audioDataHover = GameObject.Find("button-rollover").GetComponent<AudioSource>();
          audioDataClick = GameObject.Find("button-click").GetComponent<AudioSource>();
     }


     public void OnPointerEnter(PointerEventData eventData)
     {
         audioDataHover.Pause();
         audioDataHover.Play(0);
     }

     public void OnPointerDown(PointerEventData eventData){
          audioDataClick.Play(0);
     }
}
