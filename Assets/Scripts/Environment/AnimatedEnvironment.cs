using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedEnvironment : MonoBehaviour
{
    //private void Reset()
    //{
    //    EnvironmentManager.Instance.animatedEnvironments.Add(this);
    //}
    Animator animator;
    SpriteMask spriteMask;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        EnvironmentManager.Instance.animatedEnvironments.Add(this);
        if (GetComponent<SpriteMask>())
        {
            spriteMask = GetComponent<SpriteMask>();
        }
    }

    private void Start()
    {
        if (spriteMask != null)
        {
            spriteMask.isCustomRangeActive = true;
        }
    }

    public void PlayDeathAnimation()
    {
        animator.SetTrigger("enviro_death");
        if(spriteMask != null)
        {
            StartCoroutine(ActivateCustomRange());
        }
    }

    IEnumerator ActivateCustomRange()
    {
        spriteMask.isCustomRangeActive = false;
        yield return new WaitForSeconds(0.8f);
        spriteMask.isCustomRangeActive = true;
    }
}
