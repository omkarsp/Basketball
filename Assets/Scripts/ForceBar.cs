using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ForceBar : MonoBehaviour
{
    private float forceValue = 0.5f;

    [SerializeField]
    private float minForceValue = 200;

    [SerializeField]
    private float maxForceValue = 800;

    [SerializeField]
    private Color lowColor = Color.green;

    [SerializeField]
    private Color midColor = Color.yellow;

    [SerializeField]
    private Color highColor = Color.red;

    private Image forceBarImage;
    private RectTransform rt;
    private Rect rect;
    private float maxHeight;
    private float barSpeedRatio = 2;

    private void Awake()
    {
        forceBarImage = GetComponent<Image>();
        rt = GetComponent<RectTransform>();
        rect = rt.rect;
        maxHeight = rect.height;
    }

    public float GetforceValue()
    {
        return forceValue;
    }

    private void OnEnable()
    {
        StartCoroutine(ForceBarRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(ForceBarRoutine());
    }

    IEnumerator ForceBarRoutine()
    {
        while (true)
        {
            float percentage = Mathf.Abs(Mathf.Sin(Time.time));
            rt.sizeDelta = new Vector2(rect.width, maxHeight * percentage);

            if (percentage <= 0.5)
            {
                var colorPercentage = Mathf.InverseLerp(0, 0.5f, percentage);
                forceBarImage.color = Color.Lerp(lowColor, midColor, colorPercentage);
            }
            else
            {
                var colorPercentage = Mathf.InverseLerp(0.5f, 1, percentage);
                forceBarImage.color = Color.Lerp(midColor, highColor, colorPercentage);
            }

            forceValue = Mathf.Lerp(minForceValue, maxForceValue, percentage);

            yield return new WaitForFixedUpdate();
        }
    }
}
