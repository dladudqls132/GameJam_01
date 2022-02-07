using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagedPanel : MonoBehaviour
{
    // Start is called before the first frame update
    private Image image;

    private void Start()
    {
        image = this.GetComponent<Image>();
        image.enabled = false;
    }

    public IEnumerator StartDamagedPanel(float zeroToTime)
    {
        float alpha = 0.4f;
        image.color = new Color(1.0f, 0.0f, 0.0f, alpha);
        image.enabled = true;

        while (image.color.a > 0.002f)
        {
            alpha -= 0.4f * Time.deltaTime / zeroToTime;
            image.color = new Color(1.0f, 0.0f, 0.0f, alpha);
            yield return null;
        }
    }
}
