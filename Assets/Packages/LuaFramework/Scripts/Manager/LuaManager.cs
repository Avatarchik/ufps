﻿using UnityEngine;
using System.Collections;
using LuaInterface;

namespace LuaFramework {
    public class LuaManager : Manager {
        private LuaState lua;
        private LuaLoader loader;
        private LuaLooper loop = null;

        // Use this for initialization
        void Awake() {
            loader = new LuaLoader();
            lua = new LuaState();
            this.OpenLibs();
            lua.LuaSetTop(0);

            LuaBinder.Bind(lua);
            LuaCoroutine.Register(lua, this);
        }

        public void InitStart() {
            InitLuaPath();
            InitLuaBundle();
            this.lua.Start();    //启动LUAVM
            this.StartMain();
            this.StartLooper();
        }

        void StartLooper() {
            loop = gameObject.AddComponent<LuaLooper>();
            loop.luaState = lua;
        }

        //cjson 比较特殊，只new了一个table，没有注册库，这里注册一下
        protected void OpenCJson() {
            lua.LuaGetField(LuaIndexes.LUA_REGISTRYINDEX, "_LOADED");
            lua.OpenLibs(LuaDLL.luaopen_cjson);
            lua.LuaSetField(-2, "cjson");

            lua.OpenLibs(LuaDLL.luaopen_cjson_safe);
            lua.LuaSetField(-2, "cjson.safe");
        }

        void StartMain() {
            lua.DoFile("Main.lua");

            LuaFunction main = lua.GetFunction("Main");
            main.Call();
            main.Dispose();
            main = null;    
        }
        
        /// <summary>
        /// 初始化加载第三方库
        /// </summary>
        void OpenLibs() {
            lua.OpenLibs(LuaDLL.luaopen_pb);      
            lua.OpenLibs(LuaDLL.luaopen_sproto_core);
            lua.OpenLibs(LuaDLL.luaopen_protobuf_c);
            lua.OpenLibs(LuaDLL.luaopen_lpeg);
            lua.OpenLibs(LuaDLL.luaopen_bit);
            lua.OpenLibs(LuaDLL.luaopen_socket_core);
            lua.OpenLibs(LuaDLL.luaopen_struct);

            this.OpenCJson();
        }

        /// <summary>
        /// 初始化Lua代码加载路径
        /// </summary>
        void InitLuaPath() {
            if (AppConst.DebugMode) {
                string rootPath = AppConst.FrameworkRoot;
                lua.AddSearchPath(rootPath + "/Lua");
                lua.AddSearchPath(rootPath + "/ToLua/Lua");
            } else {
#if UNITY_EDITOR
                if (BundleManager.SimulateAssetBundleInEditor) {
                    string rootPath = AppConst.FrameworkRoot;
                    lua.AddSearchPath(rootPath + "/Lua");
                    lua.AddSearchPath(rootPath + "/ToLua/Lua");
                    lua.AddSearchPath(rootPath + "/lua/Proto");
                    lua.AddSearchPath(rootPath + "/lua/Proto/Common");
                    lua.AddSearchPath(rootPath + "/lua/Proto/Model");
                    lua.AddSearchPath(rootPath + "/lua/Data");
                }
#endif
                lua.AddSearchPath(Util.DataPath + "lua");
                lua.AddSearchPath(Util.DataPath + "lua/Proto");
                lua.AddSearchPath(Util.DataPath + "lua/Proto/Common");
                lua.AddSearchPath(Util.DataPath + "lua/Proto/Model");
                lua.AddSearchPath(Util.DataPath + "lua/Data");
            }
        }

        /// <summary>
        /// 初始化LuaBundle
        /// </summary>
        void InitLuaBundle() {
            if (loader.beZip) {
                loader.AddBundle("lua/lua.unity3d");
                loader.AddBundle("lua/lua_math.unity3d");
                loader.AddBundle("lua/lua_system.unity3d");
                loader.AddBundle("lua/lua_system_reflection.unity3d");
                loader.AddBundle("lua/lua_unityengine.unity3d");
                loader.AddBundle("lua/lua_common.unity3d");
                loader.AddBundle("lua/lua_logic.unity3d");
                loader.AddBundle("lua/lua_view.unity3d");
                loader.AddBundle("lua/lua_controller.unity3d");
                loader.AddBundle("lua/lua_misc.unity3d");

                loader.AddBundle("lua/lua_protobuf.unity3d");
                loader.AddBundle("lua/lua_3rd_cjson.unity3d");
                loader.AddBundle("lua/lua_3rd_luabitop.unity3d");
                loader.AddBundle("lua/lua_3rd_pbc.unity3d");
                loader.AddBundle("lua/lua_3rd_pblua.unity3d");
                loader.AddBundle("lua/lua_3rd_sproto.unity3d");
            }
        }

        //private void IteratorTable()
        //{
        //    while (lua.LuaNext(-2))
        //    {
        //        lua.LuaPushValue(-2);

        //        var key = lua.LuaToString(-1);

        //        if (lua.LuaIsNumber(-2))
        //        {

        //        }
        //        else if (lua.LuaIsString(-2))
        //        {
        //            var s = lua.LuaToString(-2);
        //            var i = 0;
        //        }
        //        else if (lua.lua_istable(-2))
        //        {
        //            lua.LuaPushValue(-2);
        //            lua.LuaPushNil();

        //            IteratorTable();

        //            lua.LuaPop(1);
        //        }
        //        lua.LuaPop(2);
        //    }
        //}

        //public void TestTable(string name, object obj)
        //{
        //    lua.LuaGetGlobal(name);
        //    //lua.PushValue(obj);

        //    lua.LuaPushNil();

        //    IteratorTable();

        //    lua.LuaPop(1);

        //}
        public object GetGlobal(string name)
        {
            lua.LuaGetGlobal(name);
            var obj = lua.ToVariant(-1);
            lua.LuaPop(1);
            return obj;
        }

        public object[] DoFile(string filename) {
            return lua.DoFile(filename);
        }

        // Update is called once per frame
        public object[] CallFunction(string funcName, params object[] args) {
            LuaFunction func = lua.GetFunction(funcName);
            if (func != null) {
                return func.Call(args);
            }
            return null;
        }

        public void LuaGC() {
            lua.LuaGC(LuaGCOptions.LUA_GCCOLLECT);
        }

        public void Close() {
            loop.Destroy();
            loop = null;

            lua.Dispose();
            lua = null;
            loader = null;
        }
    }
}