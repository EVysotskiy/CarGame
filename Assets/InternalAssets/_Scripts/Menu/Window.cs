using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Window
{
  public IVisible Visibility { get; private set; }
  public bool IsVisible =>  Visibility.IsVisible;
}
