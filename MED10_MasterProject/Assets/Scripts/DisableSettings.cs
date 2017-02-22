using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableSettings : MonoBehaviour {

    private Button _disableSettingsButton;

    private void Awake()
    {
        _disableSettingsButton = gameObject.AddComponent<Button>();
        _disableSettingsButton.onClick.AddListener(() => DeactivateSettings());
    }

    private void DeactivateSettings()
    {
        gameObject.SetActive(false);
    }
}
