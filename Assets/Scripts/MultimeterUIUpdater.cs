using TMPro;
using UnityEngine;

public class MultimeterUIUpdater : MonoBehaviour
{
    public MultimeterDisplay multimeterDisplay;

    public TextMeshProUGUI resistanceText;
    public TextMeshProUGUI currentText;
    public TextMeshProUGUI dcVoltageText;
    public TextMeshProUGUI acVoltageText;

    private void Update()
    {
        var currentMode = multimeterDisplay.CurrentMode;
        UpdateUIText(currentMode);
    }

    private void UpdateUIText(MultimeterDisplay.MultimeterMode currentMode)
    {
        ClearUIText();
        
        switch (currentMode)
        {
            case MultimeterDisplay.MultimeterMode.Resistance:
                resistanceText.SetText(multimeterDisplay.displayText.text);
                break;
            case MultimeterDisplay.MultimeterMode.Current:
                currentText.SetText(multimeterDisplay.displayText.text);
                break;
            case MultimeterDisplay.MultimeterMode.DcVoltage:
                dcVoltageText.SetText(multimeterDisplay.displayText.text);
                break;
            case MultimeterDisplay.MultimeterMode.AcVoltage:
                acVoltageText.SetText(multimeterDisplay.displayText.text);
                break;
        }
    }

    private void ClearUIText()
    {
        resistanceText.SetText("0");
        currentText.SetText("0");
        dcVoltageText.SetText("0");
        acVoltageText.SetText("0");
    }
}