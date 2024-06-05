using UnityEngine;

public interface IMoveable
{
    void Move(Vector2 direction);

    void Run(bool value);
}