using System;
using UnityEngine;

public abstract class Window<T>
{
  public IVisible Visibility { get; private set; }
  public bool IsVisible =>  Visibility.IsVisible;
  public IContext _context { get; private set; }

  protected Window(IContext context)
  {
      _context = context;
  }
  
  protected T CreateView<T>
  (
      string name
  )
      where T : MonoBehaviour
  {
      var go = Resources.Load<GameObject>(name);
      if (go == null)
      {
          throw new ArgumentException($"Invalid prefab name {name}");
      }

      go.name = name;
      var instance = GameObject.Instantiate(go) as GameObject;
      
      return PostCreateView<T>(instance);
  }



  private T PostCreateView<T>
  (
      GameObject instance
  ) where T : MonoBehaviour
  {
      return instance.GetComponent<T>();
  }

}
