using UnityEngine;

namespace UI.Display
{
  public class DepthRoundDisplay : MonoBehaviour
  {
    public ShipUiManager manager;
    
    public float minRotation;
    public float maxRotation;
    public float offsetRotation;
    public float maxDepth = 320f;

    public GameObject pointer;

    void FixedUpdate()
    {
      var diffRotation = (maxRotation - minRotation);
      var newRotation = minRotation +
                        (-diffRotation * (-manager.GameState.CurrentDepth / maxDepth));
      pointer.transform.localRotation = Quaternion.Euler(0,0, newRotation-offsetRotation);
    }
  }
}
