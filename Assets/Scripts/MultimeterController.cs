using UnityEngine;

public class MultimeterController : MonoBehaviour
{
    public Transform regulatorHandle;
    public Transform regulatorBody;
    public float rotationSpeed;
    public MultimeterDisplay multimeterDisplay;
    private bool _isActive = false;

    private void OnMouseEnter()
    {
        SetRegulatorHighlight(true);
        _isActive = true;
    }

    private void OnMouseExit()
    {
        SetRegulatorHighlight(false);
        _isActive = false;
    }

    private void Update()
    {
        if (!_isActive) return;
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll == 0) return;
        var currentRotation = regulatorHandle.localRotation.eulerAngles;
        var newRotationZ = currentRotation.z + scroll * rotationSpeed;
        SetRegulatorRotation(newRotationZ);
        multimeterDisplay.UpdateMode(newRotationZ);
        Debug.Log(newRotationZ);
    }

    private void SetRegulatorRotation(float newRotation)
    {
        regulatorHandle.localRotation = Quaternion.Euler(0f, 0f, newRotation);
    }

    private void SetRegulatorHighlight(bool highlight)
    {
        regulatorBody.GetComponent<Renderer>().material.color = highlight ? Color.white : Color.black;
    }
}