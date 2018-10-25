using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    public GameObject GVRHeadset;

    public Dictionary<string, string> defaultKeyboardScheme = new Dictionary<string, string>()
    {
        {"vertical", "w-s"},
        {"horizontal", "a-d"},
        {"fire_1", "e"},
        {"fire_2", "space"},
        {"pause", "tab"},
        {"select", "e"},
        {"back", "q"}
    };

    public Dictionary<string, string> defaultControllerScheme = new Dictionary<string, string>()
    {
        {"vertical", "Vertical"},
        {"horizontal", "Horizontal"},
        {"fire_1", "joystick button 4"},
        {"fire_2", "joystick button 5"},
        {"pause", "joystick button 9"},
        {"select", "joystick button 1"},
        {"back", "joystick button 2"}
    };

    private Vector3 GVRLookVector = new Vector3();
    private Quaternion DevLookDir = new Quaternion();
    private Vector3 half = new Vector3(180f, 180f, 180f);

    private bool controllerConnected;
    private double pollDelta = 1000;
    private double lastPoll = -1000;

    private Dictionary<string, string> controlScheme;

    private void PollControllers()
    {
        lastPoll = Time.deltaTime;
        bool previouslyConnected = controllerConnected;
        if (controllerConnected == previouslyConnected && controlScheme != null) return;
        if (controllerConnected) {
            controlScheme = defaultControllerScheme;
        } else {
            controlScheme = defaultKeyboardScheme;
        }
    }

    private bool IsAxis(string name)
    {
        return !(name.Length == 3 && name.Contains("-"));
    }

    private float GetAxis(string axis)
    {
        float val = 0f;
        if (IsAxis(axis))
            val = Input.GetAxis(axis);
        else
        {
            val += Input.GetKey(""+axis[0]) ? 0f : 1f;
            val += Input.GetKey(""+axis[2]) ? 0f : -1f;
        }
        return val;
    }

    private void ConditionallyCheckControllers()
    {
        if (Time.deltaTime - lastPoll > pollDelta)
            PollControllers();
    }

    public Quaternion GetDeviceLookDir()
    {
        return GyroToUnity(Input.gyro.attitude);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    private static string repr(Vector3 v) {
        return "[" + v.x + ", " + v.y + ", " + v.z + "]";
    }

    private static float clamp(float val, float max) {
        if (val > 0)
            return (val < max)? val : max;
        else
            return (val > -max)? val : -max;
    }

    private static Vector3 clamp(Vector3 v, float val) {
        return new Vector3(clamp(v.x, val), clamp(v.y, val), clamp(v.z, val));
    }

    public Vector3 GetRotationalInput()
    {
        ConditionallyCheckControllers();
        if (Application.platform != RuntimePlatform.WindowsEditor)
        {
            Input.gyro.enabled = true;
            GVRLookVector = GetDeviceLookDir().eulerAngles;
            Debug.Log("STVR: " + repr(GVRLookVector));
            return clamp(GVRLookVector - half / 180f, 1.0f);
        }
        controllerConnected = Input.GetJoystickNames().Length > 0;
        Vector3 pos = new Vector3();
        if (controlScheme != null)
        {
            pos.x = GetAxis(controlScheme["vertical"]);
            pos.y = GetAxis(controlScheme["horizontal"]);
        }
        return pos;
    }
}
