using Between.UserInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SpellPainter : MonoBehaviour
{
    private readonly float _drawDelay = .01f;

    private LineRenderer _lineRenderer;
    private int _mouseButton;
    private bool _isDraw = true;

    public void Init(int mouseButton)
    {
        _mouseButton = mouseButton;
        _lineRenderer = GetComponent<LineRenderer>();

        MouseInput.OnFinishDraw += MouseInput_OnFinishDraw;

        _lineRenderer.positionCount = 0;

        StartCoroutine(Draw());
    }

    private IEnumerator Draw()
    {
        yield return new WaitForSeconds(_drawDelay);

        while (_isDraw && this != null)
        {
            SetLinePoint();
            yield return new WaitForSeconds(_drawDelay);
        }
    }
    
    private void MouseInput_OnFinishDraw(int mouseButton, Vector3 point)
    {
        if (_mouseButton == mouseButton)
        {
            _isDraw = false;
            MouseInput.OnFinishDraw -= MouseInput_OnFinishDraw;
            StopCoroutine(Draw());

            Destroy(gameObject);
        }
    }

    private void SetLinePoint()
    {
        _lineRenderer.positionCount++;
        var lastPointIndex = _lineRenderer.positionCount - 1;

        SetLinePoint(lastPointIndex, Input.mousePosition);
    }

    private void SetLinePoint(int index, Vector3 screenPoint)
    {
        var worldPoint = GameCamera.ScreenToWorldPoint(screenPoint);
        _lineRenderer.SetPosition(index, worldPoint);
    }
}
