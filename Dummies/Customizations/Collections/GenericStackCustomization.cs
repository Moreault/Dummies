namespace ToolBX.Dummies.Customizations.Collections;

[AutoCustomization]
public sealed class GenericStackCustomization : GenericStackCustomizationBase
{
    public override IEnumerable<Type> Types => [typeof(Stack<>)];
    
    //TODO Use OPEX 3.0.0's ToStack method
    protected override object Convert<T>(IEnumerable<T> source)
    {
        var stack = new Stack<T>();
        foreach (var item in source) 
            stack.Push(item);
        return stack;
    }
}