using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] float delay = 3f;
    [SerializeField] float damageRadius = 20f;
    [SerializeField] float explosionForce = 1200f;

    public int lethalDamage;

    float countdown;

    bool hasExploded = false; 
    public bool hasBeenThrown = false;

    public enum ThrowableType
    {
        None,
        Grenade,
        SmokeGrenade
    }

    public ThrowableType throwableType;

    private void Start()
    {
        countdown = delay;

    }

    private void Update()
    {
        if (hasBeenThrown)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0f && !hasExploded)
            {
                Explode();
                hasExploded = true;
            }
        }
    }

    private void Explode()
    {
        GetThrowableEffect();
        Destroy(gameObject);
    }

    private void GetThrowableEffect()
    {
        print("Exploding");
        switch (throwableType)
        {
            case ThrowableType.Grenade:
            GrenadeEffect();
            break;
            case ThrowableType.SmokeGrenade:
            SmokeGrenadeEffect();
            break;
        }
    }

    private void SmokeGrenadeEffect()
    {
        // Visual Effect
        GameObject smokeEffect = GlobalReferences.Instance.smokeGrenadeEffect;
        Instantiate(smokeEffect, transform.position, transform.rotation);

        // Audio Effect
        SoundManager.Instance.ThrowablesChannel.PlayOneShot(SoundManager.Instance.grenadeSound);

        // Physical Effect
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider objectInRange in colliders)
        {
            Rigidbody rb = objectInRange.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply Blindness to enemies
            }

            // Also apply damage to enemy over here
        }
    }

    private void GrenadeEffect()
    {
        // Visual Effect
        GameObject explosionEffect = GlobalReferences.Instance.grenadeExplosionEffect;
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // Audio Effect
        SoundManager.Instance.ThrowablesChannel.PlayOneShot(SoundManager.Instance.grenadeSound);

        // Physical Effect
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider objectInRange in colliders)
        {
            Rigidbody rb = objectInRange.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, damageRadius);
            }

            if (objectInRange.gameObject.GetComponent<Enemy>())
            {
                objectInRange.gameObject.GetComponent<Enemy>().TakeDamage(lethalDamage);
            }
        }
    }

}
