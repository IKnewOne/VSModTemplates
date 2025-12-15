#if (IncludeHarmony)
using HarmonyLib;
#endif
using System;
using Vintagestory.API.Common;

namespace _ProjectName_;
public class _ProjectName_ : ModSystem {
	public static ILogger Logger { get; private set; }
	public static ICoreAPI Api { get; private set; }
#if (IncludeHarmony)
    public static Harmony harmony { get; private set; }
#endif
#if (AddAdvancedConfig and AddSampleConfig)
	public static event Action<ISetting> SettingChanged;
#endif
	public override void StartPre(ICoreAPI api) {
		base.StartPre(api);
		Logger = Mod.Logger;
		Api = api;
#if (IncludeHarmony)
        harmony = new Harmony(Mod.Info.ModID);
        harmony.PatchAll();
#endif
	}

	public override void Start(ICoreAPI api) {
		base.Start(api);

#if (AddSampleConfig)
        try {
			ModConfig.Instance = api.LoadModConfig<ModConfig>(ModConfig.ConfigName) ??= new ModConfig();
			api.StoreModConfig(ModConfig.Instance, ModConfig.ConfigName);
		} catch (Exception _) { ModConfig.Instance = new ModConfig(); }
#endif
#if (AddAdvancedConfig and AddSampleConfig)
		if (api.ModLoader.IsModEnabled("configlib")) {
			SubscribeToConfigChange(api);

			SettingChanged += (setting) => {
			};
		}
#endif
	}
#if (AddAdvancedConfig and AddSampleConfig)
	private void SubscribeToConfigChange(ICoreAPI api) {
		ConfigLibModSystem system = api.ModLoader.GetModSystem<ConfigLibModSystem>();

		system.SettingChanged += (domain, config, setting) => {
			if (domain != "_modid_")
				return;

			setting.AssignSettingValue(ModConfig.Instance);
		};
	}
#endif


	public override void Dispose() {
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
