﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class MultiplayerInfoWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MultiplayerInfo), typeof(System.Object));
		L.RegFunction("New", _CreateMultiplayerInfo);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("ip", get_ip, set_ip);
		L.RegVar("port", get_port, set_port);
		L.RegVar("matchId", get_matchId, set_matchId);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMultiplayerInfo(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				MultiplayerInfo obj = new MultiplayerInfo();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: MultiplayerInfo.New");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ip(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MultiplayerInfo obj = (MultiplayerInfo)o;
			string ret = obj.ip;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index ip on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_port(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MultiplayerInfo obj = (MultiplayerInfo)o;
			int ret = obj.port;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index port on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_matchId(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MultiplayerInfo obj = (MultiplayerInfo)o;
			int ret = obj.matchId;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index matchId on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ip(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MultiplayerInfo obj = (MultiplayerInfo)o;
			string arg0 = ToLua.CheckString(L, 2);
			obj.ip = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index ip on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_port(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MultiplayerInfo obj = (MultiplayerInfo)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.port = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index port on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_matchId(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MultiplayerInfo obj = (MultiplayerInfo)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.matchId = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index matchId on a nil value" : e.Message);
		}
	}
}

