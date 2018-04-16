using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using UnityEngine;

public class VirtualJoystickCs : MonoBehaviour , IDragHandler,IPointerUpHandler,IPointerDownHandler
{
    private Image bgImg;
    private Image joystickImg;
    private Vector3 inputVector;

// Use this for initialization
    void Start ()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
	}
	
	// Update is called once per frame
    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
          // to get a value between 0 , 1 
          pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
          pos.y=(pos.y / bgImg.rectTransform.sizeDelta.y);
          // 
          inputVector = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 - 1);
          //inputVector = new Vector3(pos.x * 1f, 0f, pos.y * 1f);
          //ternery operator 
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

          // controls how far the inner stick can move 
            joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 4), inputVector.z*(bgImg.rectTransform.sizeDelta.y/4),0);
        }
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        //throw new System.NotImplementedException();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
      //  throw new System.NotImplementedException();
    }
    public float Horizontal()
    {
        if (inputVector.x != 0)
        {
           // Debug.Log("Works_Horizontal");
            return inputVector.x;
        }
        else
        {
          //  Debug.Log("Works_Horizontal");
            return Input.GetAxis("Horizontal");
        }
  
    }
    public float Vertical()
    {
        if (inputVector.z != 0)
        {
           // Debug.Log("Works_Vertical");
            return inputVector.z;
        }   
        else
        {
           // Debug.Log("Works_Vertical");
            return Input.GetAxis("Vertical");
            
        }
            
    }
}
