using System;

namespace hostcs
{
    class Hostcs
    {
        public static int Say(string str)
        {
            Console.WriteLine("say {0}", str);
            return 9;
        }
        public static int SayWrap(IntPtr L)
        {
            int argc = Lua.lua_gettop(L);

            Lua.lua_pushinteger(L, Say(Helper.IntPtr2string(Lua.lua_tostring(L, 1))));

            return 1;
        }
        static void Main(string[] args)
        {
            testLua(args);
        }

        static private void testLua(string[] args)
        {
            IntPtr L = LuaL.luaL_newstate();

            Console.WriteLine("{0}", Helper.IntPtr2double(Lua.lua_version(L)));

            LuaL.luaL_openlibs(L);

            Lua.lua_register(L, "say", SayWrap);

            Lua.lua_register(L, "bridge", delegate (IntPtr L)
            {
                Lua.lua_pushinteger(L, 12345679);
                return 1;
            });

            LuaL.luaL_dofile(L, args[0]);
            Lua.lua_setglobal(L, "sun");
            Lua.lua_settop(L, 0);

            Lua.lua_getglobal(L, "sun");
            Lua.lua_getfield(L, -1, "hello");
            Lua.lua_call(L, 0, 0);
            Lua.lua_settop(L, 0);

            Lua.lua_getglobal(L, "hi");
            Lua.lua_call(L, 0, 0);
            Lua.lua_settop(L, 0);

            LuaL.luaL_dofile(L, args[1]);
            Lua.lua_settop(L, 0);

            Lua.lua_close(L);
        }
    }
}
