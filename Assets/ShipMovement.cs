using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {

    public InputHandler playerInput;
    public GameObject player;
    public GameObject crosshairClose;
    public GameObject crosshairFar;

    private float rollRate = 0f;
    private float pitchRate = 0f;
    private Vector3 rotation = new Vector3();

    private float roll = 0f;
    private float pitch = 0f;

    private float rollMult = 0.3f;
    private float pitchMult = 0.2f;

    public Vector3 crosshairPosition;
    private float crosshairCloseDistance = 70f;
    private float crosshairFarDistance = 250f;

    private float Smooth(float val, float dest, float smoothing)
    {
        return Mathf.SmoothDamp(val, dest, ref smoothing, 0.05f);
    }

    private void UpdateInputs()
    {
        Vector3 r = playerInput.GetRotationalInput();
        rollRate = r.y;
        pitchRate = r.x;
        rotation.y = r.y;

        roll = Smooth(roll, rollRate, rollMult);
        pitch = Smooth(pitch, pitchRate, pitchMult);
    }

    // Use this for initialization
    void Start ()
    {
        playerInput = gameObject.GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateInputs();
        player.transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(-90f, 0f, 0f));
        player.transform.RotateAround(transform.position, transform.forward, -40f * roll);
        player.transform.RotateAround(transform.position, transform.right, -20f * pitch);
        transform.Rotate(rotation * Time.deltaTime * 70f);
        transform.Translate(new Vector3(0, pitch, 3.0f));
        player.transform.position = transform.position + transform.rotation *
            new Vector3(-roll * 5.0f, - pitch - Mathf.Abs(roll) / 3f, 0);

        crosshairPosition = player.transform.rotation * -Vector3.up;
        crosshairClose.transform.position = (crosshairPosition * crosshairCloseDistance) + player.transform.position;
        crosshairFar.transform.position = (crosshairPosition * crosshairFarDistance) + player.transform.position;
    }
}
