using UnityEngine;
using TMPro;
using System.Collections;

public class MapName : MonoBehaviour
{
    public TMP_Text mapNameText;
    public float fadeInDuration = 1.0f;
    public float displayDuration = 5.0f;
    public float fadeOutDuration = 1.0f;

    private void Start()
    {
        mapNameText.alpha = 0;
        StartCoroutine(ShowMapName());
    }

    private IEnumerator ShowMapName()
    {
        // Fade in
        float elapsedTime = 0;
        while (elapsedTime < fadeInDuration)
        {
            mapNameText.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mapNameText.alpha = 1;

        // Display for a while
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        elapsedTime = 0;
        while (elapsedTime < fadeOutDuration)
        {
            mapNameText.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeOutDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mapNameText.alpha = 0;
    }
}