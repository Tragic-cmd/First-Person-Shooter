using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawnController : MonoBehaviour
{
    public int initialZombiesPerWave = 5;
    public int currentZombiesPerWave;

    public float spawnDelay = 0.5f; // Delay between spawning each Zombie in a wave. 

    public int currentWave = 0;
    public float waveCooldown = 10.0f; // Time in seconds between waves. 

    public bool inCooldown;
    public float cooldownCounter = 0; // We only use this for testing and the UI.

    public List<Enemy> currentZombiesAlive;

    public GameObject zombiePrefab;

    public TextMeshProUGUI waveOverUI;
    public TextMeshProUGUI cooldownCounterUI;

    public TextMeshProUGUI currentWaveUI;

    private void Start()
    {
        currentZombiesPerWave = initialZombiesPerWave;
        GlobalReferences.Instance.waveNumber = currentWave;
        StartNextWave();
    }

    private void StartNextWave()
    {
        currentZombiesAlive.Clear();

        currentWave++;

        GlobalReferences.Instance.waveNumber = currentWave;

        currentWaveUI.text = "Wave: " + currentWave.ToString();
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentZombiesPerWave; i++)
        {
            // Generate a random offset without a specific range
            Vector3 spawnOffset = new Vector3(Random.Range(+1f,1f), 0f, Random.Range(-1f,1f));
            Vector3 spawnPosition = transform.position + spawnOffset;

            // Instantiate the Zombie
            var zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);

            // Get Enemy Script
            Enemy enemyScript = zombie.GetComponent<Enemy>();

            // Track this Zombie
            currentZombiesAlive.Add(enemyScript);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Update()
    {
        // Get all dead Zombies
        List<Enemy> zombiesToRemove = new List<Enemy>();
        foreach (Enemy zombie in currentZombiesAlive)
        {
            if (zombie.isDead)
            {
                zombiesToRemove.Add(zombie);
            }
        }

        // Actually remove the dead Zombie
        foreach (Enemy zombie in zombiesToRemove)
        {
            currentZombiesAlive.Remove(zombie);
        }

        zombiesToRemove.Clear();

        // Start Cooldown if all Zombies are dead
        if (currentZombiesAlive.Count == 0 && inCooldown == false)
        {
            // Start Cooldown for next wave
            StartCoroutine(WaveCooldown());
        }

        // Run the cooldown counter
        if (inCooldown)
        {
            cooldownCounter = Time.deltaTime;
        }
        else
        {
            // Reset the counter
            cooldownCounter = waveCooldown;
        }

        cooldownCounterUI.text = cooldownCounter.ToString("F0");
    }

    private IEnumerator WaveCooldown()
    {
        inCooldown = true;
        waveOverUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;
        waveOverUI.gameObject.SetActive(false);

        currentZombiesPerWave *= 2; /// 5*2=10 10*2=20
        StartNextWave();
    }
}