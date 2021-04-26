using PurpleCable;
using System.Collections;
using UnityEngine;

public class Skeleton : PoolableMonoBehaviour
{
    private SpriteRenderer SpriteRenderer;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Set(FishDef def)
    {
        SpriteRenderer.sprite = def.SkeletonImage;
    }

    public void FadeOut()
    {
        StartCoroutine(DoFadeOut());
    }

    private IEnumerator DoFadeOut()
    {
        for (float a = 1; a >= 0; a -= 0.1f)
        {
            SpriteRenderer.color = new Color(1, 1, 1, a);

            yield return new WaitForSeconds(0.1f);
        }

        SetAsAvailable();
    }
}
