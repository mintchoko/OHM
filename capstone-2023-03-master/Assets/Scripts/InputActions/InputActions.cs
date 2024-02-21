
//인풋 액션들을 관리
public static class InputActions
{
    public static KeyActions keyActions; 
    
    static InputActions()
    {
        keyActions = new KeyActions();
        keyActions.Enable();
    }
}
