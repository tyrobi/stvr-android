using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    public GameObject GVRHeadset;

    private Vector3 GVRLookVector = new Vector3();
    private Vector3 half = new Vector3(180f, 180f, 180f);

    private float GetAxis(string axis)
    {
        float val = 0f;
        val = Input.GetAxis(axis);
        return val;
    }

    public Quaternion GetDeviceLookDir()
    {
        return GyroToUnity(Input.gyro.attitude);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    private static string Repr(Vector3 v) {
        return "[" + v.x + ", " + v.y + ", " + v.z + "]";
    }

    private static float Clamp(float val, float max) {
        if (val > 0)
            return (val < max)? val : max;
        else
            return (val > -max)? val : -max;
    }

    private static Vector3 Clamp(Vector3 v, float val) {
        return new Vector3(Clamp(v.x, val), Clamp(v.y, val), Clamp(v.z, val));
    }

    public Vector3 GetRotationalInput()
    {
        Vector3 pos = new Vector3();
        pos.x = Input.GetAxis("Vertical");
        pos.y = Input.GetAxis("Horizontal");

        return pos;
    }
}
