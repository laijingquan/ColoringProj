using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderClick : MonoBehaviour {

    public Slider slider;
	// Use this for initialization
	void Start ()
    {
        slider.onValueChanged.AddListener(OnValueChanged);
	}
	
    public void OnValueChanged(float curValue)
    {
        Debug.Log("进度条百分比: " + curValue.ToString());
    }

	// Update is called once per frame
	void Update () {
	
	}
}
