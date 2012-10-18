using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace MonkeySpace.Samples.Modules
{
    public class LoggingModule : HubPipelineModule
    {
        protected override bool OnBeforeIncoming(IHubIncomingInvokerContext context)
        {
            Debug.WriteLine("Before method " + context.MethodDescriptor.Name + " on " + context.MethodDescriptor.Hub.Name);
            return base.OnBeforeIncoming(context);
        }

        protected override object OnAfterIncoming(object result, IHubIncomingInvokerContext context)
        {
            Debug.WriteLine("After method " + context.MethodDescriptor.Name + " on " + context.MethodDescriptor.Hub.Name);
            return base.OnAfterIncoming(result, context);
        }

        protected override void OnIncomingError(Exception ex, IHubIncomingInvokerContext context)
        {
            Debug.WriteLine("Invoking method " + context.MethodDescriptor.Name + " on " + context.MethodDescriptor.Name + " gave error " + ex.GetBaseException());
            base.OnIncomingError(ex, context);
        }
    }
}