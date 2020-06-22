using System;
using System.Runtime.InteropServices;

namespace hostcs
{
    /// size_t -> ulong
    /// size_t* -> ref ulong
    /// int* -> ref int
    /// void* -> IntPtr
    /// void** -> IntPtr
    /// const char* -> string
    /// const char* -> IntPtr


    /// LUA_TNONE           -1
    /// LUA_TNIL            0
    /// LUA_TBOOLEAN        1
    /// LUA_TLIGHTUSERDATA  2
    /// LUA_TNUMBER         3
    /// LUA_TSTRING         4
    /// LUA_TTABLE          5
    /// LUA_TFUNCTION       6
    /// LUA_TUSERDATA       7
    /// LUA_TTHREAD         8
    /// LUA_NUMTAGS         9

    public delegate IntPtr lua_Alloc(/* void* */IntPtr ud, /* void* */IntPtr ptr, /* size_t */ulong osize, /* size_t */ulong nsize); // void*
    public delegate int lua_CFunction(IntPtr L);
    /// lua_Debug* -> IntPtr
    public delegate void lua_Hook(IntPtr L, /* lua_Debug* */IntPtr ar);
    /// lua_Integer -> long
    /// lua_KContext -> long
    public delegate int lua_KFunction(IntPtr L, int status, /* lua_KContext */long ctx);
    /// lua_Number -> double
    public delegate IntPtr lua_Reader(IntPtr L, /* void* */IntPtr ud, /* size_t* */ref ulong sz); // const char*
    /// lua_State* -> IntPtr
    /// lua_Unsigned -> ulong
    public delegate int lua_Writer(IntPtr L, /* const void* */IntPtr p, /* size_t */ulong sz, /* void* */IntPtr ud);

    public static class Lua
    {
        public static readonly int LUA_MULTRET = -1;

        [DllImport("lua")] extern public static int lua_absindex(IntPtr L, int idx);
        [DllImport("lua")] extern public static void lua_arith(IntPtr L, int op);
        [DllImport("lua")] extern public static lua_CFunction lua_atpanic(IntPtr L, lua_CFunction panicf);

        public static void lua_call(IntPtr L, int nargs, int nresults)
        {
            lua_callk(L, nargs, nresults, IntPtr.Zero, null);
        }

        [DllImport("lua")] extern public static void lua_callk(IntPtr L, int nargs, int nresults, /* lua_KContext */IntPtr ctx, lua_KFunction k);
        [DllImport("lua")] extern public static int lua_checkstack(IntPtr L, int n);
        [DllImport("lua")] extern public static void lua_close(IntPtr L);
        [DllImport("lua")] extern public static int lua_compare(IntPtr L, int index1, int index2, int op);
        [DllImport("lua")] extern public static void lua_concat(IntPtr L, int n);
        [DllImport("lua")] extern public static void lua_copy(IntPtr L, int fromidx, int toidx);
        [DllImport("lua")] extern public static void lua_createtable(IntPtr L, int narray, int nrec);
        [DllImport("lua")] extern public static int lua_dump(IntPtr L, lua_Writer writer, /* void* */IntPtr data, int strip);
        [DllImport("lua")] extern public static int lua_error(IntPtr L);
        [DllImport("lua")] extern public static int lua_gc(IntPtr L, int what, int data);
        [DllImport("lua")] extern public static lua_Alloc lua_getallocf(IntPtr L, /* void** */IntPtr ud);
        // TODO lua_getextraspace
        [DllImport("lua")] extern public static int lua_getfield(IntPtr L, int idx, /* const char* */string k);
        [DllImport("lua")] extern public static int lua_getglobal(IntPtr L, /* const char* */string name);
        [DllImport("lua")] extern public static lua_Hook lua_gethook(IntPtr L);
        [DllImport("lua")] extern public static int lua_gethookcount(IntPtr L);
        [DllImport("lua")] extern public static int lua_gethookmask(IntPtr L);
        [DllImport("lua")] extern public static int lua_geti(IntPtr L, int idx, /* lua_Integer */long n);
        [DllImport("lua")] extern public static int lua_getinfo(IntPtr L, /* const char* */string what, /* lua_Debug* */IntPtr ar);
        [DllImport("lua")] extern public static IntPtr lua_getlocal(IntPtr L, /* const lua_Debug* */IntPtr ar, int n); // const char*
        [DllImport("lua")] extern public static int lua_getmetatable(IntPtr L, int objindex);
        [DllImport("lua")] extern public static int lua_getstack(IntPtr L, int level, /* const lua_Debug* */IntPtr ar);
        [DllImport("lua")] extern public static int lua_gettable(IntPtr L, int idx);
        [DllImport("lua")] extern public static int lua_gettop(IntPtr L);
        [DllImport("lua")] extern public static IntPtr lua_getupvalue(IntPtr L, int funcindex, int n); // const char*
        [DllImport("lua")] extern public static int lua_getuservalue(IntPtr L, int idx);

