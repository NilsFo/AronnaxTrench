using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Switch
{
  public class FuseSix : Interactible, IPointerClickHandler
  {
    public ShipUiManager manager;

    public GameObject off;
    public GameObject on;

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
        if (diff > 0f)
        {
          off.SetActive(false);
          on.SetActive(true);
        }
        else
        {
          off.SetActive(true);
          on.SetActive(false);
        }
      }
      else
      {
        if (manager.GameState.FuseSix == GameState.FuseState.On)
        {
          off.SetActive(false);
          on.SetActive(true);
        }
        else
        {
          off.SetActive(true);
          on.SetActive(false);
        }
      }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      if (manager.GameState.GeneralState != GameState.MaschienState.Defective)
      {
        if (manager.GameState.FuseSix == GameState.FuseState.Off)
        {
          manager.GameState.FuseSix = GameState.FuseState.On;
          FindObjectOfType<ambientSoundController>()?.PlayClick(1.1f);
        }
        else
        {
          manager.GameState.FuseSix = GameState.FuseState.Off;
          FindObjectOfType<ambientSoundController>()?.PlayClick(0.9f);
        }
      }
    }
  }
}