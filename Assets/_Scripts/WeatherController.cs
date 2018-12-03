using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{

    public MarkovChainSystem markovChainSystem;
    public Light light;
    public Camera camera;

    private FrostEffect frostEffect;

    private float currentLightIntensity = 0.5f;

    private void Start()
    {
        frostEffect = camera.GetComponent<FrostEffect>();
    }

    private void Update()
    {
        switch (markovChainSystem.GetCurrentState())
        {
            // SUNNY
            case 0:
                Debug.Log("SUNNY");
                ResetLight();
                ResetFrozenCameraEffect();
                break;
            // CLOUDY
            case 1:
                Debug.Log("CLOUDY");
                DimLight();
                ResetFrozenCameraEffect();
                break;
            // FREEZING
            case 2:
                Debug.Log("FREEZING");
                FrozenCameraEffect();
                break;
        }
    }

    private void DimLight()
    {
        if(light.intensity >= 0)
            light.intensity -= 0.01f;
    }

    private void ResetLight()
    {
        if(light.intensity <= 0.5f)
            light.intensity += 0.01f;
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
