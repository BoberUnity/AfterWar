﻿using UnityEngine;

public class SetTaskEtap : Button3DBase
{
  [SerializeField] private Task task = null;
  [SerializeField] private int currentEtap = 0;

  protected override void MakeAction()
  {
    task.Etap = currentEtap;
  }
}
