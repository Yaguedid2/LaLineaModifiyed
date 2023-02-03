using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;
public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IDragHandler
{
    public bool Pressed;
    public float value;
    public bool amHorizontal;
    public CinemachineFreeLook vcam;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Pressed)
        {

            if(amHorizontal)
            vcam.GetComponent<CinemachineFreeLook>().m_XAxis.Value += value;
            else
            vcam.GetComponent<CinemachineFreeLook>().m_YAxis.Value += value;

        }

        
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        Pressed = true;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
      

    }
}