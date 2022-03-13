using System;

public interface IVisible
{
    event EventHandler VisibilityChanged;
    bool IsVisible { get; }
    void Show();
    void Hide();
}