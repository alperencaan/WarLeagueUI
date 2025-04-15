using UnityEngine;

public interface ICharacter
{
    int Id { get; }
    string Name { get; }
    Sprite Icon { get; }
    string Description { get; }
} 