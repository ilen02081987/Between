using Between.InputTracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLogger : MonoBehaviour
{
    void Start()
    {
        InputHandler.StartDraw += InputHandler_StartDraw;
        InputHandler.DrawCall += InputHandler_DrawCall;
        InputHandler.EndDraw += InputHandler_EndDraw;
        InputHandler.ForceEndDraw += InputHandler_ForceEndDraw;
    }

    private void InputHandler_ForceEndDraw(InputData obj)
    {
        Debug.LogError("Force end draw " + obj.MouseButton);
    }

    private void InputHandler_EndDraw(InputData obj)
    {
        Debug.LogWarning("End draw " + obj.MouseButton);
    }

    private void InputHandler_DrawCall(InputData obj)
    {
        Debug.Log("Draw call " + obj.MouseButton);
    }

    private void InputHandler_StartDraw(InputData obj)
    {
        Debug.LogWarning("Start draw " + obj.MouseButton);
    }
}
