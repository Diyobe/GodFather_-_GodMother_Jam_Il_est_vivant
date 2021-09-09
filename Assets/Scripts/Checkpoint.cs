using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;
    [SerializeField] Animation respawnAnimation;

    public void UseCheckpoint(Rigidbody2D playerRb)
    {
        playerRb.velocity = Vector3.zero;
        playerRb.position = respawnPoint.position;

        playerRb.gameObject.SetActive(false);

        if (respawnAnimation)
        {
            respawnAnimation.gameObject.SetActive(true);
            respawnAnimation.Play();
        } 
    }

    public void StartRespawnAnimation(Player player)
    {
        StartCoroutine(StartAnimation(player));
    }

    IEnumerator StartAnimation(Player player)
    {
        respawnAnimation.gameObject.SetActive(true);
        if (SoundManager.Instance != null) SoundManager.Instance.PlayRespawnSound();
        yield return new WaitForSeconds(respawnAnimation.clip.length);
        player.gameObject.SetActive(true);
    }
}
