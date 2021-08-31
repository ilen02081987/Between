using UnityEngine;
using Between;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;

    private void Awake() => _gameSettings.Init();
}
