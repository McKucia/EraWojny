using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : NetworkBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider easeHealthSlider;
    [SerializeField] float lerpSpeed = 0.025f;
    [SerializeField] Canvas healthBarCanvas;

    readonly SyncVar<float> health = new SyncVar<float>();

    bool initialized = false;

    public void Init(Camera playerCamera, bool rotate, int initHP)
    {
        healthBarCanvas.worldCamera = playerCamera;
        if (rotate)
            transform.Rotate(0, 180f, 0);

        health.Value = initHP;
        healthSlider.maxValue = initHP;
        easeHealthSlider.maxValue = initHP;
        healthSlider.value = initHP;
        easeHealthSlider.value = initHP;
    }
    
    void Update()
    {
        if (healthSlider.value != health.Value)
            healthSlider.value = health.Value;

        if (healthSlider.value != easeHealthSlider.value && healthSlider.value != 0)
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health.Value, lerpSpeed);
    }

    public void UpdateHP(int hp)
    {
        health.Value = hp;
    }
}
