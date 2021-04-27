using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class UIHealthPoint : MonoBehaviour
{
    [SerializeField] Sprite FullImage = null;

    [SerializeField] Sprite EmptyImage = null;

    public void Set(bool isFull)
    {
        GetComponent<SpriteRenderer>().sprite = (isFull ? FullImage : EmptyImage);

        StartCoroutine(Pop());
    }

    private IEnumerator Pop()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

        yield return new WaitForSeconds(0.2f);

        transform.localScale = Vector3.one;
    }
}
