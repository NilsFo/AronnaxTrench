using UnityEngine;

namespace UI.Display
{
  public class DepthRoundDisplay : MonoBehaviour
  {
    public ShipUiManager manager;
    
    public float minRotation;
    public float maxRotation;
    public float offsetRotation;

    public GameObject pointer;
    
    void FixedUpdate()
    {
      var diffRotation = (maxRotation - minRotation);
      var newRotation = minRotation +
                        (diffRotation * (manager.GameState.CurrentDepth / manager.GameState.MaxDivePressure));
        
      pointer.transform.localRotation = Quaternion.Euler(0, newRotation-offsetRotation, 0);
    }
  }
}
