using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    [SerializeField] private AudioClip coinSound;
    
    void Awake()
    {
        // Ensure there's only one AudioManager instance (Singleton Pattern)
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // Don't destroy this object when loading new scenes
    }

    private void OnEnable()
    {
        Coin.OnCoinCollected += playCoinSound;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= playCoinSound;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void playCoinSound()
    {
        PlaySfx(coinSound);
    }
}