#if ( AddSampleConfig )
using Vintagestory.API.Config;
using _ProjectName_.Config;
#endif
#if ( ModSide != "server" )
using Vintagestory.API.Client;
#endif
#if ( ModSide != "client" )
using Vintagestory.API.Server;
#endif
#if ( IncludeHarmony )
using HarmonyLib;
#endif
using Vintagestory.API.Common;

namespace _ProjectName_;

public class _ProjectName_ModSystem : ModSystem {
    public static ILogger Logger { get; private set; }
    #if ( ModSide == "client" )
    public static ICoreClientAPI Capi { get; private set; }
    #elif ( ModSide == "server" )
    public static ICoreServerAPI Sapi { get; private set; }
    #else
    public static ICoreAPI Api { get; private set; }
    #endif
    #if( IncludeHarmony )
    public static Harmony harmony { get; private set; }
    #endif
    #if( AddSampleConfig )
    public static ModConfig Config => ConfigLoader.Config;
    #endif

    public override void StartPre(ICoreAPI api)
    {
        base.StartPre(api);
        #if ( ModSide == "client" )
        Capi = api as ICoreClientAPI;
        #elif ( ModSide == "server" )
        Sapi = api as ICoreServerAPI;
        #else
        Api = api;
        #endif
        Logger = Mod.Logger;

        #if( IncludeHarmony )
        harmony = new Harmony(Mod.Info.ModID);
        harmony.PatchAll();
        #endif
    }
    
    public override void Start(ICoreAPI api)
    {
        base.Start(api);
    }
    
    #if( ModSide != "server" )
    public override void StartClientSide(ICoreClientAPI api)
    {
        base.StartClientSide(api);
    }
    #endif

    #if( ModSide != "client" )
    public override void StartServerSide(ICoreServerAPI api)
    {
        base.StartServerSide(api);
    }
    #endif
    
    public override void Dispose()
    {
        #if ( IncludeHarmony)
        harmony?.UnpatchAll(Mod.Info.ModID);
        harmony = null;
        #endif
        Logger = null;
        #if ( ModSide == "client" )
        Capi = null;
        #elif ( ModSide == "server" )
        Sapi = null;
        #else
        Api = null;
        #endif
        base.Dispose();
    }
}