        public static void lua_insert(IntPtr L, int idx)
        {
            lua_rotate(L, idx, 1);
        }

        public static int lua_isboolean(IntPtr L, int idx)
        {
            if (/* LUA_TBOOLEAN */1 == lua_type(L, idx))
            {
                return 1;
            }
            return 0;
        }

        [DllImport("lua")] extern public static int lua_iscfunction(IntPtr L, int idx);

        public static int lua_isfunction(IntPtr L, int idx)
        {
            if (/* LUA_TFUNCTION */6 == lua_type(L, idx))
            {
                return 1;
            }
            return 0;
        }

        [DllImport("lua")] extern public static int lua_isinteger(IntPtr L, int idx);

        public static int lua_islightuserdata(IntPtr L, int idx)
        {
            if (/* LUA_TLIGHTUSERDATA */2 == lua_type(L, idx))
            {
                return 1;
            }
            return 0;
        }

        public static int lua_isnil(IntPtr L, int idx)
        {
            if (/* LUA_TNIL */0 == lua_type(L, idx))
            {
                return 1;
            }
            return 0;
        }

        public static int lua_isnone(IntPtr L, int idx)
        {
            if (/* LUA_TNONE */-1 == lua_type(L, idx))
            {
                return 1;
            }
            return 0;
        }

        public static int lua_isnoneornil(IntPtr L, int idx)
        {
            if (/* LUA_TNONE or LUA_TNIL */0 >= lua_type(L, idx))
            {
                return 1;
            }
            return 0;
        }

        [DllImport("lua")] extern public static int lua_isnumber(IntPtr L, int idx);
        [DllImport("lua")] extern public static int lua_isstring(IntPtr L, int idx);

        public static int lua_istable(IntPtr L, int idx)
        {
            if (/* LUA_TTABLE */5 == lua_type(L, idx))
            {
                return 1;
            }
            return 0;
        }

        public static int lua_isthread(IntPtr L, int idx)
        {
            if (/* LUA_TTHREAD */8 == lua_type(L, idx))
            {
                return 1;
            }
            return 0;
        }

        [DllImport("lua")] extern public static int lua_isuserdata(IntPtr L, int idx);
        [DllImport("lua")] extern public static int lua_isyieldable(IntPtr L);
        [DllImport("lua")] extern public static void lua_len(IntPtr L, int idx);
        [DllImport("lua")] extern public static int lua_load(IntPtr L, lua_Reader reader, /* void* */IntPtr data, /* const char* */string chunkname, /* const char* */string mode);
        [DllImport("lua")] extern public static IntPtr lua_newstate(lua_Alloc f, /* void* */IntPtr ud); // lua_State*

        public static void lua_newtable(IntPtr L)
        {
            lua_createtable(L, 0, 0);
        }

        [DllImport("lua")] extern public static IntPtr lua_newthread(IntPtr L); // lua_State*
        [DllImport("lua")] extern public static IntPtr lua_newuserdata(IntPtr L, /* size_t */ulong size); // void*
        [DllImport("lua")] extern public static int lua_next(IntPtr L, int idx);
        // TODO lua_numbertointeger

