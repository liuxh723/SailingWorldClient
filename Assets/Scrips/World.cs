using KBEngine;
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class World : MonoBehaviour {

    private UnityEngine.GameObject player = null;
    public UnityEngine.GameObject entityPerfab;
    public UnityEngine.GameObject avatarPerfab;

    public Camera mainCamera;

    // Use this for initialization
    void Start () {

       // KBEngine.Event.registerOut("addSpaceGeometryMapping", this, "addSpaceGeometryMapping");
        KBEngine.Event.registerOut("onEnterWorld", this, "onEnterWorld");
        KBEngine.Event.registerOut("onLeaveWorld", this, "onLeaveWorld");
        KBEngine.Event.registerOut("set_position", this, "set_position");
        KBEngine.Event.registerOut("set_direction", this, "set_direction");
        // KBEngine.Event.registerOut("updatePosition", this, "updatePosition");
        // KBEngine.Event.registerOut("onControlled", this, "onControlled");

        KBEngine.Event.registerOut("onAvatarEnterWorld", this, "onAvatarEnterWorld");

    }

    // Update is called once per frame
    void Update () {
        createPlayer();

    }
    public void createPlayer()
    {
        if (player != null)
        {
          
                player.GetComponent<GameEntity>().entityEnable();
            return;
        }

        if (KBEngineApp.app.entity_type != "Avatar")
        {
            return;
        }

        Avatar avatar = (Avatar)KBEngineApp.app.player();
        if (avatar == null)
        {
            Debug.Log("wait create(palyer)!");
            return;
        }

        float y = avatar.position.y;
        if (avatar.isOnGround)
            y = 1.3f;

        player = Instantiate(avatarPerfab, new Vector3(avatar.position.x, y, avatar.position.z),
                             Quaternion.Euler(new Vector3(avatar.direction.y, avatar.direction.z, avatar.direction.x))) as UnityEngine.GameObject;

        player.GetComponent<GameEntity>().entityDisable();
        avatar.renderObj = player;
        ((UnityEngine.GameObject)avatar.renderObj).GetComponent<GameEntity>().isPlayer = true;

        // 有必要设置一下，由于该接口由Update异步调用，有可能set_position等初始化信息已经先触发了
        // 那么如果不设置renderObj的位置和方向将为0，人物会陷入地下
        set_position(avatar);
        set_direction(avatar);

        //mainCamera.GetComponent<CameraManager>().SetTarget(player);

    }
    public void onAvatarEnterWorld(UInt64 rndUUID, Int32 eid, Avatar avatar)
    {
        if (!avatar.isPlayer())
        {
            return;
        }
        Debug.Log("loading scene...");
    }

    public void onEnterWorld(KBEngine.Entity entity)
    {
        if (entity.isPlayer())
            return;

        float y = entity.position.y;
        if (entity.isOnGround)
            y = 1.3f;

        entity.renderObj = Instantiate(entityPerfab, new Vector3(entity.position.x, y, entity.position.z),
            Quaternion.Euler(new Vector3(entity.direction.y, entity.direction.z, entity.direction.x))) as UnityEngine.GameObject;

        ((UnityEngine.GameObject)entity.renderObj).name = entity.className + "_" + entity.id;
    }

    public void onLeaveWorld(KBEngine.Entity entity)
    {
        if (entity.renderObj == null)
            return;

        UnityEngine.GameObject.Destroy((UnityEngine.GameObject)entity.renderObj);
        entity.renderObj = null;
    }

    public void set_position(KBEngine.Entity entity)
    {
        if (entity.renderObj == null)
            return;

        GameEntity gameEntity = ((UnityEngine.GameObject)entity.renderObj).GetComponent<GameEntity>();
        gameEntity.destPosition = entity.position;
        gameEntity.position = entity.position;
        gameEntity.spaceID = KBEngineApp.app.spaceID;
    }
    public void onControlled(KBEngine.Entity entity, bool isControlled)
    {
        if (entity.renderObj == null)
            return;

        GameEntity gameEntity = ((UnityEngine.GameObject)entity.renderObj).GetComponent<GameEntity>();
        gameEntity.isControlled = isControlled;
    }

    public void set_direction(KBEngine.Entity entity)
    {
        if (entity.renderObj == null)
            return;

        GameEntity gameEntity = ((UnityEngine.GameObject)entity.renderObj).GetComponent<GameEntity>();
        gameEntity.destDirection = new Vector3(entity.direction.y, entity.direction.z, entity.direction.x);
        gameEntity.spaceID = KBEngineApp.app.spaceID;
    }
}
