// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Data.Entity.Infrastructure.Design
{
    using System.Data.Entity.Utilities;
    using System.Reflection;

#if NETSTANDARD
#else
    using System.Runtime.Remoting;
    using System.Runtime.Remoting.Messaging;
    using System.Runtime.Remoting.Proxies;
#endif

    // <summary>
    // This is a small piece of Remoting magic. It enables us to invoke methods on a
    // remote object without knowing its actual type. The only restriction is that the
    // names and shapes of the types and their members must be the same on each side of
    // the boundary.
    // </summary>
#if NETSTANDARD
    internal class ForwardingProxy<T> : Reflection.DispatchProxy
#else
    internal class ForwardingProxy<T> : RealProxy
#endif
    {
        private readonly MarshalByRefObject _target;

#if NETSTANDARD
        public ForwardingProxy(object target)
        {
            DebugCheck.NotNull(target);
            _target = (MarshalByRefObject)target;
            
        }

        // TODO: ZZZ - Must do something here
        public T GetTransparentProxy()
        {
            return (T)(object)null;
        }

        // <summary>
        // Intercepts method invocations on the object represented by the current instance
        // and forwards them to the target to finish processing.
        // </summary>
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            object result = targetMethod.Invoke(_target, args);
            return result;
        }
#else
        public ForwardingProxy(object target)
            : base(typeof(T))
        {
            DebugCheck.NotNull(target);

            _target = (MarshalByRefObject)target;
        }

        public new T GetTransparentProxy()
        {
            return (T)base.GetTransparentProxy();
        }

        // <summary>
        // Intercepts method invocations on the object represented by the current instance
        // and forwards them to the target to finish processing.
        // </summary>
        public override IMessage Invoke(IMessage msg)
        {
            DebugCheck.NotNull(msg);

            // NOTE: This sets the wrapped message's Uri
            new MethodCallMessageWrapper((IMethodCallMessage)msg).Uri = RemotingServices.GetObjectUri(_target);

            return RemotingServices.GetEnvoyChainForProxy(_target).SyncProcessMessage(msg);
        }
#endif
    }
}