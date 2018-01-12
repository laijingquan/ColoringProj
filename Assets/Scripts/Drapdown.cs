using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Drapdown : MonoBehaviour {

    public Dropdown dp;
	// Use this for initialization
	void Start ()
    {
        Dropdown.OptionData data1 = new Dropdown.OptionData();
        data1.text = "选项1";
        Dropdown.OptionData data2 = new Dropdown.OptionData();
        data2.text = "选项2";
        Dropdown.OptionData data3 = new Dropdown.OptionData();
        data3.text = "选项3";

        dp.options.Add(data1);
        dp.options.Add(data2);
        dp.options.Add(data3);

        dp.onValueChanged.AddListener(OnOptionsSelected);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnOptionsSelected(int index)
    {
        Debug.Log("当前下标：" + index.ToString() + "当前选项名字：" + dp.captionText.text);
    }
}
