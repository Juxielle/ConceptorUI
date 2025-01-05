using ConceptorUI.Models;

namespace ConceptorUI.Platforms;

public abstract class PlatformPage
{
    public string Name { get; init; }
    public PlatformComponent StatusBar { get; init; }
    public PlatformComponent Body { get; init; }
    public PlatformComponent BottomBar { get; init; }
    public PlatformComponent DrawerPanel { get; init; }
    public bool IsComponent { get; init; }
    
    public new abstract string ToString(CompSerializer compSerializer);
}