using UnityEngine;
using UnityEngine.UI;

public class OnButtonActive : MonoBehaviour
{
    public Image cursorImage;

    public void ShowCursor() => cursorImage.gameObject.SetActive(true);

    public void HideCursor() => cursorImage.gameObject.SetActive(false);
}