        public static int lua_pcall(IntPtr L, int nargs, int nresults, int errfunc)
        {
            return lua_pcallk(L, nargs, nresults, errfunc, 0, null);
        }

        [DllImport("lua")] extern public static int lua_pcallk(IntPtr L, int nargs, int nresults, int errfunc, /* lua_KContext */long ctx, lua_KFunction k);

        public static void lua_pop(IntPtr L, int n)
        {
            lua_settop(L, -n - 1);
        }

        [DllImport("lua")] extern public static void lua_pushboolean(IntPtr L, int b);
        [DllImport("lua")] extern public static void lua_pushcclosure(IntPtr L, lua_CFunction fn, int n);

        public static void lua_pushcfunction(IntPtr L, lua_CFunction fn)
        {
            lua_pushcclosure(L, fn, 0);
        }

        // TODO lua_pushfstring
        // TODO lua_pushglobaltable

        [DllImport("lua")] extern public static void lua_pushinteger(IntPtr L, /* lua_Integer */long n);
        [DllImport("lua")] extern public static void lua_pushlightuserdata(IntPtr L, /* void* */IntPtr p);

        public static IntPtr lua_pushliteral(IntPtr L, /* const char* */string s) // const char*
        {
            return lua_pushstring(L, s);
        }

        [DllImport("lua")] extern public static IntPtr lua_pushlstring(IntPtr L, /* const char* */string s, /* size_t */ulong len); // const char*
        [DllImport("lua")] extern public static void lua_pushnil(IntPtr L);
        [DllImport("lua")] extern public static void lua_pushnumber(IntPtr L, /* lua_Number */double n);
        [DllImport("lua")] extern public static IntPtr lua_pushstring(IntPtr L, /* const char* */string s); // const char*
        [DllImport("lua")] extern public static int lua_pushthread(IntPtr L);
        [DllImport("lua")] extern public static void lua_pushvalue(IntPtr L, int idx);
        // TODO lua_pushvfstring
        [DllImport("lua")] extern public static int lua_rawequal(IntPtr L, int index1, int index2);
        [DllImport("lua")] extern public static int lua_rawget(IntPtr L, int idx);
        [DllImport("lua")] extern public static int lua_rawgeti(IntPtr L, int idx, long n);
        [DllImport("lua")] extern public static int lua_rawgetp(IntPtr L, int idx, /* const void* */IntPtr p);
        [DllImport("lua")] extern public static ulong lua_rawlen(IntPtr L, int idx); // size_t
        [DllImport("lua")] extern public static void lua_rawset(IntPtr L, int idx);
        [DllImport("lua")] extern public static void lua_rawseti(IntPtr L, int idx, /* lua_Integer */long n);
        [DllImport("lua")] extern public static void lua_rawsetp(IntPtr L, int idx, /* const void* */IntPtr p);

        public static void lua_register(IntPtr L, /* const char* */string name, lua_CFunction fn)
        {
            lua_pushcfunction(L, fn);
            lua_setglobal(L, name);
        }

        public static void lua_remove(IntPtr L, int idx)
        {
            lua_rotate(L, idx, -1);
            lua_pop(L, 1);
        }

        public static void lua_replace(IntPtr L, int idx)
        {
            lua_copy(L, -1, idx);
            lua_pop(L, 1);
        }

