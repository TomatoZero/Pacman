using Game;
using UnityEngine;

public class GhostChase : GhostBehavior
{
    public void OnDisable() {
        if(_ghost.Mod == GhostMod.Frighten || _ghost.Mod == GhostMod.Home) {
            return;
        }
        
        _ghost.SetGhostMod(GhostMod.Scatter, true);
    }

    public override Vector2 FindDirection(Pallet pallet) {
        Vector2 direction = Vector2.zero;
        float minDistance = float.MaxValue;

        foreach (Vector2 availableDirection in pallet.AvailableDirections) {
            Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
            float distance = (_ghost.Target.position - newPosition).sqrMagnitude;

            if (distance < minDistance) {
                direction = availableDirection;
                minDistance = distance;
            }
        }

        return direction;
    }
}
