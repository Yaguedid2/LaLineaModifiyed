using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public LineRenderer line;
    public static LineManager instance;

    private void Awake()
    {
        instance = this;
    }


    public void drawLine()
    {
      line.SetPosition(1, new Vector3(line.GetPosition(1).x - 20, line.GetPosition(1).y, line.GetPosition(1).z));
      PlayerController.instance.changeLinePosition(false);
    }
}
