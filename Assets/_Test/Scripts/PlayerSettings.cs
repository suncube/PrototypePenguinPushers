using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Configs/Add Player Settings")]
public class PlayerSettings : ScriptableObject
{
    public float Speed = 10;
    public float RotateSpeed = 5;
}