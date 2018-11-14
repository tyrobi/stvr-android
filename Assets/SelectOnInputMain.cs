using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectOnInputMain : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool selectionMade;

    void Update ()
    {
        #if UNITY_EDITOR
        if (Input.GetAxisRaw("Vertical_Editor") != 0 && selectionMade == false)
        #else
        if (Input.GetAxisRaw("Vertical") != 0 && selectionMade == false)
        #endif
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            selectionMade = true;
        }
        if (Input.GetKeyDown("joystick button 1") && selectionMade)
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }
    }

    private void OnDisable()
    {
        selectionMade = false;
    }
}
