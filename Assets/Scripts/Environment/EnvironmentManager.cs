using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public List<AnimatedEnvironment> animatedEnvironments;

    private static EnvironmentManager _instance;

    public static EnvironmentManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EnvironmentManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayAllEnvironmentDeathAnim()
    {

        foreach (AnimatedEnvironment environment in animatedEnvironments)
        {
            environment.PlayDeathAnimation();
        }
    }

    public void StopAllEnvironmentDeathAnim()
    {

        foreach (AnimatedEnvironment environment in animatedEnvironments)
        {
            environment.StopDeathAnimation();
        }
    }
}
