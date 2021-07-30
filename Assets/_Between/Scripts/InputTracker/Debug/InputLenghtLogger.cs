using UnityEngine;
using Between.InputTracking;

public class InputLenghtLogger : MonoBehaviour
{
    private void Start()
    {
        InputHandler.EndDraw += LogInputLenght;
    }

    private void LogInputLenght(InputData obj)
    {
        Debug.Log($"[InputLenghtLogger] - Input lenght = {InputLenghtCalculator.LastLenght}");
    }
}
