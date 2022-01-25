using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048, 2048);

    // Start is called before the first frame update 
    void Start()
    {
        var r = GetComponent<Renderer>();

        textureSize = new Vector2(Mathf.Clamp(textureSize.x * transform.localScale.x * 10, 0, 3072), Mathf.Clamp(textureSize.y * transform.localScale.z * 10, 0, 3072));
        texture = new Texture2D((int)(textureSize.x), (int)(textureSize.y));
        r.material.mainTexture = texture;
    }
}
