using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;
[RealtimeModel]

public partial class LinkP1_Model
{
    [RealtimeProperty(30, true, true)] bool _linkP1Active;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class LinkP1_Model : RealtimeModel {
    public bool linkP1Active {
        get {
            return _linkP1ActiveProperty.value;
        }
        set {
            if (_linkP1ActiveProperty.value == value) return;
            _linkP1ActiveProperty.value = value;
            InvalidateReliableLength();
            FireLinkP1ActiveDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(LinkP1_Model model, T value);
    public event PropertyChangedHandler<bool> linkP1ActiveDidChange;
    
    public enum PropertyID : uint {
        LinkP1Active = 30,
    }
    
    #region Properties
    
    private ReliableProperty<bool> _linkP1ActiveProperty;
    
    #endregion
    
    public LinkP1_Model() : base(null) {
        _linkP1ActiveProperty = new ReliableProperty<bool>(30, _linkP1Active);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _linkP1ActiveProperty.UnsubscribeCallback();
    }
    
    private void FireLinkP1ActiveDidChange(bool value) {
        try {
            linkP1ActiveDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _linkP1ActiveProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _linkP1ActiveProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.LinkP1Active: {
                    changed = _linkP1ActiveProperty.Read(stream, context);
                    if (changed) FireLinkP1ActiveDidChange(linkP1Active);
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
        _linkP1Active = linkP1Active;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */