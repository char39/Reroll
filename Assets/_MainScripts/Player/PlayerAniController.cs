using UnityEngine;

public class PlayerAniController : MonoBehaviour
{
    private SpriteRenderer sr;

    void Start()
    {
        TryGetComponent(out sr);
    }

    public void FlipX(bool value)
    {
        if (sr == null) return;
        sr.flipX = value;
    }
}
