using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] float maxOrthographicSize;
    [SerializeField] float normalOrthographicSize;
    [SerializeField] float timeToZoomOut;
    [SerializeField] float timeToZoomIn;
    [SerializeField] float timerToZoomIn =0;
    public bool isRespawnAnimation;
    public bool isRespawnAnimationFinish;
    public Vector3 firstCameraPosition;
    [SerializeField] GameObject target;
    [SerializeField] int respawnStep = 0;
    [SerializeField] Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        normalOrthographicSize = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        Respawn();
    }

    void Respawn() {
        if (isRespawnAnimation) {
            if (respawnStep == 0) {
                camera.orthographicSize += (maxOrthographicSize - normalOrthographicSize) / timeToZoomOut * Time.deltaTime;
                Debug.Log(Time.unscaledTime);
                if (camera.orthographicSize >= maxOrthographicSize) {
                    firstCameraPosition = camera.transform.position;
                    respawnStep++;
                }
            }
            if (respawnStep == 1) {
                timerToZoomIn += 1 / timeToZoomIn * Time.deltaTime;
                camera.orthographicSize = Mathf.Lerp(maxOrthographicSize, normalOrthographicSize, timerToZoomIn);
                camera.transform.position = new Vector3(Mathf.Lerp(firstCameraPosition.x, target.transform.position.x, timerToZoomIn), Mathf.Lerp(firstCameraPosition.y, target.transform.position.y, timerToZoomIn), firstCameraPosition.z);
                if (camera.orthographicSize <= normalOrthographicSize) {
                    respawnStep++;
                }
            }
            if (respawnStep == 2) {
                if (isRespawnAnimationFinish) {
                    respawnStep = 0;
                    isRespawnAnimation = false;
                    isRespawnAnimationFinish = false;
                }
            }
        }
    }
}
