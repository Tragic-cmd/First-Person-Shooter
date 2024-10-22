using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource ShootingChannel;
    public AudioSource reloadSoundGlock;
    public AudioSource emptyGlock;
    public AudioSource reloadSoundM4A1;
    public AudioSource emptyM4A1;

    public AudioClip GlockShot;
    public AudioClip M4A1Shot;

    public AudioSource ThrowablesChannel;
    public AudioClip grenadeSound;

    public AudioSource zombieChannel;
    public AudioSource zombieChannel2;

    public AudioClip zombieWalking;
    public AudioClip zombieChase;
    public AudioClip zombieAttack;
    public AudioClip zombieHurt;
    public AudioClip zombieDeath;

    public AudioSource playerChannel;
    public AudioClip playerHurt;
    public AudioClip playerDeath;

    public AudioClip gameOverMusic;

    public AudioSource musicChannel;
    public AudioClip musicTrack1;
    public AudioClip musicTrack2;

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

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch(weapon)
        {
            case WeaponModel.Glock:
                ShootingChannel.PlayOneShot(GlockShot);
                break;
            case WeaponModel.M4A1:
                ShootingChannel.PlayOneShot(M4A1Shot);
                break;
        }
    }

    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Glock:
                reloadSoundGlock.Play();
                break;
            case WeaponModel.M4A1:
                reloadSoundM4A1.Play();
                break;
        }
    }

    public void PlayEmptySound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Glock:
                emptyGlock.Play();
                break;
            case WeaponModel.M4A1:
                emptyM4A1.Play();
                break;
        }
    }

}