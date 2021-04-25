using UnityEngine;

namespace UI.Display
{
  public class InteriorPerssuerDisplay : MonoBehaviour
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
                        (-diffRotation * (manager.GameState.InteriorPressure / manager.GameState.MaxInteriorPressure));
      pointer.transform.localRotation = Quaternion.Euler(0,0, newRotation-offsetRotation);
    }
  }
}