        [DllImport("lua")] extern public static int lua_resume(IntPtr L, /* lua_State* */IntPtr from, int nargs);
        [DllImport("lua")] extern public static void lua_rotate(IntPtr L, int idx, int n);
        [DllImport("lua")] extern public static void lua_setallocf(IntPtr L, lua_Alloc f, /* void* */IntPtr ud);
        [DllImport("lua")] extern public static void lua_setfield(IntPtr L, int idx, /* const char* */string k);
        [DllImport("lua")] extern public static void lua_setglobal(IntPtr L, /* const char* */string name);
        [DllImport("lua")] extern public static void lua_sethook(IntPtr L, lua_Hook func, int mask, int count);
        [DllImport("lua")] extern public static void lua_seti(IntPtr L, int idx, /* lua_Integer */long n);
        [DllImport("lua")] extern public static IntPtr lua_setlocal(IntPtr L, /* const lua_Debug* */IntPtr ar, int n); // const char*
        [DllImport("lua")] extern public static int lua_setmetatable(IntPtr L, int objindex);
        [DllImport("lua")] extern public static void lua_settable(IntPtr L, int idx);
        [DllImport("lua")] extern public static void lua_settop(IntPtr L, int idx);
        [DllImport("lua")] extern public static IntPtr lua_setupvalue(IntPtr L, int funcindex, int n); // const char*
        [DllImport("lua")] extern public static void lua_setuservalue(IntPtr L, int idx);
        [DllImport("lua")] extern public static int lua_status(IntPtr L);
        [DllImport("lua")] extern public static ulong lua_stringtonumber(IntPtr L, /* const char* */string s);// size_t
        [DllImport("lua")] extern public static int lua_toboolean(IntPtr L, int idx);
        [DllImport("lua")] extern public static lua_CFunction lua_tocfunction(IntPtr L, int idx);

        public static long lua_tointeger(IntPtr L, int idx)
        {
            int pisnum = 0;
            return lua_tointegerx(L, idx, ref pisnum);
        }

        [DllImport("lua")] extern public static long lua_tointegerx(IntPtr L, int idx, /* int* */ref int pisnum); // lua_Integer
        [DllImport("lua")] extern public static IntPtr lua_tolstring(IntPtr L, int idx, /* size_t* */ref ulong len); // const char*

        public static double lua_tonumber(IntPtr L, int idx)
        {
            int pisnum = 0;
            return lua_tonumberx(L, idx, ref pisnum);
        }

        [DllImport("lua")] extern public static double lua_tonumberx(IntPtr L, int idx, /* int* */ref int pisnum); // lua_Number
        [DllImport("lua")] extern public static IntPtr lua_topointer(IntPtr L, int idx); // const void*

        public static IntPtr lua_tostring(IntPtr L, int idx) // const char*
        {
            ulong len = 0;
            return lua_tolstring(L, idx, ref len);
        }

        [DllImport("lua")] extern public static IntPtr lua_tothread(IntPtr L, int idx); // lua_State*
        [DllImport("lua")] extern public static IntPtr lua_touserdata(IntPtr L, int idx); // void*
        [DllImport("lua")] extern public static int lua_type(IntPtr L, int idx);
        [DllImport("lua")] extern public static IntPtr lua_typename(IntPtr L, int t);
        [DllImport("lua")] extern public static IntPtr lua_upvalueid(IntPtr L, int fidx, int n); // void*
        // TODO lua_upvalueindex
        [DllImport("lua")] extern public static void lua_upvaluejoin(IntPtr L, int fidx1, int n1, int fidx2, int n2);
        [DllImport("lua")] extern public static IntPtr lua_version(IntPtr L); // const lua_Number*
        [DllImport("lua")] extern public static void lua_xmove(IntPtr from, /* lua_State* */IntPtr to, int n);

        public static int lua_yield(IntPtr L, int nresults)
        {
            return lua_yieldk(L, nresults, 0, null);
        }

        [DllImport("lua")] extern public static int lua_yieldk(IntPtr L, int nresults, /* lua_KContext */long ctx, lua_KFunction k);

        /// standard library
        [DllImport("lua")] extern public static int luaopen_base(IntPtr L);
        [DllImport("lua")] extern public static int luaopen_coroutine(IntPtr L);
        [DllImport("lua")] extern public static int luaopen_debug(IntPtr L);
        [DllImport("lua")] extern public static int luaopen_io(IntPtr L);
        [DllImport("lua")] extern public static int luaopen_math(IntPtr L);
        [DllImport("lua")] extern public static int luaopen_os(IntPtr L);
        [DllImport("lua")] extern public static int luaopen_package(IntPtr L);
        [DllImport("lua")] extern public static int luaopen_string(IntPtr L);
        [DllImport("lua")] extern public static int luaopen_table(IntPtr L);
        [DllImport("lua")] extern public static int luaopen_utf8(IntPtr L);
    }

