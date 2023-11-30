using UnityEngine;

public abstract class InputSource : MonoBehaviour
{
    public virtual bool IsJumping { get; }
    public virtual bool IsShooting { get; }
}
