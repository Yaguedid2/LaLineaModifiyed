using Cinemachine;
using UnityEngine;

public class CinemachinePovExtension : CinemachineExtension
{
    [SerializeField]
    private float clampAngle = 80f;
    [SerializeField]
    float horizontalSpeed=10f;
    [SerializeField]
    float verticalSpeed=10f;
    Vector3 startingRotartion;
    Vector3 rotationOfPlayer = Vector3.zero;
    public VariableJoystick variableJoystick;
    public VariableJoystick variableJoystickPlayer;
    private void Start()
    {
        startingRotartion = new Vector3(90, 0, 0);
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
       if(vcam.Follow)
        {
            
            if(stage==CinemachineCore.Stage.Aim)
            {
                if (startingRotartion == null)
                    startingRotartion = new Vector3(0,90,0);
              
                startingRotartion.x += variableJoystick.Horizontal*verticalSpeed;
                startingRotartion.y -= variableJoystick.Vertical*horizontalSpeed;
                startingRotartion.y = Mathf.Clamp(startingRotartion.y, -clampAngle, clampAngle);
              
                    state.RawOrientation = Quaternion.Euler(startingRotartion.y, startingRotartion.x, 0f);
            }
        }
    }
}
