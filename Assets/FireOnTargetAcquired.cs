using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOnTargetAcquired : MonoBehaviour {

    public GameObject projectile;
    public Transform player;

    public float maxAngle;
    public const float MISSILE_SPEED = 350;
    public float cooldownTime;

    private float cooldown;


    private bool ShouldFire() {
        if (player == null) return false;
        float ang = Vector3.Angle(player.position - transform.position, transform.forward);
        return Mathf.Abs(ang) < maxAngle;
    }

    private void FireMissile()
    {
        AudioSource aud = GetComponent<AudioSource>();
        aud.Play();
        float dist = Vector3.Magnitude(player.position - transform.position);
        Vector3 anticipatedPos = (player.position + (player.forward * dist / 2));
        Vector3 v = (anticipatedPos - transform.position);
        v.Normalize();
        Quaternion q = Quaternion.LookRotation(v) * Quaternion.Euler(Vector3.right * 90);
        var mis = Instantiate(projectile, transform.position + 10 * transform.forward, q);
        mis.GetComponent<Rigidbody>().velocity = v * MISSILE_SPEED;
        Destroy(mis, 10.0f);
    }

	void Update () {
		if (cooldown > 0)
            cooldown -= Time.deltaTime;
        else if (ShouldFire()) {
            FireMissile();
            cooldown = cooldownTime;
        }
	}
}
