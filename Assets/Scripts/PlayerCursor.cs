using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerCursor : MonoBehaviour, IPointerClickHandler
{
    public GameState gameState;
    public Transform defaultCursor;
    public Transform interactCursor;
    public Transform cameraCursor;
    public Transform cameraIdleCursor;
    public List<RectTransform> cameraTargets;
    public Camera cameraFront, cameraLeft, cameraRight;
    private float camFlash;
    public Image camFlashOverlay;

    public GameObject monsterFish;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(cameraIdleCursor.gameObject.activeInHierarchy) {
            gameState.Camera = GameState.CameraState.Disarm;
        }
        else if(cameraCursor.gameObject.activeInHierarchy) {
            if(camFlash > 0) {
                // Debug.Log("Camera cooldown");
                return;
            }
            // Debug.Log("Taking a picture");
            
            // camera flash
            FireCameraFlash();

            // Find out which window we are taking a pic from
            RectTransform camTarget = null;
            Rect newRect = new Rect();
            Vector3[] worldCorners = new Vector3[4];
            foreach(var ct in cameraTargets) {
                ct.GetWorldCorners(worldCorners);
                newRect = new Rect(worldCorners[0], worldCorners[2]-worldCorners[0]);
                if(newRect.Contains(Input.mousePosition)) {
                    camTarget = ct;
                    break;
                }
            }
            if(camTarget == null) {
                return;
            }

            // Find the ray parameters
            Vector2 mousePos = eventData.position;
            Vector2 rectPos = new Vector2(newRect.x, newRect.y);
            Vector2 localPos = mousePos - rectPos;

            var texture = camTarget.GetComponent<UnityEngine.UI.RawImage>().texture;
            Camera activeCamera = null;
            if(cameraFront.targetTexture == texture)
                activeCamera = cameraFront;
            else if(cameraLeft.targetTexture == texture)
                activeCamera = cameraLeft;
            else if(cameraRight.targetTexture == texture)
                activeCamera = cameraRight;
            if(activeCamera == null)
                return;
            
            // Annihilate the fishes with a barrage of deadly raytraces
            int fishcount = 0;
            for(int i = -320; i <= 320; i+=64) {
                for(int j = -192; j <= 192; j+=64) {
                    var rayScreenPos = localPos + new Vector2(i,j);
                    if(newRect.Contains(rayScreenPos + rectPos)) {
                        var ray = activeCamera.ScreenPointToRay(rayScreenPos);
                        Debug.DrawRay(ray.origin, ray.direction*20, Color.magenta, 20f);
                        //Debug.Log(ray);
                        var hits = Physics.RaycastAll(ray.origin, ray.direction, 100);
                        foreach(var hit in hits) {
                            print(hit.transform.gameObject.name);

                            FishAI fish = hit.transform.GetComponent<FishAI>();
                            if(fish != null) {
                                fish.Catch();  // TODO: sound if something is catched?
                                fishcount++;
                            }

                            MoveAlongPath monsterPath = hit.transform.GetComponent<MoveAlongPath>();
                            if (monsterPath != null && !gameState.tookFotoOfMonster)
                            {
                                print("You took a picture of the monster! Ending game...");
                                gameState.tookFotoOfMonster = true;
                                gameState.Camera = GameState.CameraState.Disarm;
                            }
                        }
                    }
                }
            }
            // Debug.Log("Hit fish " + fishcount + " times while taking a picture");
        }
    }

    private void FireCameraFlash() {
        camFlash = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        defaultCursor.transform.position = Input.mousePosition;
        interactCursor.transform.position = Input.mousePosition;
        cameraCursor.transform.position = Input.mousePosition;
        cameraIdleCursor.transform.position = Input.mousePosition;

        if(gameState.Camera == GameState.CameraState.Armed) {
            bool inCameraCaptureArea = false;
            Vector3[] worldCorners = new Vector3[4];
            foreach(var camtarget in cameraTargets) {
                camtarget.GetWorldCorners(worldCorners);
                Rect newRect = new Rect(worldCorners[0], worldCorners[2]-worldCorners[0]);
                if(newRect.Contains(Input.mousePosition)) {
                    inCameraCaptureArea = true;
                    break;
                }
            }

            if(inCameraCaptureArea) {
                defaultCursor.gameObject.SetActive(false);
                interactCursor.gameObject.SetActive(false);
                cameraIdleCursor.gameObject.SetActive(false);
                cameraCursor.gameObject.SetActive(true);
            } else {
                defaultCursor.gameObject.SetActive(false);
                interactCursor.gameObject.SetActive(false);
                cameraIdleCursor.gameObject.SetActive(true);
                cameraCursor.gameObject.SetActive(false);
            }
        } else {
            defaultCursor.gameObject.SetActive(true);
            interactCursor.gameObject.SetActive(false);
            cameraIdleCursor.gameObject.SetActive(false);
            cameraCursor.gameObject.SetActive(false);
        }

        if(camFlash > 0) {
            camFlashOverlay.color = new Color(1,1,1,camFlash);
            camFlash -= 0.4f*Time.deltaTime;
        }
    }
}
