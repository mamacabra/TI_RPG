using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using TMPro;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] TextMeshProUGUI field;

    [SerializeField] private string busPath;
    private FMOD.Studio.Bus bus;

    // Start is called before the first frame update
    void Start()
    {
        if (busPath != "")
            bus = RuntimeManager.GetBus(busPath);

        bus.getVolume(out float volume);
        slider.value = volume * slider.maxValue;

        UpdateSlider();
    }

    public void UpdateSlider()
    {
        field.text = slider.value.ToString();
        bus.setVolume(slider.value / slider.maxValue);
        /*field.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-15f, 15f));
        float aux = Random.Range(0.5f + slider.value / 250, 0.7f + slider.value / 250);
        field.transform.localScale = new Vector3(aux, aux, aux);*/
    }
}
