using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private GameObject timerObject;

    [SerializeField]
    float maxDuration = 40;

    private float duration = 40;

    private float beepDuration = 5;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip beep;

    public static event Action TimerFinishedAction;

    private void Start()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        StartCoroutine(TimerRoutine());
    }

    public void PauseTimer()
    {
        StopCoroutine(TimerRoutine());
    }

    public void ResetTimerRoutine()
    {
        duration = 40;
        UpdateTimerValueView();
    }

    private IEnumerator TimerRoutine()
    {
        timerText.enabled = true;

        while (duration > 0)
        {
            yield return new WaitForSeconds(1);

            duration--;
            UpdateTimerValueView();
            if (duration == 0)
            {
                TimerFinishedAction();
                timerText.enabled = false;
            }

            //blink timer for last 5 seconds
            if (duration <= beepDuration)
            {
                StartCoroutine(BlinkRoutine(0.5f));

                //add beeping audio for last 5 seconds
                audioSource.PlayOneShot(beep, 1);
            }
        }
    }

    private void UpdateTimerValueView()
    {
        timerText.text = duration > 9 ? duration.ToString() : "0" + duration.ToString();
    }

    private IEnumerator BlinkRoutine(float blinkGap)
    {
        timerText.enabled = false;
        yield return new WaitForSeconds(blinkGap);
        timerText.enabled = true;
    }
}
