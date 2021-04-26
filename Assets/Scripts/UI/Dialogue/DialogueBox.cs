using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public Transform trackObject;

    public UnityEngine.UI.Image image;
    public float marginX = 25, marginY = 25;
    public TMPro.TextMeshProUGUI textMesh;
    public float maxWidth = 400f;
    public float secondsPerCharacter = 0.02f;
    private float textBuildDelta = 0f;
    public float lifetime = 5f;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(trackObject == null) {
            Destroy(gameObject);
            return;
        }
        updateScreenPosition(trackObject.transform.position);

        lifetime -= Time.deltaTime;

        if(secondsPerCharacter > 0 && textMesh.maxVisibleCharacters < textMesh.text.Length) {
            textBuildDelta += Time.deltaTime;
            if(textBuildDelta > secondsPerCharacter/1000f) {
                var n = Mathf.FloorToInt(textBuildDelta / secondsPerCharacter);
                textBuildDelta -= n * secondsPerCharacter;

                textMesh.maxVisibleCharacters += n;
            }
        }

        if(lifetime <= 0) {
            Destroy(gameObject);
        }

    }

    public void updateScreenPosition(Vector3 pos) {
        
        /*var canvasrect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        Debug.Log(pos.x);
        if(pos.x + GetComponent<RectTransform>().rect.width > canvasrect.rect.width) {
            pos.x = canvasrect.rect.width - GetComponent<RectTransform>().rect.width;
        }
        if(pos.x < 32) {
            pos.x = 32;
        }*/
        transform.position = pos;
    }

    public void SetText(string text) {
        textMesh.GetComponent<UnityEngine.UI.LayoutElement>().preferredWidth = maxWidth;
        textMesh.rectTransform.sizeDelta.Set(maxWidth, 100);
        textMesh.SetText(text);
        var bounds = textMesh.GetPreferredValues();
        //textMesh.ForceMeshUpdate();
        
        //Debug.Log(bounds);
        //bounds.x += 2*marginX;
        //bounds.y += 2*marginY;
        //image.rectTransform.sizeDelta = bounds;
        textMesh.GetComponent<UnityEngine.UI.LayoutElement>().preferredWidth = Mathf.Min(maxWidth, bounds.x);

        if(secondsPerCharacter > 0) {
            textMesh.maxVisibleCharacters = 1;
            textBuildDelta = 0f;
        }
    }
}
