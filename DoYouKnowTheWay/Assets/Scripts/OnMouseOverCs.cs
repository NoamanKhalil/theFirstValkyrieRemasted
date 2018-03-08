using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
