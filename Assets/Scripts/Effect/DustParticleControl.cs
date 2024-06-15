using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticleControl : MonoBehaviour
{
    private bool createDustOnWalk = true;
    private ParticleSystem dustParticleSystem;

    private void Awake()
    {
        dustParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void CreateDustParticles()
    {
        if (createDustOnWalk)
        {
            dustParticleSystem.Stop();
            dustParticleSystem.Play();
        }
    }
}