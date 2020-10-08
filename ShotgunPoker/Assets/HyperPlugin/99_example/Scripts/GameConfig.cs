using UnityEngine;

[CreateAssetMenu(menuName = "Hyper/Create GameConfig", fileName = "GameConfig")]
public class GameConfig : ScriptableObject {
  public float MinX;
  public float MaxX;
  public float MaxPlayerSpeed;
  public float PlayerSpeed;
  public float PlayerBackSpeed;
  public float PlayerBackRate;
  public float ScrollSpeed;
  public float RingSpawnInterval;
}