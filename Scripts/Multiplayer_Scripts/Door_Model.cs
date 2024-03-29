using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;
[RealtimeModel]
public partial class Door_Model 
{
    [RealtimeProperty(10, true, true)] bool _doorActive;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class Door_Model : RealtimeModel {
    public bool doorActive {
        get {
            return _doorActiveProperty.value;
        }
        set {
            if (_doorActiveProperty.value == value) return;
            _doorActiveProperty.value = value;
            InvalidateReliableLength();
            FireDoorActiveDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(Door_Model model, T value);
    public event PropertyChangedHandler<bool> doorActiveDidChange;
    
    public enum PropertyID : uint {
        DoorActive = 10,
    }
    
    #region Properties
    
    private ReliableProperty<bool> _doorActiveProperty;
    
    #endregion
    
    public Door_Model() : base(null) {
        _doorActiveProperty = new ReliableProperty<bool>(10, _doorActive);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _doorActiveProperty.UnsubscribeCallback();
    }
    
    private void FireDoorActiveDidChange(bool value) {
        try {
            doorActiveDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _doorActiveProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _doorActiveProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.DoorActive: {
                    changed = _doorActiveProperty.Read(stream, context);
                    if (changed) FireDoorActiveDidChange(doorActive);
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
        _doorActive = doorActive;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
