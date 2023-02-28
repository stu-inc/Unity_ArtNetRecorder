namespace inc.stu.SyncArena
{
    public class IntInputField : InputField<int>
    {
        protected override int OnDragged(float dragAmount) => _dragStartValue + (int)dragAmount;
        protected override int Parse(string value) => int.TryParse(value, out var result) ? result : 0;
        protected override string Format(int value) => value.ToString();
    }

}

