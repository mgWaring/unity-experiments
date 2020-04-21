using UnityEngine;

public interface ICharacter {
    Vector3 Aim (Vector3 target);
    void Move(Vector2 movement);
    void Jump();
}