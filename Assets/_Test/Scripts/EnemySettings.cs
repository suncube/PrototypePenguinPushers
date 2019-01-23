using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Configs/Add Enemy Settings")]
public class EnemySettings : ScriptableObject
{
    public float RunSpeed = 4;
    public float IdleSpeed = 2;
    // rigedbody drag
}