using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] Slider healthSlider;
    [SerializeField] TMP_Text ammoText;
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
            Instance = this;
    }

    public void SetHealthSlider(float _value)
    {
        Debug.Log(_value);
        healthSlider.value = _value;
    }
    public void SetAmmoText(int clipAmount, int stashAmount)
    {
        ammoText.SetText(clipAmount.ToString() + " / " + stashAmount.ToString());
    }
}
