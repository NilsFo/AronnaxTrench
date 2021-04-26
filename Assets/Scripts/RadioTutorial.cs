using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RadioTutorial : Interactible, IPointerDownHandler
{

    public string text = null;
    public float displayTime = 7f;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (text==null)
        {
            print("WARNING! ABOUT TO SEND EMPTY TXT!!");
            return;
        }

        print("Attempting to radio message: "+text);
        FindObjectOfType<RadioManager>().ReuqestRadioMessage(text, displayTime);
    }


    }
