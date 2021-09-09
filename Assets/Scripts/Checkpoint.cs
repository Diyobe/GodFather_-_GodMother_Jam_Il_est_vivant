using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform respawnPoint;
    [SerializeField] Animation respawnAnimation;

    public void UseCheckpoint(Rigidbody2D playerRb)
    {
        playerRb.velocity = Vector3.zero;

        StartCoroutine(StartAnimation(playerRb));
    }

    IEnumerator StartAnimation(Rigidbody2D playerRb)
    {
        GameObject player = playerRb.gameObject;
        player.SetActive(false);


        if (SoundManager.Instance != null) SoundManager.Instance.PlayRespawnSound();

        respawnAnimation.gameObject.SetActive(true);
        respawnAnimation.Play();
        yield return new WaitForSeconds(respawnAnimation.clip.length);
        respawnAnimation.gameObject.SetActive(false);

        player.transform.position = respawnPoint.position;
        player.transform.rotation = Quaternion.identity;

        player.SetActive(true);
    }
}
