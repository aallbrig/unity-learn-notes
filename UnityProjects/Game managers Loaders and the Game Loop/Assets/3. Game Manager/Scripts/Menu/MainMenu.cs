using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Events.EventFadeComplete onMainMenuFadeComplete;
    [SerializeField] private Animation _mainMenuAnimator;

    #region FadeIn
    [SerializeField] private AnimationClip _fadeInAnimation;

    private void PlayFadeIn() => PlayClip(_fadeInAnimation);

    public void OnFadeInStart()
    {
        Debug.LogWarning("On fade in start");
    }

    public void OnFadeInComplete()
    {
        UIManager.Instance.SetDummyCameraActive(true);
        onMainMenuFadeComplete.Invoke(false);
    }
    #endregion

    #region FadeOut
    [SerializeField] private AnimationClip _fadeOutAnimation;

    private void PlayFadeOut() => PlayClip(_fadeOutAnimation);

    public void OnFadeOutStart()
    {
        Debug.LogWarning("On fade out start");
        UIManager.Instance.SetDummyCameraActive(false);
    }

    public void OnFadeOutComplete()
    {
        onMainMenuFadeComplete.Invoke(true);
    }
    #endregion

    private void PlayClip(AnimationClip clip)
    {
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = clip;
        _mainMenuAnimator.Play();
    }

    private void HandleGameStateChange(GameManager.GameState prevState, GameManager.GameState currentState)
    {
        if (prevState == GameManager.GameState.Pregame && currentState == GameManager.GameState.Running)
        {
            PlayFadeOut();
        } else if (prevState != GameManager.GameState.Pregame && currentState == GameManager.GameState.Pregame)
        {
            PlayFadeIn();
        }
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChange);
    }
}
