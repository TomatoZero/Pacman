using System.Collections;
using Game;
using UnityEngine;

public class GhostHome : GhostBehavior {
    [SerializeField] private Pallet inside;
    [SerializeField] private Pallet outside;
    [SerializeField] private Vector2 direction;

    public override Vector2 FindDirection(Pallet pallet) {

        if(pallet == inside) 
            return Vector2.up;
        else if(pallet == outside){
            _ghost.SetGhostMod(GhostMod.Scatter, true);
            return direction;
        }

        return (_ghost.transform.position - inside.transform.position);
    }

    private IEnumerator Wait(float duration) {
        while (duration > 0) {
            yield return new WaitForSeconds(1f);
            duration--;
        }
    }
}
