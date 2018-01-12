using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        //GetComponent<Toggle>().onValueChanged.AddListener(OnValueChanged);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnValueChanged(bool change)
    {
        if (change)
            Debug.Log("你被选中了");
        else
            Debug.Log("取消");
    }
}
