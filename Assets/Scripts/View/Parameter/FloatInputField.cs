using UnityEngine;

namespace inc.stu.SyncArena
{
    
    public class FloatInputField : InputField<float>
    {

        [SerializeField] private string _format = "0.0";
        
        protected override float OnDragged(float dragAmount) => _dragStartValue + dragAmount;

        protected override float Parse(string value) => float.TryParse(value, out var result) ? result : 0;
        protected override string Format(float value) => value.ToString(_format);
    }

}
