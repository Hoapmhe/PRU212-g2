using UnityEngine;

public class Location : MonoBehaviour
{
    public CarColor locationColor;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite greenSprite;
    [SerializeField] private Sprite blueSprite;
    [SerializeField] private Sprite yellowSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateLocationColor()
    {
        switch (locationColor)
        {
            case CarColor.Green:
                spriteRenderer.sprite = greenSprite;
                break;
            case CarColor.Blue:
                spriteRenderer.sprite = blueSprite;
                break;
            case CarColor.Yellow:
                spriteRenderer.sprite = yellowSprite;
                break;
        }
    }
}
