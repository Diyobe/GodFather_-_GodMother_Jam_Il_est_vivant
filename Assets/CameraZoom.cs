using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] float maxOrthographicSize;
    float normalOrthographicSize;
    [SerializeField] float timeToZoomOut;
    [SerializeField] float timeToZoomIn;
    float timerToZoomIn = 0;
    bool isRespawnAnimation;
    bool isRespawnAnimationFinish;
    Vector3 firstCameraPosition;
    GameObject target;
    int respawnStep = 0;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null) cam = GetComponent<Camera>();
        normalOrthographicSize = cam.orthographicSize;

        target = Player.instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Respawn();
    }

    void Respawn()
    {
        if (isRespawnAnimation)
        {
            if (respawnStep == 0)
            {
                cam.orthographicSize += (maxOrthographicSize - normalOrthographicSize) / timeToZoomOut * Time.deltaTime;
                Debug.Log(Time.unscaledTime);
                if (cam.orthographicSize >= maxOrthographicSize)
                {
                    firstCameraPosition = cam.transform.position;
                    respawnStep++;
                }
            }
            if (respawnStep == 1)
            {
                timerToZoomIn += 1 / timeToZoomIn * Time.deltaTime;
                cam.orthographicSize = Mathf.Lerp(maxOrthographicSize, normalOrthographicSize, timerToZoomIn);
                cam.transform.position = new Vector3(Mathf.Lerp(firstCameraPosition.x, target.transform.position.x, timerToZoomIn), Mathf.Lerp(firstCameraPosition.y, target.transform.position.y, timerToZoomIn), firstCameraPosition.z);
                if (cam.orthographicSize <= normalOrthographicSize)
                {
                    respawnStep++;
                }
            }
            if (respawnStep == 2)
            {
                respawnStep = 0;
                timerToZoomIn = 0;
                isRespawnAnimation = false;
                isRespawnAnimationFinish = false;
            }
        }
    }

    public void StartRespawn(GameObject newTarget)
    {
        GameObject oldTarget = target;
        target = newTarget;

        isRespawnAnimation = true;
    }

    //IEnumerator StartRespawnAnimation()
    //{
    //    //Step 0
    //    while (camera.orthographicSize < maxOrthographicSize)
    //    {
    //        camera.orthographicSize += (maxOrthographicSize - normalOrthographicSize) / timeToZoomOut * Time.deltaTime;
    //        yield return null;
    //    }
    //    camera.orthographicSize = maxOrthographicSize;

    //    // Step 1
    //    float timer = 0;
    //    while (timer < timerToZoomIn)
    //    {
    //        timer += Time.deltaTime;
    //        yield return null;
    //    }
    //}
}
