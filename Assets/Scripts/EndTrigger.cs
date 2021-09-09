using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] GameObject toEnable;
    [SerializeField] GameObject toDisable;
    [Space(10), SerializeField] Transform newCameraTarget;

    Animator animator;
    bool trigger;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!trigger && collision.CompareTag("Player"))
        {
            trigger = true;
            Player.instance.gameObject.SetActive(false);
            StartCoroutine(StartEndAnimation());
        }
    }

    IEnumerator StartEndAnimation()
    {
        Camera cam = Camera.main;
        cam.GetComponent<CameraController>().SetTarget(newCameraTarget ?? transform);

        if (toEnable) toEnable.SetActive(true);
        if (toDisable) toDisable.SetActive(false);

        animator.enabled = true;
        animator.SetTrigger("start_grinder");

        yield return new WaitForSeconds(1.5f);

        cam.GetComponent<CameraZoom>().ZoomOut();
    }
}
