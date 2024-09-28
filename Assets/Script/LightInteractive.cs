using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightInteractive : MonoBehaviour, IInteractiveWithLight
{
    public void InteractWithLight()
    {
        Debug.Log("LightInteractive: InteractWithLight");
        StartCoroutine(StartInteraction());
    }
    public IEnumerator StartInteraction()
    {
        float lightDuration = 2f;  
        float elapsedTime = 0f;
        while (elapsedTime < lightDuration)
        {
            gameObject.GetComponent<Light2D>().intensity = Mathf.Lerp(0, 3, (elapsedTime / lightDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Start EndInteraction after StartInteraction completes
        StartCoroutine(EndInteraction());
    }

    public IEnumerator EndInteraction()
    {
        float lightDuration = 2f;
        float elapsedTime = 0f;
        while (elapsedTime < lightDuration)
        {
            gameObject.GetComponent<Light2D>().intensity = Mathf.Lerp(3, 0, (elapsedTime / lightDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(lightDuration);
    }
}
