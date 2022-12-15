using Game;
using UnityEngine;

public class GhostScatter : GhostBehavior
{
    public void OnDisable() {
        if(_ghost.Mod == GhostMod.Frighten || _ghost.Mod == GhostMod.Home) {
            return;
        }
        
        _ghost.SetGhostMod(GhostMod.Chase, true);
    }

    public override Vector2 FindDirection(Pallet pallet) {
        int index = Random.Range(0, pallet.AvailableDirections.Count);

        if (pallet.AvailableDirections.Count > 1 && pallet.AvailableDirections[index] == -_ghost.Movement.Direction) {
            index++;
            if (index >= pallet.AvailableDirections.Count) 
                index = 0;
        }
        
        return pallet.AvailableDirections[index];
    }
}
