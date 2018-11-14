using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectOnInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;

    void Start()
    {
        eventSystem.SetSelectedGameObject(selectedObject);
    }

    void Update ()
    {
        if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("joystick button 0"))
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }
	}
}
