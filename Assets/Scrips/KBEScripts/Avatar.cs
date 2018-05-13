using UnityEngine;
using UnityEditor;
using KBEngine;

public class Avatar : AvatarBase
{
    public override void onCountryChanged(byte oldValue)
    {
        Debug.LogFormat("onCountryChanged old:[{0}] new:[{1}]", oldValue, Country);
    }

    public override void onSexChanged(byte oldValue)
    {
        Debug.LogFormat("onSexChanged old:[{0}] new:[{1}]", oldValue, Sex);
    }

    public override void onPlayerNameChanged(string oldValue)
    {
        Debug.LogFormat("onNameChanged old:[{0}] new:[{1}]", oldValue, PlayerName);
    }

    public override void onGoldChanged(int oldValue)
    {
        Debug.LogFormat("onNameChanged old:[{0}] new:[{1}]", oldValue, Gold);
    }
    public override void onPositionChanged(Vector3 oldValue)
    {
        KBEngine.Event.fireOut("onPositionChanged", new object[] { this, position });
        Debug.LogFormat("onPositionChanged old:[{0}] new:[{1}]", oldValue, position);
    }
    public override void onEnterWorld()
    {
        base.onEnterWorld();

        // 当玩家进入世界时，请求获取自己的技能列表
        if (isPlayer())
        {
            KBEngine.Event.fireOut("onAvatarEnterWorld", new object[] { KBEngineApp.app.entity_uuid, id, this });
        }
    }
    public override void onEnterSpace()
    {
        KBEngine.Event.fireOut("onEnterSpace", new object[] { this, PlayerName });
        base.onEnterSpace();
    }

    public override void onLeaveWorld()
    {
        KBEngine.Event.fireOut("onEnterWorld", new object[] { this, PlayerName });
        base.onLeaveWorld();
    }
}