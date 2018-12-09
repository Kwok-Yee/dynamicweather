using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{

    public MarkovChainSystem markovChainSystem;
    public Light light;
    public Camera camera;
    public GameObject rainPrefab;

    private FrostEffect frostEffect;

    private float currentLightIntensity = 0.5f;

    private void Start()
    {
        Cursor.visible = false;
        frostEffect = camera.GetComponent<FrostEffect>();
    }

    private void Update()
    {
        switch (markovChainSystem.GetCurrentState())
        {
            // SUNNY
            case 0:
                Light();
                SetRaining(false);
                ResetFrozenCameraEffect();
                break;
            // CLOUDY
            case 1:
                SetRaining(false);
                ResetFrozenCameraEffect();
                DimLight();
                break;
            // RAINING
            case 2:
                DimLight();
                ResetFrozenCameraEffect();
                SetRaining(true);
                break;
            // FREEZING
            case 3:
                DimLight();
                SetRaining(false);
                FrozenCameraEffect();
                break;
        }
    }

    private void DimLight()
    {
        if (light.intensity >= 0)
            light.intensity -= 0.01f;
    }

    private void Light()
    {
        if (light.intensity <= 0.5f)
            light.intensity += 0.01f;
    }

    private void SetRaining(bool state)
    {
        rainPrefab.SetActive(state);
    }

    private void FrozenCameraEffect()
    {
        if (frostEffect.FrostAmount <= 0.4f)
            frostEffect.FrostAmount += 0.01f;
    }

    private void ResetFrozenCameraEffect()
    {
        if (frostEffect.FrostAmount >= 0)
            frostEffect.FrostAmount -= 0.01f;
    }
}
