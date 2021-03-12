using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthInfoUI : MonoBehaviour
{
    [SerializeField] TMP_Text HealthText;
    private HealthComponant PlayerHealthComponent;
    private void OnEnable()
    {
        PlayerEvents.OnHealthInitialize += OnHealthInitialize;
    }

    private void OnDisable()
    {
        PlayerEvents.OnHealthInitialize -= OnHealthInitialize;
    }
    private void OnHealthInitialize(HealthComponant obj)
    {
        PlayerHealthComponent = obj;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = PlayerHealthComponent.Health.ToString();
    }
}
