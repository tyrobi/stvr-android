using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {

    public GameObject GVRHeadset;

    public GameObject pausePanel;
    public GameObject settingsPanel;
    private bool isPaused = false;

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

    public bool IsFiring()
    {
        return Input.GetAxisRaw("Fire1") >= 0.95f;
    }

    public void CheckPaused()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
        {
            SetPaused(!isPaused);
        }
    }

    public void SetPaused(bool paused)
    {
        pausePanel.gameObject.SetActive(paused);
        settingsPanel.gameObject.SetActive(false);
        Time.timeScale = (paused)? 0f : 1f;
        isPaused = paused;
    }
}
