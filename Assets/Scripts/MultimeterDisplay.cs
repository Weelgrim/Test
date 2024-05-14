using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MultimeterDisplay : MonoBehaviour
{
    public TextMeshPro displayText;
    [SerializeField]
    private MultimeterModeSettings resistanceSettings;
    [SerializeField]
    private MultimeterModeSettings currentSettings;
    [SerializeField]
    private MultimeterModeSettings dcVoltageSettings;
    [SerializeField]
    private MultimeterModeSettings acVoltageSettings;

    private const float Resistance = 1000f; // Сопротивление в Омах
    private const float Power = 400f; // Мощность в Ваттах
    
    public MultimeterMode CurrentMode { get; private set; }
    public void UpdateMode(float newRotationZ)
    {
        CurrentMode = DetermineModeFromRotation(newRotationZ);
        var mode = DetermineModeFromRotation(newRotationZ);
        UpdateDisplay(mode);
    }

    private MultimeterMode DetermineModeFromRotation(float newRotationZ)
    {
        if (IsInRange(newRotationZ, resistanceSettings))
        {
            return MultimeterMode.Resistance;
        }
        else if (IsInRange(newRotationZ, currentSettings))
        {
            return MultimeterMode.Current;
        }
        else if (IsInRange(newRotationZ, dcVoltageSettings))
        {
            return MultimeterMode.DcVoltage;
        }
        else if (IsInRange(newRotationZ, acVoltageSettings))
        {
            return MultimeterMode.AcVoltage;
        }
        else
        {
            return MultimeterMode.Neutral;
        }
    }

    private static bool IsInRange(float newRotationZ, MultimeterModeSettings settings)
    {
        var startAngle = settings.startAngle % 360f;
        var endAngle = (settings.startAngle + settings.range) % 360f;
        var normalizedRotation = (newRotationZ + 360f) % 360f;
        return (normalizedRotation >= startAngle && normalizedRotation < endAngle) || (startAngle > endAngle && (normalizedRotation >= startAngle || normalizedRotation < endAngle));
    }

    private void UpdateDisplay(MultimeterMode mode)
    {
        float current = Mathf.Sqrt(Power / Resistance);
        switch (mode)
        {
            case MultimeterMode.Neutral:
                displayText.text = "0";
                break;
            case MultimeterMode.Resistance:
                displayText.text = Resistance.ToString("0.00");
                break;
            case MultimeterMode.Current:
                displayText.text = current.ToString("0.00");
                break;
            case MultimeterMode.DcVoltage:
                var voltageDc = Mathf.Sqrt(Power * Resistance);
                displayText.text = voltageDc.ToString("0.00");
                break;
            case MultimeterMode.AcVoltage:
                displayText.text = "0.01";
                break;
            default:
                displayText.text = "Error";
                break;
        }
    }

    [System.Serializable]
    public class MultimeterModeSettings
    {
        [FormerlySerializedAs("StartAngle")] public float startAngle = 0f;
        [FormerlySerializedAs("Range")] public float range = 0f;
    }

    public enum MultimeterMode
    {
        Neutral,
        Resistance,
        Current,
        DcVoltage,
        AcVoltage
    }
}