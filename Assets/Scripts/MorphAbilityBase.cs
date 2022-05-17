using UnityEngine;
public abstract class MorphAbilityBase : ScriptableObject {

    public string title = "Base Name";
    public float cooldown = 1f;

    public GameObject form;
    
    public abstract void Init();
    public abstract void Transition();
    public abstract void Execute();
}
