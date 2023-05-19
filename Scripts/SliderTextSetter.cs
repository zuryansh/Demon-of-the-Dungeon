using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SliderTextSetter : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI text;

    private void Start()
    {
        slider.value = PlayerPrefs.GetInt("RoomBudget");
    }

    private void Update()
    {
        PlayerPrefs.SetInt("RoomBudget",(int) slider.value);
        text.text = PlayerPrefs.GetInt("RoomBudget").ToString();
        Utilities.instance.roomBudget = PlayerPrefs.GetInt("RoomBudget");
    }
}
