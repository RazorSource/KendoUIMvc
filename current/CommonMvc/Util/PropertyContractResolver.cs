using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMvc.Util
{
    /// <summary>
    /// NewtonSoft contract resolver utility class that can be used to specify specific properties to serialize.
    /// Annotations are typically used to determine the properties to serialize, however this contract resolver
    /// can be used to specify custom filters to minimize the properties that are serialized.  Example usage:
    /// 
    /// JsonSerializerSettings jsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
    /// {
    ///     ContractResolver = new PropertyContractResolver(
    ///         new string[] { "Text", "Value" })
    /// };
    /// 
    /// Newtonsoft.Json.JsonConvert.SerializeObject(yourObject, jsonSerializerSettings)
    /// </summary>
    public class PropertyContractResolver : DefaultContractResolver
    {
        IEnumerable<string> propertyNames;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="propertyNames">List of the property names that should be serialized.</param>
        public PropertyContractResolver(IEnumerable<string> propertyNames)
        {
            if (propertyNames == null)
            {
                throw new ArgumentNullException("propertyNames", "propertyNames is required.");
            }

            this.propertyNames = propertyNames;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, Newtonsoft.Json.MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

            return properties.Where(p => propertyNames.Contains(p.PropertyName)).ToList();
        }
    }
}
