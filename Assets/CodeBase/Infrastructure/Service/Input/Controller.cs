using CodeBase.Character;
using UnityEngine;

namespace CodeBase.Infrastructure.Service.Input
{
    public abstract class Controller : ScriptableObject
    {
        public CharacterMove Character { get; set; }
        public abstract void Init();
        public abstract void OnCharacterUpdate();
    }
}
