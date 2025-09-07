#if ( IncludeHarmony )
using HarmonyLib;
#endif
using System;
using Vintagestory.API.Common;

namespace _ProjectName_;
public class _ProjectName_ModSystem : ModSystem
{
    public static ILogger Logger { get; private set; }
    public static ICoreAPI Api { get; private set; }
    #if (IncludeHarmony)
    public static Harmony harmony { get; private set; }
    #endif

	public override void StartPre(ICoreAPI api)
    {
        base.StartPre(api);
        #if (IncludeHarmony)
        harmony = new Harmony(Mod.Info.ModID);
        harmony.PatchAll();
        #endif
	}
    
	public override void Start(ICoreAPI api)
    {
        base.Start(api);

        Logger = Mod.Logger;
		Api = api;

        #if (AddSampleConfig)
        try {
			Config = api.LoadModConfig<ModConfig>(ModConfig.ConfigName);
			if (Config == null) {
				Config = new ModConfig();
				Logger.VerboseDebug("[_ProjectName_]Config file not found, creating a new one...");
			}
			api.StoreModConfig(Config, ConfigName);
		} catch (Exception e) {
			Logger.Error("[_ProjectName_] Failed to load config, you probably made a typo: {0}", e);
			Config = new ModConfig();
		}
        #endif
	}
    
    public override void Dispose()
    {
        Logger = null;
        Api = null;
        #if (IncludeHarmony)
        harmony?.UnpatchAll(Mod.Info.ModID);
        harmony = null;
        #endif
        #if (AddSampleConfig)
        Config = null;
        #endif
        base.Dispose();
    }
}