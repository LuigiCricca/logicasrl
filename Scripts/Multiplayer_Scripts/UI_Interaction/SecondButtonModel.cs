using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;
[RealtimeModel]
public partial class SecondButtonModel
{
    [RealtimeProperty(51, true, true)] bool _exitClicked;
    [RealtimeProperty(52, true, true)] bool _retryCicked;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class SecondButtonModel : RealtimeModel {
    public bool exitClicked {
        get {
            return _exitClickedProperty.value;
        }
        set {
            if (_exitClickedProperty.value == value) return;
            _exitClickedProperty.value = value;
            InvalidateReliableLength();
            FireExitClickedDidChange(value);
        }
    }
    
    public bool retryCicked {
        get {
            return _retryCickedProperty.value;
        }
        set {
            if (_retryCickedProperty.value == value) return;
            _retryCickedProperty.value = value;
            InvalidateReliableLength();
            FireRetryCickedDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(SecondButtonModel model, T value);
    public event PropertyChangedHandler<bool> exitClickedDidChange;
    public event PropertyChangedHandler<bool> retryCickedDidChange;
    
    public enum PropertyID : uint {
        ExitClicked = 51,
        RetryCicked = 52,
    }
    
    #region Properties
    
    private ReliableProperty<bool> _exitClickedProperty;
    
    private ReliableProperty<bool> _retryCickedProperty;
    
    #endregion
    
    public SecondButtonModel() : base(null) {
        _exitClickedProperty = new ReliableProperty<bool>(51, _exitClicked);
        _retryCickedProperty = new ReliableProperty<bool>(52, _retryCicked);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _exitClickedProperty.UnsubscribeCallback();
        _retryCickedProperty.UnsubscribeCallback();
    }
    
    private void FireExitClickedDidChange(bool value) {
        try {
            exitClickedDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireRetryCickedDidChange(bool value) {
        try {
            retryCickedDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _exitClickedProperty.WriteLength(context);
        length += _retryCickedProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _exitClickedProperty.Write(stream, context);
        writes |= _retryCickedProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.ExitClicked: {
                    changed = _exitClickedProperty.Read(stream, context);
                    if (changed) FireExitClickedDidChange(exitClicked);
                    break;
                }
                case (uint) PropertyID.RetryCicked: {
                    changed = _retryCickedProperty.Read(stream, context);
                    if (changed) FireRetryCickedDidChange(retryCicked);
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
        _exitClicked = exitClicked;
        _retryCicked = retryCicked;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