    public static class LuaL
    {
        /// luaL_Buffer* -> IntPtr
        /// luaL_Reg* -> IntPtr
        /// luaL_Stream* -> IntPtr

        // TODO luaL_addchar
        [DllImport("lua")] extern public static void luaL_addlstring(/* luaL_Buffer* */IntPtr B, /* const char* */string s, /* size_t */ulong l);
        // TODO luaL_addsize
        [DllImport("lua")] extern public static void luaL_addstring(/* luaL_Buffer* */IntPtr B, /* const char* */string s);
        [DllImport("lua")] extern public static void luaL_addvalue(/* luaL_Buffer* */IntPtr B);
        // TODO luaL_argcheck
        [DllImport("lua")] extern public static int luaL_argerror(IntPtr L, int arg, /* const char* */string extramsg);
        [DllImport("lua")] extern public static void luaL_buffinit(IntPtr L, /* luaL_Buffer* */IntPtr B);
        [DllImport("lua")] extern public static IntPtr luaL_buffinitsize(IntPtr L, /* luaL_Buffer* */IntPtr B, /* size_t */ulong sz); // char*
        [DllImport("lua")] extern public static int luaL_callmeta(IntPtr L, int obj, /* const char* */string event_);
        [DllImport("lua")] extern public static void luaL_checkany(IntPtr L, int arg);
        [DllImport("lua")] extern public static long luaL_checkinteger(IntPtr L, int arg); // lua_Integer
        [DllImport("lua")] extern public static IntPtr luaL_checklstring(IntPtr L, int arg, /* size_t* */ref ulong len); // const char*
        [DllImport("lua")] extern public static double luaL_checknumber(IntPtr L, int arg); // lua_Number
        // TODO [DllImport("lua")] extern public static int luaL_checkoption(IntPtr L, int arg, /* const char* */string def, const char* const lst[])
        [DllImport("lua")] extern public static void luaL_checkstack(IntPtr L, int space, /* const char* */string msg);

        public static IntPtr luaL_checkstring(IntPtr L, int arg)
        {
            ulong len = 0;
            return luaL_checklstring(L, arg, ref len);
        }

        [DllImport("lua")] extern public static void luaL_checktype(IntPtr L, int arg, int t);
        [DllImport("lua")] extern public static IntPtr luaL_checkudata(IntPtr L, int ud, /* const char* */string tname); // void*

        // TODO luaL_checkversion

        public static int luaL_dofile(IntPtr L, /* const char* */string filename)
        {
            if (0 != luaL_loadfile(L, filename) || 0 != Lua.lua_pcall(L, 0, Lua.LUA_MULTRET, 0))
            {
                return 1;
            }
            return 0;
        }

        public static int luaL_dostring(IntPtr L, /* const char* */string s)
        {
            if (0 != luaL_loadstring(L, s) || 0 != Lua.lua_pcall(L, 0, Lua.LUA_MULTRET, 0))
            {
                return 1;
            }
            return 0;
        }

        // TODO luaL_error

        [DllImport("lua")] extern public static int luaL_execresult(IntPtr L, int stat);
        [DllImport("lua")] extern public static int luaL_fileresult(IntPtr L, int stat, /* const char* */string fname);
        [DllImport("lua")] extern public static int luaL_getmetafield(IntPtr L, int obj, /* const char* */string event_);
        // TODO luaL_getmetatable
        [DllImport("lua")] extern public static int luaL_getsubtable(IntPtr L, int idx, /* const char* */string fname);
        [DllImport("lua")] extern public static IntPtr luaL_gsub(IntPtr L, /* const char* */string s, /* const char* */string p, /* const char* */string r); // const char*
        [DllImport("lua")] extern public static long luaL_len(IntPtr L, int idx); // lua_Integer

        public static int luaL_loadbuffer(IntPtr L, /* const char* */string buff, /* size_t */ulong size, /* const char* */string name)
        {
            return luaL_loadbufferx(L, buff, size, name, null);
        }

