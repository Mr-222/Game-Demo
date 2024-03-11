using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip[] skillSounds;

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
    
    public void playSkillSound(CardType cardType)
    {
        int index = (int)cardType;
        PlaySfx(skillSounds[index]);
    }
}