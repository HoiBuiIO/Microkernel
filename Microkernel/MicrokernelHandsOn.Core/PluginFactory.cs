using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using MicrokernelHandsOn.Contract;

namespace MicrokernelHandsOn.Core
{
    public class PluginFactory
    {
        private readonly PluginConfigurationSection _pluginConfigurationSection;

        public PluginFactory()
        {
            _pluginConfigurationSection = ConfigurationManager.GetSection("pluginSection") as PluginConfigurationSection;
        }

        public IEnumerable<IPlugin> GetPlugins()
        {
            var plugins = new List<IPlugin>();

            foreach (PluginElement plugin in _pluginConfigurationSection.PluginElementCollection)
            {
                var type = Type.GetType(plugin.Type);

                if (type == null)
                    continue;

                var pluginInstance1 = Activator.CreateInstance(type) as IPlugin;

                if (pluginInstance1 == null)
                    continue;
                plugins.Add(pluginInstance1);
            }
            return plugins;
        }
    }
}
