using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 moveSpeed = new Vector3(0,70,0);
    public float TimeToFade = 1.5f;
    RectTransform rectTransform;
    TextMeshProUGUI textMeshPro;

    private float TimeElapsed;
    private Color startColor;

    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    private void Update()
    {

       rectTransform.position += moveSpeed * Time.deltaTime;

        TimeElapsed += Time.deltaTime;

        if (TimeElapsed < TimeToFade)
        {
            float fadeAlpha = Time.deltaTime / TimeToFade;
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);

        }
    


    }
}
