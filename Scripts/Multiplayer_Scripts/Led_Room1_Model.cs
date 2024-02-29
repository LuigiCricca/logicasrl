using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class Led_Room1_Model
{

    [RealtimeProperty(5, false, true)] private Color _ledP1Colors;

}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class Led_Room1_Model : RealtimeModel {
    public UnityEngine.Color ledColors {
        get {
            return _ledColorsProperty.value;
        }
        set {
            if (_ledColorsProperty.value == value) return;
            _ledColorsProperty.value = value;
            InvalidateUnreliableLength();
            FireLedColorsDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(Led_Room1_Model model, T value);
    public event PropertyChangedHandler<UnityEngine.Color> ledColorsDidChange;
    
    public enum PropertyID : uint {
        LedColors = 5,
    }
    
    #region Properties
    
    private UnreliableProperty<UnityEngine.Color> _ledColorsProperty;
    
    #endregion
    
    public Led_Room1_Model() : base(null) {
        _ledColorsProperty = new UnreliableProperty<UnityEngine.Color>(5, _ledP1Colors);
    }
    
    private void FireLedColorsDidChange(UnityEngine.Color value) {
        try {
            ledColorsDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _ledColorsProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _ledColorsProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.LedColors: {
                    changed = _ledColorsProperty.Read(stream, context);
                    if (changed) FireLedColorsDidChange(ledColors);
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
        _ledP1Colors = ledColors;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */