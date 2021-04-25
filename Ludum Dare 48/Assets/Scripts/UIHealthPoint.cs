using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class UIHealthPoint : MonoBehaviour
{
    [SerializeField] Sprite FullImage = null;

    [SerializeField] Sprite EmptyImage = null;

    public void Set(bool isFull)
    {
        GetComponent<SpriteRenderer>().sprite = (isFull ? FullImage : EmptyImage);
    }
}
