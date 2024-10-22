using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;
    private void OnCollisionEnter(Collision objectWeHit)
    {
        if (objectWeHit.gameObject.CompareTag("Target"))
        {
            print("Hit " + objectWeHit.gameObject.name + " !");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }
        if (objectWeHit.gameObject.CompareTag("Wall"))
        {
            print("Hit a wall!");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }
        if (objectWeHit.gameObject.CompareTag("Beer"))
        {
            print("One less bottle of beer on the wall!");
            objectWeHit.gameObject.GetComponent<BeerBottle>().Shatter();
            // We will not destroy the bullet on impact.
        }
        if (objectWeHit.gameObject.CompareTag("Enemy"))
        {
            if (objectWeHit.gameObject.GetComponent<Enemy>().isDead == false)
            {
                print("Hit an enemy!");
                objectWeHit.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
            }

            CreateBloodSprayEffect(objectWeHit);

            Destroy(gameObject);
        }
    }

    private void CreateBloodSprayEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];
        GameObject bloodSprayPrefab = Instantiate(
            GlobalReferences.Instance.bloodSprayEffect,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );
        bloodSprayPrefab.transform.SetParent(objectWeHit.gameObject.transform);
    }

    void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];
        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );
        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}
