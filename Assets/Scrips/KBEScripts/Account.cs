using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class Account : AccountBase {


    public override void __init__()
    {
        base.__init__();
        Debug.LogFormat("账号初始化成功");
        KBEngine.Event.fireOut("onLoginSuccessfully");

        KBEngine.Event.registerIn("CreateAvatar", this, "CreateAvatar");
    }

    public override void onReqAvatar(byte arg1)
    {
       if (arg1 == 0)
        {
            Debug.Log("没有创建角色！");
            KBEngine.Event.fireOut("onHaveNoAvatar");

        }
        if (arg1 == 1)
        {
            Debug.Log("获取角色！");
            KBEngine.Event.fireOut("onHaveAvatar");
        }
    }

    public override void onCreateAvatarResult(byte arg1, byte[] arg2)
    {
        throw new System.NotImplementedException();

    }

    public void CreateAvatar(UINT8 country, UINT8 sex, string name)
    {
        baseEntityCall.reqCreateAvatar((byte)country, (byte)sex, name);
    }

    //public override void onCountryChanged(byte oldValue)
    //{
    //    Debug.LogFormat("onCountryChanged old:[{0}] new:[{1}]", oldValue,Country);
    //}

    //public override void onSexChanged(byte oldValue)
    //{
    //    Debug.LogFormat("onSexChanged old:[{0}] new:[{1}]", oldValue, Sex);
    //}

    //public override void onNameChanged(string oldValue)
    //{
    //    Debug.LogFormat("onNameChanged old:[{0}] new:[{1}]", oldValue, Name);
    //}
}
