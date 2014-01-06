﻿using UnityEngine;

public class ParticleEmit : Button3DBase
{
  [SerializeField] private ParticleEmitter emitter = null;

  protected override void MakeAction()
  {
    emitter.emit = true;
  }
}
