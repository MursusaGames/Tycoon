using UnityEngine;

public class TextureRolling : MonoBehaviour
{
    public float speed =-0.19f;
    Renderer rend;
    float offset;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        // Increase offset based on time
        offset += Time.deltaTime * speed;
        // Keep offset between 0 and 1
        if (offset > 1)
            offset -= 1;
        // Apply the offset to the material
        rend.material.mainTextureOffset = new Vector2(0, offset);
    }
}
