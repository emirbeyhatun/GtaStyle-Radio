using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHandler : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler ,IPointerExitHandler{
    [SerializeField]
    private Channels channel;

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(20, 20, 20, 50);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if (RadioManager.Instance.RadioChannelsSelectUI != null)
        {
            
            RadioManager.Instance.RadioChannelsSelectUI.GetComponent<MouseHandler>().disactiveUI();
            RadioManager.Instance.RadioChannelsSelectUI = gameObject;
        }else if(RadioManager.Instance.RadioChannelsSelectUI == null)
        {
            RadioManager.Instance.RadioChannelsSelectUI = gameObject;
        }
       

        RadioManager.Instance.ChangeRS(channel);
        activeUI();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //disactiveUI();

    }
    
    public void disactiveUI()
    {
       
        Image parent = GetComponent<Image>();
        parent.enabled = false;

        Image child = transform.GetChild(0). GetComponent<Image>();
        Image grandchild = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        Color newColorChild = child.color;
        Color newColorGrandChild = grandchild.color;
        newColorChild.a = 0.3829f;
        newColorGrandChild.a = 0.3829f;
        child.color = newColorChild;
        grandchild.color = newColorGrandChild;
    }
    public void activeUI()
    {



        transform.parent.parent.GetComponent<AudioSource>().Play();
        Image parent = GetComponent<Image>();
        parent.enabled = true;

        Image child = transform.GetChild(0).GetComponent<Image>();
        Image grandchild = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        Color newColorChild = child.color;
        Color newColorGrandChild = grandchild.color;
        newColorChild.a = 1f;
        newColorGrandChild.a = 1f;
        child.color = newColorChild;
        grandchild.color = newColorGrandChild;
    }

}
