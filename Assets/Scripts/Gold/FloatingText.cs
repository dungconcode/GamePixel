using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public TextMeshProUGUI label;
    public float lifetime = 0.8f;
    public Vector3 move = new Vector3(0, 30f, 0); // pixel
    public AnimationCurve alphaCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    Vector3 _startPos;
    float _t;

    public void Show(string text)
    {
        if (label == null) label = GetComponent<TextMeshProUGUI>();
        label.text = text;
        _startPos = transform.localPosition;
        _t = 0f;
    }

    void Update()
    {
        _t += Time.unscaledDeltaTime / lifetime;
        float t = Mathf.Clamp01(_t);

        // move + fade
        transform.localPosition = _startPos + move * t;

        var c = label.color;
        c.a = alphaCurve.Evaluate(t);
        label.color = c;

        if (_t >= 1f) Destroy(gameObject);
    }
}
