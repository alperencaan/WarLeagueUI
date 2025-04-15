using System;
using UnityEngine;

public interface ICharacterCard
{
    ICharacter Character { get; }
    bool IsSelected { get; }
    event Action<ICharacterCard, bool> OnSelectionChanged;
    void SetSelected(bool selected);
    void Initialize(ICharacter character);
} 