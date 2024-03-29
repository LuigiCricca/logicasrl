using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]

public partial class SingleLedModel 
{


    [RealtimeProperty(1, false,true)] private Color _ledColor;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class SingleLedModel : RealtimeModel {
    public UnityEngine.Color ledColor {
        get {
            return _ledColorProperty.value;
        }
        set {
            if (_ledColorProperty.value == value) return;
            _ledColorProperty.value = value;
            InvalidateUnreliableLength();
            FireLedColorDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(SingleLedModel model, T value);
    public event PropertyChangedHandler<UnityEngine.Color> ledColorDidChange;
    
    public enum PropertyID : uint {
        LedColor = 1,
    }
    
    #region Properties
    
    private UnreliableProperty<UnityEngine.Color> _ledColorProperty;
    
    #endregion
    
    public SingleLedModel() : base(null) {
        _ledColorProperty = new UnreliableProperty<UnityEngine.Color>(1, _ledColor);
    }
    
    private void FireLedColorDidChange(UnityEngine.Color value) {
        try {
            ledColorDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _ledColorProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _ledColorProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.LedColor: {
                    changed = _ledColorProperty.Read(stream, context);
                    if (changed) FireLedColorDidChange(ledColor);
                    break;
                }
                default: {
                    stream.SkipProperty();
                    break;
                }
            }
            anyPropertiesChanged |= changed;
        }
        if (anyPropertiesChanged) {
            UpdateBackingFields();
        }
    }
    
    private void UpdateBackingFields() {
        _ledColor = ledColor;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
