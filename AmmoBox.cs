using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int ammoAmount = 120;
    public AmmoType ammoType;

    public enum AmmoType
    {
        ARAmmo,
        PistolAmmo
    }
}
