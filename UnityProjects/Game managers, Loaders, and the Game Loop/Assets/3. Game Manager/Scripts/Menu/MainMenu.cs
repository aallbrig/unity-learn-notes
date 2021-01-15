using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Functions to play fade in/out animations
    [SerializeField] private Animation _mainMenuAnimator;

    #region FadeIn
    [SerializeField] private AnimationClip _fadeInAnimation;

    public void PlayFadeIn() => PlayClip(_fadeInAnimation);

    public void OnFadeInStart()
    {
        Debug.LogWarning("On fade in start");
    }

    public void OnFadeInComplete()
    {
        Debug.LogWarning("On fade in complete");
        UIManager.Instance.SetDummyCameraActive(true);
    }
    #endregion

    #region FadeOut
    [SerializeField] private AnimationClip _fadeOutAnimation;

    public void PlayFadeOut() => PlayClip(_fadeOutAnimation);

    public void OnFadeOutStart()
    {
        Debug.LogWarning("On fade out start");
        UIManager.Instance.SetDummyCameraActive(false);
    }

    public void OnFadeOutComplete()
    {
        Debug.LogWarning("On fade out complete");
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
        }
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChange);
    }
}
