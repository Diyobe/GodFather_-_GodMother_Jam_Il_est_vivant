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
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        EnvironmentManager.Instance.animatedEnvironments.Add(this);
    }

    public void PlayDeathAnimation()
    {
        animator.SetBool("arbre_death", true);
    }

    public void StopDeathAnimation()
    {
        animator.SetBool("arbre_death", false);
    }
}
