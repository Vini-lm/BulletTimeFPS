using UnityEngine;
using UnityEngine.Audio;

public class AdaptiveMusicController : MonoBehaviour
{    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioLowPassFilter lowPassFilter;
    [SerializeField] private float pitchSmoothTime = 0.5f;
    [SerializeField] private AnimationCurve pitchCurve = AnimationCurve.Linear(0, 0, 1, 1);

    private float targetPitch;
    private float currentPitch;
    private float pitchVelocity;

    void Start()
    {
        currentPitch = Time.timeScale;
        UpdateAudioSettings();
    }

    void Update()
    {
        float pitchMultiplier = Time.timeScale < 1f ? 2.0f : 1.0f;
    targetPitch = pitchCurve.Evaluate(Time.timeScale* pitchMultiplier) ;

    currentPitch = Mathf.SmoothDamp(currentPitch, targetPitch, ref pitchVelocity, pitchSmoothTime);
    
    UpdateAudioSettings();
    }

    void UpdateAudioSettings()
    {
        if(musicSource != null)
        {
            musicSource.pitch = Mathf.Clamp(currentPitch, 0.01f, 1f);
        }

        if(lowPassFilter != null)
        {
            lowPassFilter.cutoffFrequency = Mathf.Lerp(500f, 22000f, currentPitch);
        }
    }

    public void OnSlowMotionChanged(bool isSlowMotion)
    {
        pitchSmoothTime = isSlowMotion ? 2f : 0.5f;
    }
}