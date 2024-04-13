using Scripts.UI;

public interface IUIWindowService 
{
    public abstract void OpenWindow(EUIWindow windowId);
    public abstract void CloseWindow(EUIWindow windowId);
}
