using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ability
{
    public int id; 
    public string name; 
    public Sprite sprite;
    public string description;

    public enum abilityType { normal, rare, unique, legend};
    public abilityType type;
}

[CreateAssetMenu(fileName = "AbilitySO", menuName = "Scriptable Object/ AbilitySO")] // fileName = 생성될 파일 이름 menuName = 생성 경로
public class AbilitySo : ScriptableObject
{
    public Ability[] abilities;
}