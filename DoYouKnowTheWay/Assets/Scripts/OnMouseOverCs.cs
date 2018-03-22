using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///OnMouseoverCs used to activate on screen images 

public class OnMouseOverCs :MonoBehaviour
   {

    public GameObject image;

    void OnMouseOver()
    {
        image.SetActive(true);
    }

    void OnMouseExit()
    {
        image.SetActive(false);
    }
}
