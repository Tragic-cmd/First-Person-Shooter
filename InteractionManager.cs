using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Weapon hoveredWeapon = null;
    public AmmoBox hoveredAmmoBox = null;
    public Throwable hoveredThrowable = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;

                if (objectHitByRaycast.GetComponent<Weapon>() && objectHitByRaycast.GetComponent<Weapon>().isActiveWeapon == false)
                {
                    // Disable the outline of the previously selected item. 
                    if (hoveredWeapon)
                    {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                    }

                    hoveredWeapon = objectHitByRaycast.GetComponent<Weapon>();
                    hoveredWeapon.GetComponent<Outline>().enabled = true;

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        WeaponManager.Instance.PickupWeapon(objectHitByRaycast.gameObject); 
                    }
                }
                else
                {
                    if (hoveredWeapon)
                    {
                        hoveredWeapon.GetComponent<Outline>().enabled = false;
                    }
                }
                // Ammo Box
                if (objectHitByRaycast.GetComponent<AmmoBox>())
                {
                    // Disable the outline of the previously selected item. 
                    if (hoveredAmmoBox)
                    {
                        hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                    }

                    hoveredAmmoBox = objectHitByRaycast.GetComponent<AmmoBox>();
                    hoveredAmmoBox.GetComponent<Outline>().enabled = true;

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        WeaponManager.Instance.PickupAmmo(hoveredAmmoBox);
                    Destroy(objectHitByRaycast.gameObject);
                    }
                }
                else
                {
                    if (hoveredAmmoBox)
                    {
                        hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                    }
                }
                // Throwables
                if (objectHitByRaycast.GetComponent<Throwable>())
                {
                    // Disable the outline of the previously selected item. 
                    if (hoveredThrowable)
                    {
                        hoveredThrowable.GetComponent<Outline>().enabled = false;
                    }

                    hoveredThrowable = objectHitByRaycast.GetComponent<Throwable>();
                    hoveredThrowable.GetComponent<Outline>().enabled = true;

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        WeaponManager.Instance.PickupThrowable(hoveredThrowable);
                    }
                }
                else
                {
                    if (hoveredThrowable)
                    {
                        hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                    }
                }
        }
    }

}
