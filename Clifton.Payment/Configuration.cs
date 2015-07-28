﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clifton.Payment {
    public class GatewaySection : ConfigurationElement {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name {
            get { return this["name"] as string; }
        }
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url {
            get { return this["url"] as string; }
        }
        [ConfigurationProperty("apiKey", IsRequired = true)]
        public string ApiKey {
            get { return this["apiKey"] as string; }
        }
        [ConfigurationProperty("apiSecret", IsRequired = true)]
        public string ApiSecret {
            get { return this["apiSecret"] as string; }
        }
        [ConfigurationProperty("token", IsRequired = true)]
        public string Token {
            get { return this["token"] as string; }
        }
    }

    public class GatewaysSection : ConfigurationElementCollection {
        public GatewaySection this[int index] {
            get {
                return base.BaseGet(index) as GatewaySection;
            }
            set {
                if (base.BaseGet(index) != null) {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new GatewaySection this[string responseString] {
            get { return (GatewaySection)BaseGet(responseString); }
            set {
                if (BaseGet(responseString) != null) {
                    BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));
                }
                BaseAdd(value);
            }
        }

        protected override System.Configuration.ConfigurationElement CreateNewElement() {
            return new GatewaySection();
        }

        protected override object GetElementKey(System.Configuration.ConfigurationElement element) {
            return ((GatewaySection)element).Name;
        }
    }

    public class Configuration : ConfigurationSection {
        public static Configuration GetConfig() {
            return (Configuration)System.Configuration.ConfigurationManager.GetSection("Clifton.Payment") ?? new Configuration();
        }

        [System.Configuration.ConfigurationProperty("Gateways")]
        [ConfigurationCollection(typeof(GatewaysSection), AddItemName = "Gateway")]
        public GatewaysSection Gateways {
            get {
                object o = this["Gateways"];
                return o as GatewaysSection;
            }
        }
    }
}
