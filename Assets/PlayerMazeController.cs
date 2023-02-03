using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class PlayerMazeController : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    public VariableJoystick CamvariableJoystick;
    CharacterController characterController;
    public Transform cam;

    public float speed = 10;
    float smoothTime = 0.4f;
    float smoothAngle;
    public FixedTouchField touchPad;
    public  CinemachineFreeLook vcam;
    [SerializeField]
    float horizontalSpeed = 10f;
    [SerializeField]
    float verticalSpeed = 10f;
    [SerializeField]
    private float clampAngle = 80f;
    Vector3 startingRotartion;
    Animator animator;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    
    }

    // Update is called once per frame
    void Update()
    {

       






        float horizontal = variableJoystick.Horizontal;
        float vertical = variableJoystick.Vertical;
        animator.SetFloat("x",horizontal);
        animator.SetFloat("y", vertical);
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        if(direction.magnitude>0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg+cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothAngle, smoothTime);
            transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
            
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDir * speed * Time.deltaTime);
        }

       /* //cam rot
        startingRotartion.x += CamvariableJoystick.Horizontal * horizontalSpeed;
        startingRotartion.y += CamvariableJoystick.Vertical * verticalSpeed;
       //w startingRotartion.y = Mathf.Clamp(startingRotartion.y, -clampAngle, clampAngle);

        vcam.GetComponent<CinemachineFreeLook>().m_XAxis.Value = startingRotartion.x;
        vcam.GetComponent<CinemachineFreeLook>().m_YAxis.Value = startingRotartion.y;
       */

    }
  
}
