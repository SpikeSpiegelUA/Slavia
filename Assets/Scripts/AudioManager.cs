using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] battleMusic;
    public AudioClip[] ambientMusic;
    public AudioSource backgroundMusicSource;
    public AudioClip attackSound;
    public AudioClip walkingSound;
    public AudioClip runningSound;
    public AudioClip hugeAttackSound;
    public AudioClip blockSound;
    public AudioClip withoutWeaponHit;
    public AudioClip withoutWeaponHugeHit;
    public AudioClip archerySound;
    public AudioClip crouchingSound;
    public AudioClip fireballSound;
    public AudioClip recoverSound;
    public AudioClip summonSound;
    public AudioClip twoHandHit;
    public AudioClip twoHandHugeHit;
    public AudioClip missSound;
    public AudioClip fallSound;
    public AudioClip workingSound;
    private bool ambientPlaying = false;
    private bool battlePlaying = false;
    private PlayerController player;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        BackGroundMusic();
    }
    //Background music
    private void BackGroundMusic()
    {
        //Play battle music if player is in fight
        if (player.combatEnemies > 0)
        {
            if (backgroundMusicSource.clip == null || battlePlaying == false)
            {
                backgroundMusicSource.clip = battleMusic[Random.Range(0, battleMusic.Length)];
                backgroundMusicSource.Play();
                battlePlaying = true;
                ambientPlaying = false;
            }
            if (backgroundMusicSource.isPlaying == false)
            {
                backgroundMusicSource.clip = battleMusic[Random.Range(0, battleMusic.Length)];
                backgroundMusicSource.Play();
                battlePlaying = true;
                ambientPlaying = false;
            }
        }
        //Play ambient if player isn't in fight
        if (player.combatEnemies == 0)
        {
            if (backgroundMusicSource.clip == null || ambientPlaying == false)
            {
                backgroundMusicSource.clip = ambientMusic[Random.Range(0, ambientMusic.Length)];
                backgroundMusicSource.Play();
                ambientPlaying = true;
                battlePlaying = false;
            }
            if (backgroundMusicSource.isPlaying == false)
            {
                backgroundMusicSource.clip = ambientMusic[Random.Range(0, ambientMusic.Length)];
                backgroundMusicSource.Play();
                ambientPlaying = true;
                battlePlaying = false;
            }
        }
    }
}
