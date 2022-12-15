using Game;
using UnityEngine;

public abstract class GhostBehavior : MonoBehaviour {
    [SerializeField] private float duration;
    
    protected Ghost _ghost;
    protected float Duration {
        get => duration;
    }

    private void Awake() {
        _ghost = GetComponent<Ghost>();
    }
    
    
    public void Enable()
    {
        Enable(duration);
    }

    public virtual void Enable(float duration)
    {
        enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        enabled = false;
        CancelInvoke();
    }

    public abstract Vector2 FindDirection(Pallet pallet);
}
