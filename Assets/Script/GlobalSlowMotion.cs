using UnityEngine;

public class GlobalSlowMotion : MonoBehaviour
{
    [SerializeField] private float slowMotionTimeScale = 0.05f;
    [SerializeField] private AnimationCurve enterCurve = AnimationCurve.EaseInOut(0,0,1,1);
    [SerializeField] private AnimationCurve exitCurve = AnimationCurve.EaseInOut(0,0,1,1);
    [SerializeField] private float enterDuration = 1f;
    [SerializeField] private float exitDuration = 0.5f;
    [SerializeField] private AdaptiveMusicController musicController;


    private float originalTimeScale;
    private float originalFixedDeltaTime;
    private float currentTimeScale;
    private float transitionProgress;
    private bool isInSlowMotion;
    private float targetTimeScale;

    void Start()
    {
        originalTimeScale = Time.timeScale;
        originalFixedDeltaTime = Time.fixedDeltaTime;
        currentTimeScale = originalTimeScale;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool shouldCancelSlowMo = (horizontal != 0 || vertical != 0);

        bool wantsSlowMotion = !shouldCancelSlowMo;
        
        if(wantsSlowMotion != isInSlowMotion)
        {
            isInSlowMotion = wantsSlowMotion;
            transitionProgress = 0f;
            targetTimeScale = isInSlowMotion ? slowMotionTimeScale : 1f;
        }

        transitionProgress += Time.unscaledDeltaTime / (isInSlowMotion ? enterDuration : exitDuration);
        transitionProgress = Mathf.Clamp01(transitionProgress);

        float curveValue = isInSlowMotion ? 
            enterCurve.Evaluate(transitionProgress) : 
            exitCurve.Evaluate(transitionProgress);

        currentTimeScale = Mathf.Lerp(
            isInSlowMotion ? 1f : slowMotionTimeScale,
            targetTimeScale,
            curveValue
        );

        Time.timeScale = Mathf.Clamp(currentTimeScale, 0.01f, 1f);
        Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;
        if(musicController != null)
        {
            musicController.OnSlowMotionChanged(isInSlowMotion);
        }
    }

    void OnDestroy()
    {
        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = originalFixedDeltaTime;
    }
}