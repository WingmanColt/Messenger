using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Text.RegularExpressions;

namespace HireMe.Utility
{
        public class SlugifyParameterTransformer : IOutboundParameterTransformer
        {
            public string TransformOutbound(object value)
            {
                // Slugify value
                return value == null ? null : Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
            }
        }
    }
