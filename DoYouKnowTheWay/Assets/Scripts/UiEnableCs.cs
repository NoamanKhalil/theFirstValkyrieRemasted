using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiEnableCs : MonoBehaviour ,IPointerEnterHandler , IPointerExitHandler{

    public GameObject image;
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.SetActive(true);
        Debug.Log("The cursor entered the selectable UI element.");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        image.SetActive(false);
        Debug.Log("The cursor exited the selectable UI element.");
    }
}
