using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip gameOverMusic;

    public AudioClip jumpSfx;
    public AudioClip switchZombieSfx;
    public AudioClip coinSfx;

    public AudioClip startSfx;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void PlayBackgroundMusic()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.Stop();
            musicSource.loop = true;
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    public void PlayGameOverMusic()
    {
        if (musicSource != null && gameOverMusic != null)
        {
            musicSource.Stop();
            musicSource.loop = false;
            musicSource.clip = gameOverMusic;
            musicSource.Play();
        }
    }

    public void PlayJump()
    {
        if (sfxSource != null && jumpSfx != null)
        {
            sfxSource.PlayOneShot(jumpSfx);
        }
    }

    public void PlaySwitchZombie()
    {
        if (sfxSource != null && switchZombieSfx != null)
        {
            sfxSource.PlayOneShot(switchZombieSfx);
        }
    }

    public void PlayCoin()
    {
        if (sfxSource != null && coinSfx != null)
        {
            sfxSource.PlayOneShot(coinSfx);
        }
    }

    public void PlayStart()
    {
        if (sfxSource != null && startSfx != null)
        {
            sfxSource.PlayOneShot(startSfx);
        }
    }
}