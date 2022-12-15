using Game;
using UnityEngine;

public class GhostFrightened : GhostBehavior {


    public override void Disable() {
        enabled = false;
    }

    private void OnEnable() {
        _ghost.SetRenderer(false, Color.white);
        _ghost.Movement.SpeedMultiper = 0.7f;
        _ghost.SetAnimatorTrigger("FrightenedModeStart");
    }

    private void OnDisable() {
        _ghost.SetRenderer(true, _ghost.DefaultColor);
        _ghost.Movement.SpeedMultiper = 2f;
        _ghost.SetAnimatorTrigger("FrightenedModeEnd");
        _ghost.SetGhostMod(GhostMod.Scatter, true);
    }

    public override Vector2 FindDirection(Pallet pallet) {
        Vector2 direction = Vector2.zero;
        float maxDistance = float.MinValue;

        foreach (Vector2 availableDirection in pallet.AvailableDirections) {
            Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
            float distance = (_ghost.Target.position - newPosition).sqrMagnitude;

            if (distance > maxDistance) {
                direction = availableDirection;
                maxDistance = distance;
            }
        }

        return direction;
    }
}
