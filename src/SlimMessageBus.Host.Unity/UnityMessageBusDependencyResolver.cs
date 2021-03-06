﻿using Common.Logging;
using System;
using System.Globalization;
using SlimMessageBus.Host.DependencyResolver;
using Unity;

namespace SlimMessageBus.Host.Unity
{
    public class UnityMessageBusDependencyResolver : IDependencyResolver
    {
        private static readonly ILog Log = LogManager.GetLogger<UnityMessageBusDependencyResolver>();

        public static IUnityContainer Container { get; set; }

        #region Implementation of IDependencyResolver

        public object Resolve(Type type)
        {
            if (Container == null)
            {
                throw new ArgumentNullException($"The {nameof(Container)} property was null at this point");
            }

            Log.TraceFormat(CultureInfo.InvariantCulture, "Resolving type {0}", type);
            var o = Container.Resolve(type);
            Log.DebugFormat(CultureInfo.InvariantCulture, "Resolved type {0} to object {1}", type, o);
            return o;
        }

        #endregion
    }
}
