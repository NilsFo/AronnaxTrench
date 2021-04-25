using UnityEngine;

namespace UI.Display
{
  public class BallastDisplay : MonoBehaviour
  {
    public ShipUiManager manager;

    public RectTransform myTransform;

    public float posMin;
    public float posMax;

    public float minHight;
    public float maxHight;

    void FixedUpdate()
    {
      var factor = (manager.GameState.CurrentDivePressure / manager.GameState.MaxDivePressure);
      var rect = myTransform.rect;
       
      myTransform.rect.Set(rect.x,factor * (posMax - posMin),rect.y,factor * (maxHight - minHight));
    }
  }
}