        [DllImport("lua")] extern public static int luaL_loadbufferx(IntPtr L, /* const char* */string buff, /* size_t */ulong size, /* const char* */string name, /* const char* */string mode);

        public static int luaL_loadfile(IntPtr L, /* const char* */string filename)
        {
            return luaL_loadfilex(L, filename, null);
        }

        [DllImport("lua")] extern public static int luaL_loadfilex(IntPtr L, /* const char* */string filename, /* const char* */string mode);
        [DllImport("lua")] extern public static int luaL_loadstring(IntPtr L, /* const char* */string s);
        // TODO luaL_newlib
        // TODO luaL_newlibtable
        [DllImport("lua")] extern public static int luaL_newmetatable(IntPtr L, /* const char* */string tname);
        [DllImport("lua")] extern public static IntPtr luaL_newstate(); // lua_State*
        [DllImport("lua")] extern public static void luaL_openlibs(IntPtr L);
        // TODO luaL_opt
        [DllImport("lua")] extern public static long luaL_optinteger(IntPtr L, int arg, /* lua_Integer */long def); // lua_Integer
        [DllImport("lua")] extern public static IntPtr luaL_optlstring(IntPtr L, int arg, /* const char* */string def, /* size_t* */ref ulong len); // const char*
        [DllImport("lua")] extern public static double luaL_optnumber(IntPtr L, int arg, /* lua_Number */double def); // lua_Number

        public static IntPtr luaL_optstring(IntPtr L, int arg, /* const char* */string def)
        {
            ulong len = 0;
            return luaL_optlstring(L, arg, def, ref len);
        }

        public static IntPtr luaL_prepbuffer(/* luaL_Buffer* */IntPtr B)
        {
            return luaL_prepbuffsize(B, 8192); // TODO
        }

        [DllImport("lua")] extern public static IntPtr luaL_prepbuffsize(/* luaL_Buffer* */IntPtr B, /* size_t */ulong sz); // char*

        [DllImport("lua")] extern public static void luaL_pushresult(/* luaL_Buffer* */IntPtr B);
        [DllImport("lua")] extern public static void luaL_pushresultsize(/* luaL_Buffer* */IntPtr B, /* size_t */ulong sz);
        [DllImport("lua")] extern public static int luaL_ref(IntPtr L, int t);
        [DllImport("lua")] extern public static void luaL_requiref(IntPtr L, /* const char* */string modname, lua_CFunction openf, int glb);
        // TODO [DllImport("lua")] extern public static void luaL_setfuncs(IntPtr L, const luaL_Reg* l, int nup);
        [DllImport("lua")] extern public static void luaL_setmetatable(IntPtr L, /* const char* */string tname);
        [DllImport("lua")] extern public static IntPtr luaL_testudata(IntPtr L, int ud, /* const char* */string tname); // void*
        [DllImport("lua")] extern public static IntPtr luaL_tolstring(IntPtr L, int idx, /* size_t* */ref ulong len); // const char*
        [DllImport("lua")] extern public static void luaL_traceback(IntPtr L, /* lua_State* */IntPtr L1, /* const char* */string msg, int level);

        public static IntPtr luaL_typename(IntPtr L, int idx) // const char*
        {
            return Lua.lua_typename(L, Lua.lua_type(L, idx));
        }

        [DllImport("lua")] extern public static void luaL_unref(IntPtr L, int t, int ref_);
        [DllImport("lua")] extern public static void luaL_where(IntPtr L, int level);
    }

    public static class Helper
    {
        public static string IntPtr2string(IntPtr ptr)
        {
            if (IntPtr.Zero == ptr)
            {
                return "";
            }
            return Marshal.PtrToStringAnsi(ptr);
        }

        public static double IntPtr2double(IntPtr ptr)
        {
            if (IntPtr.Zero == ptr)
            {
                return 0;
            }

            byte[] bytes = new byte[sizeof(double)];

            for (int i = 0; i < bytes.Length; ++i)
            {
                bytes[i] = Marshal.ReadByte(ptr, i);
            }

            return BitConverter.ToDouble(bytes, 0);
        }
    }
}