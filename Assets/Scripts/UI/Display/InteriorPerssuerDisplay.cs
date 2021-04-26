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

    private float _maxTime = Mathf.PI * 2;
    private float _timer = 0;
    private float speed = 4f;
    private float offset = 0;

    void Start()
    {
      offset = Random.value;
    }

    void FixedUpdate()
    {
      _timer += Time.deltaTime;
      if (_timer > _maxTime)
      {
        _timer = 0;
      }
      
      if (manager.GameState.GeneralState == GameState.MaschienState.Defective)
      {
        float diff = Mathf.Sin((_timer + offset) * speed);

        var diffRotation = (maxRotation - minRotation);
        var newRotation = minRotation +
                          (-diffRotation * ((diff + 1f) / 2f));

        pointer.transform.localRotation = Quaternion.Euler(0, 0, newRotation - offsetRotation);
      }
      else
      {
        var diffRotation = (maxRotation - minRotation);
        var newRotation = minRotation +
                          (-diffRotation *
                           (manager.GameState.InteriorPressure / manager.GameState.MaxInteriorPressure));
        pointer.transform.localRotation = Quaternion.Euler(0, 0, newRotation - offsetRotation);
      }
    }
  }
}